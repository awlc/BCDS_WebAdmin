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
    public class ContractsController : ControllerBase
    {
        private readonly bdcswebContext _context;

        public ContractsController(bdcswebContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public IEnumerable<Contracts> GetContracts()
        {
            return _context.Contracts;
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContracts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contracts = await _context.Contracts.FindAsync(id);

            if (contracts == null)
            {
                return NotFound();
            }

            return Ok(contracts);
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContracts([FromRoute] int id, [FromBody] Contracts contracts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contracts.ContractId)
            {
                return BadRequest();
            }

            _context.Entry(contracts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractsExists(id))
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

        // POST: api/Contracts
        [HttpPost]
        public async Task<IActionResult> PostContracts([FromBody] Contracts contracts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contracts.Add(contracts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContracts", new { id = contracts.ContractId }, contracts);
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContracts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contracts = await _context.Contracts.FindAsync(id);
            if (contracts == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(contracts);
            await _context.SaveChangesAsync();

            return Ok(contracts);
        }

        private bool ContractsExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractId == id);
        }
    }
}