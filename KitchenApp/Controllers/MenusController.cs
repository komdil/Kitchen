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
    public class MenusController : Controller
    {
        KitchenAppContext db;
        public MenusController(KitchenAppContext context)
        {
            db = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Menu> Get()
        {
            return db.Menus.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Menu menu = db.Menus.FirstOrDefault(a => a.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(menu);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Menu menu)
        {
            if (menu == null)
            {
                return BadRequest();
            }
            db.Menus.Add(menu);
            db.SaveChanges();
            return Ok(menu);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Menu menu)
        {
            if (menu == null)
            {
                return BadRequest();
            }
            if (!db.Menus.Any(x => x.Id == menu.Id))
            {
                return NotFound();
            }
            db.Update(menu);
            db.SaveChanges();
            return Ok(menu);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Menu menu = db.Menus.FirstOrDefault(x => x.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            db.Menus.Remove(menu);
            db.SaveChanges();
            return Ok(menu);
        }
    }
}
