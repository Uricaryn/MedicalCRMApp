using AutoMapper;
using Medical_CRM_Domain.DTOs.PatientDTOs;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_CRM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _patientService = patientService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Patient
        [HttpGet("All")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        // GET: api/Patient/{id}
        [HttpGet("Find/{id}")]
        public async Task<IActionResult> GetPatientById(Guid id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        // POST: api/Patient
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientCreateDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _patientService.CreatePatientAsync(patientDto); 
            return Ok(new { Message = "Patient created successfully" });
        }

        // PUT: api/Patient/{id}
        [HttpPut("Edit/{id}")]
        public async Task UpdatePatientAsync(PatientUpdateDto patientUpdateDto)
        {
            if (patientUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(patientUpdateDto), "Patient update data is required.");
            }

            var existingPatient = await _unitOfWork.Patients.GetByIdAsync(patientUpdateDto.Id);

           
            if (existingPatient == null)
            {
                throw new KeyNotFoundException("Patient not found for update.");
            }

            
            var patient = _mapper.Map(patientUpdateDto, existingPatient);

            try
            {
                await _unitOfWork.Patients.UpdateAsync(patient);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating patient: " + ex.Message);
            }
        }

        // DELETE: api/Patient/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            await _patientService.DeletePatientAsync(id);
            return NoContent();
        }
    }
}
