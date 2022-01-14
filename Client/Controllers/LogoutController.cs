using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LogoutController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Authorize]
        [HttpGet("Logout/")]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
    }
}
