using Medical_CRM_Domain.DTOs.ProcedureDTOs;
using Medical_CRM_Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CRM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureService _procedureService;

        public ProcedureController(IProcedureService procedureService)
        {
            _procedureService = procedureService;
        }

        // GET: api/Procedure
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProcedures()
        {
            var procedures = await _procedureService.GetAllProceduresAsync();
            return Ok(procedures);
        }

        // GET: api/Procedure/{id}
        [HttpGet("Find/{id}")]
        public async Task<IActionResult> GetProcedureById(Guid id)
        {
            var procedure = await _procedureService.GetProcedureByIdAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }
            return Ok(procedure);
        }

        // ProcedureController.cs
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProcedure([FromBody] ProcedureCreateDto procedureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Debugging step: Check if products are being received
            if (procedureDto.Products == null)
            {
                return BadRequest("Products list is missing.");
            }

            if (!procedureDto.Products.Any())
            {
                return BadRequest("Products list is empty. At least one product is required.");
            }

            await _procedureService.CreateProcedureAsync(procedureDto);
            return Ok(new { Message = "Procedure created successfully" });
        }

        // PUT: api/Procedure/{id}
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateProcedure(Guid id, [FromBody] ProcedureUpdateDto procedureDto)
        {
            if (id != procedureDto.Id || !ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                // `procedureDto.Id`'yi güncellemeye çalışmadan, var olan kaydı bulmak için kullanıyoruz
                await _procedureService.UpdateProcedureAsync(procedureDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the procedure: {ex.Message}");
            }
        }

        // DELETE: api/Procedure/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProcedure(Guid id)
        {
            await _procedureService.DeleteProcedureAsync(id);
            return NoContent();
        }
    }
}
