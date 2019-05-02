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
        MainContext context;
        public MenusController(MainContext context)
        {
            this.context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Menu> Get()
        {
            context.Menus.Add(new Menu());
            context.SaveChanges();
            return context.Menus.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Menu menu = context.Menus.FirstOrDefault(a => a.Id == id);
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
            context.Menus.Add(menu);
            context.SaveChanges();
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
            if (!context.Menus.Any(x => x.Id == menu.Id))
            {
                return NotFound();
            }
            context.Update(menu);
            context.SaveChanges();
            return Ok(menu);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Menu menu = context.Menus.FirstOrDefault(x => x.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            context.Menus.Remove(menu);
            context.SaveChanges();
            return Ok(menu);
        }
    }
}
