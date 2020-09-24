using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            User user = new User();
            user.Username = userLogin.User;
            user.Password = userLogin.Password;
            var result = await Business.ApiClientManager.Login(user);

            if (result.Token == null)
            {
                TempData["Message"] = "Wrong user or password.";
                return RedirectToAction("Index", "Home");
            }
            user.Role = result.Role;
            user.Token = result.Token;
            user.FirstName= result.FirstName;
            user.LastName = result.LastName;
            user.Id = result.Id;
            //return View(result);
            SetToken(user);
            return RedirectToAction("Index", "Student");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
