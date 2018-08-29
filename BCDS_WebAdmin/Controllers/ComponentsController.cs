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
    public class ComponentsController : ControllerBase
    {
        private readonly bdcswebContext _context;

        public ComponentsController(bdcswebContext context)
        {
            _context = context;
        }

        // GET: api/Components
        [HttpGet]
        public IEnumerable<Components> GetComponents()
        {
            return _context.Components;
        }

        // GET: api/Components/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var components = await _context.Components.FindAsync(id);

            if (components == null)
            {
                return NotFound();
            }

            return Ok(components);
        }

        // PUT: api/Components/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponents([FromRoute] int id, [FromBody] Components components)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != components.ComponentId)
            {
                return BadRequest();
            }

            _context.Entry(components).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentsExists(id))
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

        // POST: api/Components
        [HttpPost]
        public async Task<IActionResult> PostComponents([FromBody] Components components)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Components.Add(components);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponents", new { id = components.ComponentId }, components);
        }

        // DELETE: api/Components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var components = await _context.Components.FindAsync(id);
            if (components == null)
            {
                return NotFound();
            }

            _context.Components.Remove(components);
            await _context.SaveChangesAsync();

            return Ok(components);
        }

        private bool ComponentsExists(int id)
        {
            return _context.Components.Any(e => e.ComponentId == id);
        }

        [HttpGet("[action]")]
        public IEnumerable<Components> GetComponentsByContract(string contractNo, int? componentTypeId = null)
        {
            IEnumerable<Components> components = new List<Components>();

            if (!ModelState.IsValid)
            {
                return null;
            }

            var contract = _context.Contracts.Where(c => c.ContractNo == contractNo);
            if (!contract.Any())
            {
                return components;
            }

            var contractManufacturers = _context.ContractManufacturers.Where(c => c.ContractId == contract.First().ContractId);
            if (componentTypeId != null)
            {
                contractManufacturers = contractManufacturers.Where(c => c.ComponentTypeId == componentTypeId);
            }
            if (!contractManufacturers.Any())
            {
                return components;
            }

            var contractManufacturerIds = contractManufacturers.Select(c => c.ContractManufacturerId).ToList();
            components = _context.Components.Where(c => contractManufacturerIds.Contains(c.ContractManufacturerId));

            return components;
        }
    }
}