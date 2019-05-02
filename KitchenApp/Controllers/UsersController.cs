using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KitchenApp.DateProvider;
using KitchenApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KitchenApp.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        MainContext db;
        public UsersController(MainContext context)
        {
            db = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return db.Users.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            User user = db.Users.FirstOrDefault(a => a.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(user);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            db.Users.Add(user);
            db.SaveChanges();
            return Ok(user);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]User user)
        {
            if (User == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }
            db.Update(user);
            db.SaveChanges();
            return Ok(user);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(user);
        }
    }
}
