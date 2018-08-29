using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCDS_WebAdmin.Models;
using Microsoft.Extensions.Configuration;

namespace BCDS_WebAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentItemsController : ControllerBase
    {
        private readonly bdcswebContext _context;
        private readonly IConfiguration _configuration;

        private readonly int componentItemsPageLimit;

        public ComponentItemsController(bdcswebContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            componentItemsPageLimit = _configuration.GetValue<int>("UISettings:ComponentItemsPageLimit");
        }

        // GET: api/ComponentItems
        [HttpGet]
        public IEnumerable<ComponentItems> GetComponentItems()
        {
            return _context.ComponentItems;
        }

        // GET: api/ComponentItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponentItems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var componentItems = await _context.ComponentItems.FindAsync(id);

            if (componentItems == null)
            {
                return NotFound();
            }

            return Ok(componentItems);
        }

        // PUT: api/ComponentItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentItems([FromRoute] int id, [FromBody] ComponentItems componentItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != componentItems.ComponentItemId)
            {
                return BadRequest();
            }

            _context.Entry(componentItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentItemsExists(id))
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

        // POST: api/ComponentItems
        [HttpPost]
        public async Task<IActionResult> PostComponentItems([FromBody] ComponentItems componentItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ComponentItems.Add(componentItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponentItems", new { id = componentItems.ComponentItemId }, componentItems);
        }

        // DELETE: api/ComponentItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponentItems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var componentItems = await _context.ComponentItems.FindAsync(id);
            if (componentItems == null)
            {
                return NotFound();
            }

            _context.ComponentItems.Remove(componentItems);
            await _context.SaveChangesAsync();

            return Ok(componentItems);
        }

        private bool ComponentItemsExists(int id)
        {
            return _context.ComponentItems.Any(e => e.ComponentItemId == id);
        }

        [HttpGet("[action]")]
        public IActionResult GetComponentItemsByContract(string contractNo, int? componentTypeId = null, int? pageNum = 1, bool getTotalPages = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var components = new ComponentsController(_context).GetComponentsByContract(contractNo, componentTypeId);
            var componentIds = components.Select(c => c.ComponentId).ToList();

            if (getTotalPages)
            {
                var componentItems = _context.ComponentItems.Where(ci => componentIds.Contains(ci.ComponentId));
                return Ok(componentItems.Count() / componentItemsPageLimit);
            }
            else
            {
                pageNum--;
                var componentItems = _context.ComponentItems.Where(ci => componentIds.Contains(ci.ComponentId)).Skip((int)pageNum * componentItemsPageLimit).Take(componentItemsPageLimit);
                return Ok(componentItems);
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetComponentItemsByTagId(string tagId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_context.ComponentItems.Where(ci => ci.TagId == tagId));
        }
    }
}