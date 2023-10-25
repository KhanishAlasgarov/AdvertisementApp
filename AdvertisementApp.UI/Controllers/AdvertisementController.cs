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

        public AdvertisementController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
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


            

            List<MilitaryStatusListDto> militaryStatuses = Enum.GetValues(typeof(MilitaryStatusType))
                .Cast<MilitaryStatusType>()
                .Select(x => new MilitaryStatusListDto
                {
                    Id = (int)x,
                    Definition = x.ToString()
                }).ToList();

            ViewBag.MilitaryStatuses = new SelectList(militaryStatuses, "Id", "Definition");
            return View(new AdvertisementUserCreateDto { AppUserId = userId, AdvertisementId = advertisementid });
        }
        [HttpPost]
        public IActionResult Send()
        {
            return RedirectToAction();
        }
    }
}
