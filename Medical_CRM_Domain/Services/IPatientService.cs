using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.PatientDTOs;

namespace Medical_CRM_Domain.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientGetDto>> GetAllPatientsAsync();
        Task<PatientGetDto> GetPatientByIdAsync(Guid id);
        Task CreatePatientAsync(PatientCreateDto patientCreateDto);
        Task UpdatePatientAsync(PatientUpdateDto patientUpdateDto);
        Task DeletePatientAsync(Guid id);
    }
}
