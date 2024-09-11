using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Enums;

namespace Medical_CRM_Domain.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        // Hastanın randevuları ile birlikte alınması
        Task<Patient> GetPatientWithAppointmentsAsync(Guid patientId);

        // Hastanın işlemleri ile birlikte alınması
        Task<Patient> GetPatientWithProceduresAsync(Guid patientId);

        // Belirli bir işlem türüne göre hastaların listelenmesi
        Task<IEnumerable<Patient>> GetPatientsByProcedureTypeAsync(ProcedureType procedureType);

        // Ödemesi tamamlanan hastaların listelenmesi
        Task<IEnumerable<Patient>> GetPatientsWithCompletedPaymentsAsync();

        // Ödemesi tamamlanmayan hastaların listelenmesi
        Task<IEnumerable<Patient>> GetPatientsWithPendingPaymentsAsync();
    }
}
