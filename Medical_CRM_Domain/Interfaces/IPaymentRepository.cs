using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;

namespace Medical_CRM_Domain.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByPatientIdAsync(Guid patientId);
        Task<IEnumerable<Payment>> GetPaymentsByProcedureIdAsync(Guid procedureId);
    }
}
