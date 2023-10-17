using AdvertisementApp.Common.ResponseObject;
using AdvertisementApp.Common.ResponseObject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AdvertisementApp.UI.Extensions;

public static class ControllerExtension
{
    public static IActionResult ResponseRedirectAction<T>(this Controller controller, IResponse<T> response, string actionName)
    {
        if (response.ResponseType == ResponseType.NotFound)
            return controller.NotFound();

        if (response.ResponseType == ResponseType.ValidationError)
        {
            foreach (var error in response.ValidationErrors)
            {
                controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            controller.View(response.Data);
        }

        return controller.RedirectToAction(actionName);
    }

    public static IActionResult ResponseView<T>(this Controller controller, IResponse<T> response)
    {
        if (ResponseType.NotFound == response.ResponseType)
            return controller.NotFound();

        return controller.View(response.Data);
    }

    public static IActionResult ResponseRedirectAction(this Controller controller, IResponse response, string actionName)
    {
        if (ResponseType.NotFound == response.ResponseType)
            return controller.NotFound();

        return controller.RedirectToAction(actionName);

    }
}
