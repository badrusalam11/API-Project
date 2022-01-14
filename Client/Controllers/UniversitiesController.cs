using API.Models;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        private UniversityRepository repository;
        public UniversitiesController(UniversityRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
