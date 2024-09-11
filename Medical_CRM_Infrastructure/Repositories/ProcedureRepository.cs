using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Interfaces;
using Medical_CRM_Infrastructure.Context;

namespace Medical_CRM_Infrastructure.Repositories
{
    public class ProcedureRepository : GenericRepository<Procedure>, IProcedureRepository
    {
        public ProcedureRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Procedure>> GetProceduresByPatientIdAsync(Guid patientId)
        {
            return await GetAllAsync(
                pr => pr.PatientId == patientId,
                includeProperties: "ProcedureProducts,ProcedureProducts.Product");
        }

        public async Task<IEnumerable<Procedure>> GetProceduresWithProductsAsync(Guid procedureId)
        {
            return await GetAllAsync(
                pr => pr.Id == procedureId,
                includeProperties: "ProcedureProducts,ProcedureProducts.Product");
        }

        public IQueryable<Procedure> GetQueryable()
        {
            return _context.Procedures.AsQueryable();
        }
    }
}
