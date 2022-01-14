using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository accountRepository;
        private IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            int login = accountRepository.Login(loginVM);
            if (login == 1)
            {

                //JWT
                var getUserData = accountRepository.GetUserData(loginVM);
                //payload
                var claims = new List<Claim>
            {
                new Claim("Email", loginVM.Email)
                //new Claim('roles', g.role)
            };
                foreach (var g in getUserData)
                {
                    claims.Add(new Claim("roles", g.ToString()));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  //Header
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audiance"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                    );

                var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idtoken.ToString()));



                return Ok(new JWTokenVM{ status = HttpStatusCode.OK, idToken = idtoken, message = "Login Success" });
            }
            else if (login == 0)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, login, message = "Error User Not Found" });
            }
            else if (login == -1)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, login, message = "Wrong Password" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.BadRequest, login, message = "Login Failed" });
            }
        }

        [HttpPut]
        [Route("Forgot")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            int forgot = accountRepository.ForgotPassword(forgotPasswordVM);
            if (forgot == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, forgot, message = "OTP was Sended to your email" +
                    ", Please Check Your Email" });
            }
            else if (forgot == 0)
            {
                return Ok(new { status = HttpStatusCode.NotFound, forgot, message = "Error Email Not Found" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.BadRequest, forgot, message = "Request failed" });
            }
        }

        [HttpPut]
        [Route("changepassword")]
        public ActionResult ChangePassword(ForgotPasswordVM forgotPasswordVM)
        {
            int change = accountRepository.ChangePassword(forgotPasswordVM);
            if (change == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, change, message = "Password Has Been Changed" });
            }
            else if (change == 0)
            {
                return Ok(new { status = HttpStatusCode.NotFound, change, message = "Error Wrong OTP not found" });
            }
            else if (change == -1)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, change, message = "OTP Expired, try again" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.BadRequest, change, message = "OTP Already Used, try again" });
            }
        }

        


    }
}
