using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class BaseController : Controller
    {

        public void SetToken(User user)
        {
            HttpContext.Session.SetString("Token", user.Token);

        }

        public string GetToken()
        {
            return HttpContext.Session.GetString("Token");
        }

        public ActionResult HandleError(string error)
        {
            ViewBag.Message = error;
            return View("~/Views/Base/HandleError.cshtml");
        }


    }
}