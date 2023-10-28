using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Application.Services;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AdvertisementApp.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private IAppUserService _appUserService { get; }
        private IAdvertisementUserService _advertisementUserService { get; }


        public AdvertisementController(IAppUserService appUserService, IAdvertisementUserService advertisementUserService)
        {
            _appUserService = appUserService;
            _advertisementUserService = advertisementUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Member")]

        public async Task<IActionResult> Send(int advertisementid)
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return NotFound();
            }
            var userId = int.Parse(userIdClaim.Value);
            var userResponse = await _appUserService.GetByIdAsync<AppUserListDto>(userId);

            ViewBag.GenderId = userResponse.Data?.GenderId;

            ViewBag.MilitaryStatuses = GetMilitaryStatus();

            return View(new AdvertisementUserCreateDto { AppUserId = userId, AdvertisementId = advertisementid });
        }
        [HttpPost]
        public async Task<IActionResult> Send(AdvertisementUserCreateDto dto, IFormFile cv)
        {
            var userResponse = await _appUserService.GetByIdAsync<AppUserListDto>(dto.AppUserId);

            if (cv == null)
            {
                ViewBag.MilitaryStatuses = GetMilitaryStatus();
                ViewBag.GenderId = userResponse.Data?.GenderId;
                ModelState.AddModelError("", "Add Cv");
                return View(dto);
            }



            var fileName = Guid.NewGuid() + cv.FileName;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvFiles", fileName);

            var stream = new FileStream(path, FileMode.Create);

            await cv.CopyToAsync(stream);
            dto.CvPath = fileName;
            var response = await _advertisementUserService.CreateWithCvAsync(dto);

            if (response.ResponseType == ResponseType.Error)
            {
                ModelState.AddModelError("", response?.Message ?? "");
                ViewBag.MilitaryStatuses = GetMilitaryStatus();
                ViewBag.GenderId = userResponse.Data?.GenderId;
                return View(dto);
            }
            if (response.ResponseType == ResponseType.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var item in response.ValidationErrors)
            {
                ModelState.AddModelError("", item.ErrorMessage);
            }
            ViewBag.MilitaryStatuses = GetMilitaryStatus();
            ViewBag.GenderId = userResponse.Data?.GenderId;
            return View(dto);


        }


        public SelectList GetMilitaryStatus()
        {
            List<MilitaryStatusListDto> militaryStatuses = Enum.GetValues(typeof(MilitaryStatusType))
                .Cast<MilitaryStatusType>()
                .Select(x => new MilitaryStatusListDto
                {
                    Id = (int)x,
                    Definition = x.ToString()
                }).ToList();

            return new SelectList(militaryStatuses, "Id", "Definition");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List(AdvertisementAppUserStatusType type)
        {
            var response = await _advertisementUserService.GetApplicationsAsync(type);
            ViewBag.type = type;

            if (response.ResponseType == ResponseType.NotFound)
            {
                ViewBag.Count = 0;
                return View();
            }

            return View(response.Data);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetStatus(int advertisementUserId, AdvertisementAppUserStatusType type, AdvertisementAppUserStatusType currentStatus)
        {
            var response = await _advertisementUserService.SetStatusAsync(advertisementUserId, type);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound(response.Message);
            }

            return RedirectToAction("List", "Advertisement", new { type = currentStatus });
        }

        

    }
}
