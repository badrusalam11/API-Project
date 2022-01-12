using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<AccountRole, RoleRepository, int>
    {
        private RoleRepository roleRepository;
        public RolesController(RoleRepository roleRepository) : base(roleRepository)
        {
            this.roleRepository = roleRepository;
        }


        [Authorize(Roles = "Director")]
        [HttpPost]
        [Route("AssignManager")]
        public ActionResult AssignManager(AccountRole accountRole)
        {
            int assign = roleRepository.AssignManager(accountRole);
            if (assign == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, assign, message = $"User with NIK {accountRole.NIK} was assigned to Manager" });
            }
            else if (assign == 0)
            {
                return Ok(new { status = HttpStatusCode.NotFound, assign, message = "Error NIK not found" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.BadRequest, assign, message = "User already assign to Manager" });
            }
        }
    }
}
