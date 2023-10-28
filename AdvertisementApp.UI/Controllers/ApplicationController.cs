using AdvertisementApp.Application.Interfaces;
using AdvertisementApp.Application.Services;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Domain.Entities;
using AdvertisementApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementApp.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationController : Controller
    {
        private IAdvertisementService _advertisementService { get; }
        public ApplicationController(IAdvertisementService advertisementService)
        {
            this._advertisementService = advertisementService;
        }
        public async Task<IActionResult> List()
        {
            var data = await _advertisementService.GetAllAsync();
            return View(data.Data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementCreateDto dto)
        {
            var response = await _advertisementService.CreateAsync(dto);
            if (response.ResponseType != ResponseType.Success)
            {
                if (response.ValidationErrors == null)
                {
                    ModelState.AddModelError("", "Something went wrong");
                    return View(dto);
                }


                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item?.PropertyName ?? "", item?.ErrorMessage ?? "");
                }
                return View(dto);
            }

            return RedirectToAction("List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _advertisementService.GetByIdAsync<AdvertisementUpdateDto>(id);

            if (response.ResponseType == ResponseType.NotFound)
                return NotFound(response.Message);
            return View(response.Data);

        }
        [HttpPost]
        public async Task<IActionResult> Update(AdvertisementUpdateDto dto)
        {
            var response = await _advertisementService.UpdateAsync(dto);

            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound(response.Message);
            }

            if (response.ResponseType == ResponseType.Success)
            {
                return RedirectToAction("List");
            }

            if (response.ValidationErrors == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View();
            }

            foreach (var item in response.ValidationErrors)
            {
                ModelState.AddModelError(item?.PropertyName ?? "", item?.ErrorMessage ?? "");
            }

            return View(dto);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var response = await _advertisementService.RemoveAsync(id);

            if (response.ResponseType == ResponseType.NotFound)
                return NotFound(response.Message);

            return RedirectToAction("List","Application");
            
        }
    }
}
