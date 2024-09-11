using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Interfaces;
using Medical_CRM_Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Medical_CRM_Infrastructure.Repositories
{
    public class ProcedureProductRepository : GenericRepository<ProcedureProduct>, IProcedureProductRepository
    {
        public ProcedureProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProcedureProduct>> GetByProcedureIdAsync(Guid procedureId)
        {
            return await dbSet.Where(pp => pp.ProcedureId == procedureId).ToListAsync();
        }

        public async Task<ProcedureProduct> GetByProcedureProductIdAsync(string procedureProductId)
        {
            return await dbSet.FirstOrDefaultAsync(pp => pp.ProcedureProductId == procedureProductId);
        }

        public async Task RemoveRangeAsync(IEnumerable<ProcedureProduct> procedureProducts)
        {
            dbSet.RemoveRange(procedureProducts);
            await _context.SaveChangesAsync();
        }
    }
}
