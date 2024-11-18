using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiStockBackend.Context;
using FiStockBackend.Models;
using FiStockBackend.DTO;

namespace FiStockBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotController : ControllerBase
    {
        private readonly StockTrackingDbContext _context;

        public LotController(StockTrackingDbContext context)
        {
            _context = context;
        }

        // GET: api/Lot
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LotDTO>>> GetLots()
        {
            return await _context.Lots
                .Select(l => new LotDTO
                {
                    LotId = l.LotId,
                    LotNumber = l.LotNumber,
                    ExpiryDate = l.ExpiryDate,
                    ProductId = l.ProductId
                })
                .ToListAsync();
        }

        // GET: api/Lot/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LotDTO>> GetLot(int id)
        {
            var lot = await _context.Lots
                .Select(l => new LotDTO
                {
                    LotId = l.LotId,
                    LotNumber = l.LotNumber,
                    ExpiryDate = l.ExpiryDate,
                    ProductId = l.ProductId
                })
                .FirstOrDefaultAsync(l => l.LotId == id);

            if (lot == null)
            {
                return NotFound();
            }

            return lot;
        }

        // PUT: api/Lot/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLot(int id, LotDTO lotDto)
        {
            if (id != lotDto.LotId)
            {
                return BadRequest();
            }

            // Validate ProductId
            if (!await _context.Products.AnyAsync(p => p.ProductId == lotDto.ProductId))
            {
                return BadRequest("Invalid ProductId");
            }

            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }

            lot.LotNumber = lotDto.LotNumber;
            lot.ExpiryDate = lotDto.ExpiryDate;
            lot.ProductId = lotDto.ProductId;

            _context.Entry(lot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LotExists(id))
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

        // POST: api/Lot
        [HttpPost]
        public async Task<ActionResult<LotDTO>> PostLot(LotDTO lotDto)
        {
            // Validate ProductId
            if (!await _context.Products.AnyAsync(p => p.ProductId == lotDto.ProductId))
            {
                return BadRequest("Invalid ProductId");
            }

            var lot = new Lot
            {
                LotNumber = lotDto.LotNumber,
                ExpiryDate = lotDto.ExpiryDate,
                ProductId = lotDto.ProductId
            };

            _context.Lots.Add(lot);
            await _context.SaveChangesAsync();

            lotDto.LotId = lot.LotId;

            return CreatedAtAction("GetLot", new { id = lot.LotId }, lotDto);
        }

        // DELETE: api/Lot/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLot(int id)
        {
            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }

            _context.Lots.Remove(lot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LotExists(int id)
        {
            return _context.Lots.Any(e => e.LotId == id);
        }
    }
}