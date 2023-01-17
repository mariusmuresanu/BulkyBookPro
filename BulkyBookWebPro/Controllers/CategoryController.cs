using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWebPro.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
