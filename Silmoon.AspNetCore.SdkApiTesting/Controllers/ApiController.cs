using Microsoft.AspNetCore.Mvc;

namespace Silmoon.AspNetCore.SdkApiTesting.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
