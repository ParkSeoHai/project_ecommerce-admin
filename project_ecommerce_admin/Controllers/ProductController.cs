using Microsoft.AspNetCore.Mvc;

namespace project_ecommerce_admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
