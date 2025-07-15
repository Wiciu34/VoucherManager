using Microsoft.AspNetCore.Mvc;

namespace VoucherManager.Controllers;

public class VoucherController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
