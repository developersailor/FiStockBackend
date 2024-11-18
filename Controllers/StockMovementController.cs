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
    public class StockMovementController : ControllerBase
    {
        private readonly StockTrackingDbContext _context;

        public StockMovementController(StockTrackingDbContext context)
        {
            _context = context;
        }

        // GET: api/StockMovement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockMovementDTO>>> GetStockMovements()
        {
            return await _context.StockMovements
                .Select(sm => new StockMovementDTO
                {
                    StockMovementId = sm.StockMovementId,
                    TransactionDate = sm.TransactionDate,
                    ProductId = sm.ProductId,
                    Quantity = sm.Quantity,
                    UnitPrice = sm.UnitPrice,
                    TotalAmount = sm.TotalAmount,
                    SourceDestinationId = sm.SourceDestinationId,
                    Description = sm.Description,
                    TransactionType = sm.TransactionType
                })
                .ToListAsync();
        }

        // GET: api/StockMovement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockMovementDTO>> GetStockMovement(int id)
        {
            var stockMovement = await _context.StockMovements
                .Select(sm => new StockMovementDTO
                {
                    StockMovementId = sm.StockMovementId,
                    TransactionDate = sm.TransactionDate,
                    ProductId = sm.ProductId,
                    Quantity = sm.Quantity,
                    UnitPrice = sm.UnitPrice,
                    TotalAmount = sm.TotalAmount,
                    SourceDestinationId = sm.SourceDestinationId,
                    Description = sm.Description,
                    TransactionType = sm.TransactionType
                })
                .FirstOrDefaultAsync(sm => sm.StockMovementId == id);

            if (stockMovement == null)
            {
                return NotFound();
            }

            return stockMovement;
        }

        // PUT: api/StockMovement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockMovement(int id, StockMovementDTO stockMovementDto)
        {
            if (id != stockMovementDto.StockMovementId)
            {
                return BadRequest();
            }

            // Validate ProductId
            if (!await _context.Products.AnyAsync(p => p.ProductId == stockMovementDto.ProductId))
            {
                return BadRequest("Invalid ProductId");
            }

            // Validate SourceDestinationId
            if (!await _context.Warehouses.AnyAsync(w => w.WarehouseId == stockMovementDto.SourceDestinationId))
            {
                return BadRequest("Invalid SourceDestinationId");
            }

            var stockMovement = await _context.StockMovements.FindAsync(id);
            if (stockMovement == null)
            {
                return NotFound();
            }

            stockMovement.TransactionDate = stockMovementDto.TransactionDate;
            stockMovement.ProductId = stockMovementDto.ProductId;
            stockMovement.Quantity = stockMovementDto.Quantity;
            stockMovement.UnitPrice = stockMovementDto.UnitPrice;
            stockMovement.TotalAmount = stockMovementDto.TotalAmount;
            stockMovement.SourceDestinationId = stockMovementDto.SourceDestinationId;
            stockMovement.Description = stockMovementDto.Description;
            stockMovement.TransactionType = stockMovementDto.TransactionType;

            _context.Entry(stockMovement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockMovementExists(id))
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

        // POST: api/StockMovement
        [HttpPost]
        public async Task<ActionResult<StockMovementDTO>> PostStockMovement(StockMovementDTO stockMovementDto)
        {
            // Validate ProductId
            if (!await _context.Products.AnyAsync(p => p.ProductId == stockMovementDto.ProductId))
            {
                return BadRequest("Invalid ProductId");
            }

            // Validate SourceDestinationId
            if (!await _context.Warehouses.AnyAsync(w => w.WarehouseId == stockMovementDto.SourceDestinationId))
            {
                return BadRequest("Invalid SourceDestinationId");
            }

            var stockMovement = new StockMovement
            {
                TransactionDate = stockMovementDto.TransactionDate,
                ProductId = stockMovementDto.ProductId,
                Quantity = stockMovementDto.Quantity,
                UnitPrice = stockMovementDto.UnitPrice,
                TotalAmount = stockMovementDto.TotalAmount,
                SourceDestinationId = stockMovementDto.SourceDestinationId,
                Description = stockMovementDto.Description,
                TransactionType = stockMovementDto.TransactionType
            };

            _context.StockMovements.Add(stockMovement);
            await _context.SaveChangesAsync();

            stockMovementDto.StockMovementId = stockMovement.StockMovementId;

            return CreatedAtAction(nameof(GetStockMovement), new { id = stockMovementDto.StockMovementId }, stockMovementDto);
        }

        // DELETE: api/StockMovement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockMovement(int id)
        {
            var stockMovement = await _context.StockMovements.FindAsync(id);
            if (stockMovement == null)
            {
                return NotFound();
            }

            _context.StockMovements.Remove(stockMovement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockMovementExists(int id)
        {
            return _context.StockMovements.Any(e => e.StockMovementId == id);
        }
    }
}