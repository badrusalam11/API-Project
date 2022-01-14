using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var register = employeeRepository.Register(registerVM);

            if (register > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, register, message = "Register Success" });
                //return Ok(register);

            }
            else if (register == -2)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, register, message = "Error Employee NIK duplicate" });
            }
            else if (register == -3)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, register, message = "Error Employee Phone Number duplicate" });
            }
            else if (register == -4)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, register, message = "Error Employee Email duplicate" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.BadRequest, register, message = "Unknown error" });
            }

            
            //return BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Register Error" });
        }


        [HttpGet]
        //[Authorize(Roles = "Director, Manager")]
        [Route("Getregisterdata")]
        public ActionResult<Object> GetRegisterData()
        {
            var result = employeeRepository.GetRegisterData();
            if (result != null)
            {
               //return Ok(new { status = HttpStatusCode.OK, result, message="Data loaded" });
               return Ok(result);
            }
            return Ok(new { status = HttpStatusCode.NotFound, message = "Error data not found" });

        }

        [HttpGet]
        [Route("Getregisterdata/{NIK}")]
        public ActionResult<Object> GetRegisterData(string NIK)
        {
            var result = employeeRepository.GetRegisterData(NIK);
            if (result != null)
            {
                //return Ok(new { status = HttpStatusCode.OK, result, message = "Data loaded" });
                return Ok(result);
            }
            return Ok(new { status = HttpStatusCode.NotFound, message = "Error data not found" });

        }

        [HttpPut]
        [Route("UpdateRegisterData")]
        public ActionResult UpdateRegisterData(RegisterVM registerVM)
        {
            int result = employeeRepository.UpdateRegisterData(registerVM);
            if (result > 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Employee data Updated succefully" });
            }
            else if (result == -3)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result, message = "Error Employee Phone Number duplicate" });
            }
            else if (result == -4)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result, message = "Error Employee Email duplicate" });
            }
            else if (result == -2)
            {
                return Ok(new { status = HttpStatusCode.NotFound, result, message = "Employee not found" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.BadRequest, result, message = "Unknown error" });
            }

        }



        // Update data
        [HttpPut]
        [Route("update")]
        public ActionResult Update(Employee employee)
        {
            //try
            //{
                int check = employeeRepository.CheckEmailPhone(employee);
                if (check == 1)
                {
                     repository.Update(employee);
                     return Ok(new { status = HttpStatusCode.OK, check, message = "Employee data Updated succefully" });
                }
                else if (check == 3)
                {
                    return Ok(new { status = HttpStatusCode.BadRequest, check, message = "Error Employee Phone Number duplicate" });
                }
                else if (check == 4)
                {
                    return Ok(new { status = HttpStatusCode.BadRequest, check, message = "Error Employee Email duplicate" });
                }
                else if (check == 0)
                {
                    return Ok(new { status = HttpStatusCode.BadRequest, check, message = "Unknown error" });
                }
                else
                {
                    return Ok(new { status = HttpStatusCode.NotFound, check, message = "Employee not found" });
                }


            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError,
            //        "Error updating employee record");
            //}
            //return Ok(employeeRepository.Update(employee));
        }

        [HttpDelete]
        [Route("DeleteRegisterData/{NIK}")]
        public ActionResult DeleteRegisterData(string NIK)
        {
            var find = repository.Get(NIK);
            if (find == null)
            {
                return Ok(new { status = HttpStatusCode.NotFound, find, message = $"Error data with key = {NIK} not found" });
            }
            else
            {
                int delete;
                employeeRepository.DeleteRegisterData(NIK);
                delete = repository.Delete(NIK);
                return Ok(new { status = HttpStatusCode.OK, delete, message = $"Data with Key = {NIK} deleted succefully" });
            }
        }


        [HttpPost]
        [Route("Input")]
        public ActionResult Input(Employee employee)
        {
            var check = employeeRepository.CheckInsert(employee);

            if (check > 0)
            {
                repository.Insert(employee);
                return Ok(new { status = HttpStatusCode.OK, check, message = "Register Success" });

            }
            else if (check == -2)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, check, message = "Error Employee NIK duplicate" });
            }
            else if (check == -3)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, check, message = "Error Employee Phone Number duplicate" });
            }
            else if (check == -4)
            {
                return Ok(new { status = HttpStatusCode.BadRequest, check, message = "Error Employee Email duplicate" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.BadRequest, check, message = "Unknown error" });
            }


            //return BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Register Error" });
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil");
        }


    }
}

