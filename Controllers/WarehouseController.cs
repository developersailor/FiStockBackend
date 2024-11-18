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
    public class WarehouseController : ControllerBase
    {
        private readonly StockTrackingDbContext _context;

        public WarehouseController(StockTrackingDbContext context)
        {
            _context = context;
        }

        // GET: api/Warehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseDTO>>> GetWarehouses()
        {
            return await _context.Warehouses
                .Select(w => new WarehouseDTO
                {
                    WarehouseId = w.WarehouseId,
                    WarehouseName = w.WarehouseName,
                    Address = w.Address,
                    ResponsiblePerson = w.ResponsiblePerson,
                    WarehouseCode = w.WarehouseCode
                })
                .ToListAsync();
        }

        // GET: api/Warehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseDTO>> GetWarehouse(int id)
        {
            var warehouse = await _context.Warehouses
                .Select(w => new WarehouseDTO
                {
                    WarehouseId = w.WarehouseId,
                    WarehouseName = w.WarehouseName,
                    Address = w.Address,
                    ResponsiblePerson = w.ResponsiblePerson,
                    WarehouseCode = w.WarehouseCode
                })
                .FirstOrDefaultAsync(w => w.WarehouseId == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return warehouse;
        }

        // PUT: api/Warehouse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, WarehouseDTO warehouseDto)
        {
            if (id != warehouseDto.WarehouseId)
            {
                return BadRequest();
            }

            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            warehouse.WarehouseName = warehouseDto.WarehouseName;
            warehouse.Address = warehouseDto.Address;
            warehouse.ResponsiblePerson = warehouseDto.ResponsiblePerson;
            warehouse.WarehouseCode = warehouseDto.WarehouseCode;

            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
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

        // POST: api/Warehouse
        [HttpPost]
        public async Task<ActionResult<WarehouseDTO>> PostWarehouse(WarehouseDTO warehouseDto)
        {
            var warehouse = new Warehouse
            {
                WarehouseName = warehouseDto.WarehouseName,
                Address = warehouseDto.Address,
                ResponsiblePerson = warehouseDto.ResponsiblePerson,
                WarehouseCode = warehouseDto.WarehouseCode
            };

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();

            warehouseDto.WarehouseId = warehouse.WarehouseId;

            return CreatedAtAction("GetWarehouse", new { id = warehouse.WarehouseId }, warehouseDto);
        }

        // DELETE: api/Warehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WarehouseExists(int id)
        {
            return _context.Warehouses.Any(e => e.WarehouseId == id);
        }
    }
}