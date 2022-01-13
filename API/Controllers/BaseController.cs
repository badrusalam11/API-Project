using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {

        public Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        //Get all
        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            return Ok(result);
        }
        //Get by primary key
        [HttpGet("{key}")]
        public ActionResult<Entity> Get(Key key)
        {
            var get = repository.Get(key);
            if (get == null)
            {
                return Ok(new { status = HttpStatusCode.NotFound, get, message = $"{key} not found" });
            }
            return Ok(get);
        }
        //Insert data
        [HttpPost]
        public ActionResult<Entity> Insert(Entity entity)
        {
            int insert = repository.Insert(entity);
            return Ok(insert);
        }
        //Update data
        [HttpPut]
        public ActionResult<Entity> Put(Entity entity)
        {
            int update = repository.Update(entity);
            return Ok(update);
        }

        [HttpDelete("{key}")]
        public ActionResult<Entity> Delete(Key key)
        {
            var find = repository.Get(key);
            if (find == null)
            {
                return Ok(new { status = HttpStatusCode.NotFound, find, message = $"Error data with key = {key} not found" });
            }
            else
            {
                int delete = repository.Delete(key);
                return Ok(new { status = HttpStatusCode.OK, delete, message = $"Data with Key = {key} deleted succefully" });
            }
        }


    } 

}
