using Medical_CRM_Domain.DTOs.StockTransactionDTOs;
using Medical_CRM_Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CRM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockTransactionController : ControllerBase
    {
        private readonly IStockTransactionService _stockTransactionService;

        public StockTransactionController(IStockTransactionService stockTransactionService)
        {
            _stockTransactionService = stockTransactionService;
        }

        // GET: api/StockTransaction
        [HttpGet("All")]
        public async Task<IActionResult> GetAllStockTransactions()
        {
            var transactions = await _stockTransactionService.GetAllStockTransactionsAsync();
            return Ok(transactions);
        }

        // GET: api/StockTransaction/{id}
        [HttpGet("Find/{id}")]
        public async Task<IActionResult> GetStockTransactionById(Guid id)
        {
            var transaction = await _stockTransactionService.GetStockTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        // POST: api/StockTransaction
        [HttpPost("Create")]
        public async Task<IActionResult> CreateStockTransaction([FromBody] StockTransactionCreateDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _stockTransactionService.CreateStockTransactionAsync(transactionDto);
            return Ok(new { Message = "Stock transaction created successfully" });
        }

        // PUT: api/StockTransaction/{id}
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateStockTransaction(Guid id, [FromBody] StockTransactionUpdateDto transactionDto)
        {
            if (id != transactionDto.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _stockTransactionService.UpdateStockTransactionAsync(transactionDto);
            return NoContent();
        }

        // DELETE: api/StockTransaction/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteStockTransaction(Guid id)
        {
            await _stockTransactionService.DeleteStockTransactionAsync(id);
            return NoContent();
        }
    }
}
