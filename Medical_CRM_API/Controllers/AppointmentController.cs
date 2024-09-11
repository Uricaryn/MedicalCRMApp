using Medical_CRM_Domain.DTOs.AppointmentDTOs;
using Medical_CRM_Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CRM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            
        }

        // GET: api/Appointment
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        // GET: api/Appointment/{id}
        [HttpGet("Find/{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        // POST: api/Appointment/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreateDto appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _appointmentService.CreateAppointmentAsync(appointmentDto);
            return Ok(new { Message = "Appointment created successfully" });
        }

        // PUT: api/Appointment/{id}
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] AppointmentUpdateDto appointmentDto)
        {
            if (appointmentDto == null || id != appointmentDto.Id || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid appointment data." });
            }

            var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (existingAppointment == null)
            {
                return NotFound(new { message = "Appointment not found for update." });
            }

            try
            {
                // Perform the update operation using the service layer
                await _appointmentService.UpdateAppointmentAsync(appointmentDto);
                return Ok(new { message = "Appointment updated successfully." });
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a detailed error message
                return StatusCode(500, new { message = $"Error updating appointment: {ex.Message}" });
            }
        }


        // DELETE: api/Appointment/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            try
            {
                // Check if the appointment exists before attempting to delete
                var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                {
                    return NotFound(new { message = "Appointment not found for deletion." });
                }

                // Perform the delete operation using the service layer
                await _appointmentService.DeleteAppointmentAsync(id);
                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a detailed error message
                return StatusCode(500, new { message = $"Error deleting appointment: {ex.Message}" });
            }
        }

    }
}
