using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCDS_WebAdmin.Models;

namespace BCDS_WebAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentTypesController : ControllerBase
    {
        private readonly bdcswebContext _context;

        public ComponentTypesController(bdcswebContext context)
        {
            _context = context;
        }

        // GET: api/ComponentTypes
        [HttpGet]
        public IEnumerable<ComponentTypes> GetComponentTypes()
        {
            return _context.ComponentTypes;
        }

        // GET: api/ComponentTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponentTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var componentTypes = await _context.ComponentTypes.FindAsync(id);

            if (componentTypes == null)
            {
                return NotFound();
            }

            return Ok(componentTypes);
        }

        // PUT: api/ComponentTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentTypes([FromRoute] int id, [FromBody] ComponentTypes componentTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != componentTypes.ComponentTypeId)
            {
                return BadRequest();
            }

            _context.Entry(componentTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentTypesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ComponentTypes
        [HttpPost]
        public async Task<IActionResult> PostComponentTypes([FromBody] ComponentTypes componentTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ComponentTypes.Add(componentTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponentTypes", new { id = componentTypes.ComponentTypeId }, componentTypes);
        }

        // DELETE: api/ComponentTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponentTypes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var componentTypes = await _context.ComponentTypes.FindAsync(id);
            if (componentTypes == null)
            {
                return NotFound();
            }

            _context.ComponentTypes.Remove(componentTypes);
            await _context.SaveChangesAsync();

            return Ok(componentTypes);
        }

        private bool ComponentTypesExists(int id)
        {
            return _context.ComponentTypes.Any(e => e.ComponentTypeId == id);
        }
    }
}