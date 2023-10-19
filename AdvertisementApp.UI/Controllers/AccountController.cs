using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Common.ResponseObject;
using AdvertisementApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdvertisementApp.UI.Controllers
{
    public class AccountController : Controller
    {
        public IGenderService _genderService { get; set; }
        public IAppUserService _appUserService { get; set; }

        public AccountController(IGenderService genderService, IAppUserService appUserService)
        {
            _genderService = genderService;
            _appUserService = appUserService;
        }

        public async Task<IActionResult> SignUp()
        {
            ViewBag.Genders = new SelectList(await GetGenders(), "Id", "Definition");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserCreateDto dto)
        {
            var response = await _appUserService.CreateAsync(dto,2);

            if (response.ResponseType == ResponseType.Success)
            {
                return RedirectToAction("Index", "Home");
            }
             
            if (response.ValidationErrors == null)
                return View(dto);
             
            foreach (var item in response.ValidationErrors)
            {
                ModelState.AddModelError("", item.ErrorMessage ?? "");
            }

            ViewBag.Genders = new SelectList(await GetGenders(), "Id", "Definition", dto.GenderId);
            return View(dto);
        }

        public async Task<List<GenderListDto>> GetGenders()
        {
            var responseGender = await _genderService.GetAllAsync();
            return responseGender.Data;
        }
    }
}
