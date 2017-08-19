using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWorld.Services;
using MyWorld.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
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
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("", "We don't support AOL addresses.");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, model.Name, model.Message);

                ModelState.Clear();

                ViewBag.Message = "Message Sent!";
            }
            return View();
        }

        // GET: /<controller>/
        public IActionResult About()
        {
            return View();
        }
    }
}