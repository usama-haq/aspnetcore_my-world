using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWorld.Controllers.Web
{
    public class AppController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Contact()
            
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult About()
        {
            return View();
        }
    }
}