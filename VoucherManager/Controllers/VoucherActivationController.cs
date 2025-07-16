using Microsoft.AspNetCore.Mvc;

namespace VoucherManager.Controllers;

public class VoucherActivationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
