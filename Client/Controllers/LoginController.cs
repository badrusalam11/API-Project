using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : BaseController<Account, LoginRepository, string>
    {

        private LoginRepository repository;

        public LoginController(LoginRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginVM loginVM)
        {
            var result = repository.Login(loginVM);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Auth(LoginVM loginVM)
        {
            var jwtToken = await repository.Auth(loginVM);
            var token = jwtToken.idToken;

            if (token == null)
            {
                return RedirectToAction("index", "Login");
            }

            HttpContext.Session.SetString("JWToken", token);
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            return RedirectToAction("index", "Admin");
        }


    }
}
