using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.ProcedureProductDTOs;
using Medical_CRM_Domain.Entities;

namespace Medical_CRM_Domain.Services
{
    public interface IProcedureProductService
    {

        Task<IEnumerable<ProcedureProduct>> GetByProcedureIdAsync(Guid procedureId);
        Task AddProcedureProductAsync(ProcedureProduct procedureProduct);
        Task UpdateProcedureProductAsync(ProcedureProduct procedureProduct);
        Task DeleteProcedureProductAsync(ProcedureProduct procedureProduct);
        Task<ProcedureProductDto> GetByProcedureProductIdAsync(string procedureProductId);
    }
}
