using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;

namespace Medical_CRM_Domain.Interfaces
{
    public interface IProcedureRepository : IGenericRepository<Procedure>
    {
        Task<IEnumerable<Procedure>> GetProceduresByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Procedure>> GetProceduresWithProductsAsync(Guid procedureId);

        IQueryable<Procedure> GetQueryable();
    }
}
