using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;

namespace Medical_CRM_Domain.Interfaces
{
    public interface IProcedureProductRepository : IGenericRepository<ProcedureProduct>
    {
        Task<IEnumerable<ProcedureProduct>> GetByProcedureIdAsync(Guid procedureId);
        Task RemoveRangeAsync(IEnumerable<ProcedureProduct> procedureProducts);

        Task<ProcedureProduct> GetByProcedureProductIdAsync(string procedureProductId);
    }
}
