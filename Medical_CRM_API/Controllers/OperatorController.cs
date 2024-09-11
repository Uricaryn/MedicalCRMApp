using Medical_CRM_Domain.DTOs.OperatorDTOs;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CRM_API.Controllers
{
    namespace Medical_CRM.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OperatorController : ControllerBase
        {
            private readonly IOperatorService _operatorService;

            public OperatorController(IOperatorService operatorService)
            {
                _operatorService = operatorService;
            }

            // GET: api/operator
            [HttpGet("All")]
            public async Task<IActionResult> GetAllOperators()
            {
                try
                {
                    var operators = await _operatorService.GetAllOperatorsAsync();
                    return Ok(operators);
                }
                catch (Exception ex)
                {
                    // Log exception if a logging mechanism is available
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // GET: api/operator/{id}
            [HttpGet("Find/{id}")]
            public async Task<IActionResult> GetOperatorById(Guid id)
            {
                try
                {
                    var operatorDto = await _operatorService.GetOperatorByIdAsync(id);
                    if (operatorDto == null)
                        return NotFound("Operator not found.");

                    return Ok(operatorDto);
                }
                catch (KeyNotFoundException knfEx)
                {
                    return NotFound(knfEx.Message);
                }
                catch (Exception ex)
                {
                    // Log exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // POST: api/operator
            [HttpPost("Create")]
            public async Task<IActionResult> CreateOperator([FromBody] OperatorCreateDto operatorCreateDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    await _operatorService.CreateOperatorAsync(operatorCreateDto);
                    return CreatedAtAction(nameof(GetOperatorById), new { id = operatorCreateDto.OperatorCode }, operatorCreateDto);
                }
                catch (ArgumentException argEx)
                {
                    return BadRequest(argEx.Message);
                }
                catch (Exception ex)
                {
                    // Log exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // PUT: api/operator/{id}
            [HttpPut("Edit/{id}")]
            public async Task<IActionResult> UpdateOperator(Guid id, [FromBody] OperatorUpdateDto operatorUpdateDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                try
                {
                    await _operatorService.UpdateOperatorAsync(id, operatorUpdateDto);
                    return NoContent();
                }
                catch (ArgumentException argEx)
                {
                    return BadRequest(argEx.Message);
                }
                catch (KeyNotFoundException knfEx)
                {
                    return NotFound(knfEx.Message);
                }
                catch (Exception ex)
                {
                    // Log exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // DELETE: api/operator/{id}
            [HttpDelete("Delete/{id}")]
            public async Task<IActionResult> DeleteOperator(Guid id)
            {
                try
                {
                    await _operatorService.DeleteOperatorAsync(id);
                    return NoContent();
                }
                catch (KeyNotFoundException knfEx)
                {
                    return NotFound(knfEx.Message);
                }
                catch (Exception ex)
                {
                    // Log exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }
}

