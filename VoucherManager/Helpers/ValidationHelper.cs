using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VoucherManager.Helpers;

public static class ValidationHelper
{
    public static JsonResult GenerateErrorResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
        );

        return new JsonResult(new { success = false, errors });
    }
}
