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
    public class OperatorRepository : GenericRepository<Operator>, IOperatorRepository
    {
        private readonly ApplicationDbContext _context;

        public OperatorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Implement additional methods specific to the Operator entity
        public async Task<Operator> GetOperatorByCodeAsync(string operatorCode)
        {
            return await _context.Operators
                .FirstOrDefaultAsync(o => o.OperatorCode == operatorCode);
        }
    }
}
