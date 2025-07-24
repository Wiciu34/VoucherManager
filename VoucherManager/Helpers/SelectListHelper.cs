using Microsoft.AspNetCore.Mvc.Rendering;

namespace VoucherManager.Helpers;

public static class SelectListHelper
{
    public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj)
    where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.ToString()
            });
    }

}
