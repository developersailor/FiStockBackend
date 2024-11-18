using FiStockBackend.Models;
using FiStockBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiStockBackend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StockMovementController : ControllerBase
{
    private readonly IStockMovementService _stockMovementService;

    public StockMovementController(IStockMovementService stockMovementService)
    {
        _stockMovementService = stockMovementService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockMovementById(int id)
    {
        var stockMovement = await _stockMovementService.GetStockMovementByIdAsync(id);
        if (stockMovement == null)
        {
            return NotFound();
        }
        return Ok(stockMovement);
    }

    [HttpPost]
    public async Task<IActionResult> AddStockMovement([FromBody] StockMovement stockMovement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var addedStockMovement = await _stockMovementService.AddStockMovementAsync(stockMovement);
        if (addedStockMovement == null)
        {
            return BadRequest("Failed to add stock movement.");
        }
        return CreatedAtAction(nameof(GetStockMovementById), new { id = addedStockMovement.StockMovementId }, addedStockMovement);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStockMovement(int id, [FromBody] StockMovement stockMovement)
    {
        var updatedStockMovement = await _stockMovementService.UpdateStockMovementAsync(id, stockMovement);
        if (updatedStockMovement == null)
        {
            return NotFound();
        }
        return Ok(updatedStockMovement);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockMovement(int id)
    {
        var success = await _stockMovementService.DeleteStockMovementAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}