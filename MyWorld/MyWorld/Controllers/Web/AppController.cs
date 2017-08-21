using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyWorld.Models;
using MyWorld.Services;
using MyWorld.ViewModels;
using System;

namespace MyWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository, ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                //var data = _repository.GetAllTrips();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"-- Exception in Index() of AppController: {ex.Message}");
                return Redirect("/error");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                var data = _repository.GetAllTrips();
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"-- Exception in Trips() of AppController: {ex.Message}");
                return Redirect("/error");
            }
        }

        // GET: /<controller>/
        [HttpGet]
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
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}