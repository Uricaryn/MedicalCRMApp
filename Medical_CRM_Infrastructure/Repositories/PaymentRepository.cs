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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByPatientIdAsync(Guid patientId)
        {
            return await GetAllAsync(p => p.PatientId == patientId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByProcedureIdAsync(Guid procedureId)
        {
            return await GetAllAsync(p => p.ProcedureId == procedureId);
        }
    }
}
