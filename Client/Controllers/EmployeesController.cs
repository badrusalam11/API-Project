using API.Models;
using API.ViewModels;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{


    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private EmployeeRepository repository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetRegisterData()
        {
            var result = await repository.GetRegisterData();
            return Json(result);
        }

        [HttpGet("Employees/GetRegisterDataNIK/{NIK}")]
        public async Task<JsonResult> GetRegisterDataNIK(string NIK)
        {
            var result = await repository.GetRegisterDataNIK(NIK);
            return Json(result);
        }

        [HttpPost("Employees/Register")]
        public JsonResult Register(RegisterVM registerVM)
        {
            var result = repository.Register(registerVM);
            return Json(result);
        }

        [HttpPut("Employees/UpdateRegisterData")]
        public JsonResult UpdateRegisterData(RegisterVM registerVM)
        {
            var result = repository.UpdateRegisterData(registerVM);
            return Json(result);
        }

        [HttpDelete("Employees/DeleteRegisterData/{NIK}")]
        public JsonResult DeleteRegisterData(string NIK)
        {
            var result = repository.DeleteRegisterData(NIK);
            return Json(result);
        }

    }
}
