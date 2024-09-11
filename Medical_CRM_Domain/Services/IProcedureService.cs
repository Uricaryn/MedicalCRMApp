using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.ProcedureDTOs;

namespace Medical_CRM_Domain.Services
{
    public interface IProcedureService
    {
        Task<IEnumerable<ProcedureGetDto>> GetAllProceduresAsync();
        Task<IEnumerable<ProcedureGetDto>> GetProceduresByPatientIdAsync(Guid patientId);
        Task<ProcedureGetDto> GetProcedureByIdAsync(Guid id);
        Task CreateProcedureAsync(ProcedureCreateDto procedureCreateDto);
        Task UpdateProcedureAsync(ProcedureUpdateDto procedureUpdateDto);
        Task DeleteProcedureAsync(Guid id);
    }
}
