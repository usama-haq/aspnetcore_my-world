using Microsoft.AspNetCore.Mvc;
using MyWorld.Services;
using MyWorld.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }

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

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModel model)
        {
            _mailService.SendMail("usamahaq.5533@gmail.com", model.Email, model.Name, model.Message);
            return View();
        }

        // GET: /<controller>/
        public IActionResult About()
        {
            return View();
        }
    }
}