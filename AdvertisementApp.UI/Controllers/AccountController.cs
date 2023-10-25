using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Common.ResponseObject;
using AdvertisementApp.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

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
            var response = await _appUserService.CreateAsync(dto, (int)RoleType.Member);

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
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginDto dto)
        {
            var response = await _appUserService.CheckUserAsync(dto);

            if (response.ResponseType != ResponseType.Success)
            {

                ModelState.AddModelError("", response?.Message ?? "");
                return View(dto);
            }

            var roleResponse = await _appUserService.GetRolesByUserIdAsync(response.Data?.Id);

            var claims = new List<Claim>();

            if (roleResponse.ResponseType == ResponseType.Success)
            {
                foreach (var role in roleResponse.Data)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Definition));
                }
                claims.Add(new Claim(ClaimTypes.Name, dto?.Username));

            }
            claims.Add(new Claim(ClaimTypes.NameIdentifier, response.Data.Id.ToString()));


            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = dto.RememberMe,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");



        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<List<GenderListDto>> GetGenders()
        {
            var responseGender = await _genderService.GetAllAsync();



            return responseGender.Data != null ? responseGender.Data : new List<GenderListDto>();
        }
    }
}
