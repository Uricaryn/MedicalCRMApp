using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Enums;
using Medical_CRM_Domain.Interfaces;
using Medical_CRM_Infrastructure.Context;

namespace Medical_CRM_Infrastructure.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Patient> GetPatientWithAppointmentsAsync(Guid patientId)
        {
            return await GetAsync(p => p.Id == patientId, includeProperties: "Appointments");
        }

        public async Task<Patient> GetPatientWithProceduresAsync(Guid patientId)
        {
            return await GetAsync(p => p.Id == patientId, includeProperties: "Procedures");
        }

        public async Task<IEnumerable<Patient>> GetPatientsByProcedureTypeAsync(ProcedureType procedureType)
        {
            return await GetAllAsync(p => p.Procedures.Any(pr => pr.ProcedureType == procedureType), includeProperties: "Procedures");
        }

        public async Task<IEnumerable<Patient>> GetPatientsWithCompletedPaymentsAsync()
        {
            return await GetAllAsync(
                p => p.Procedures.All(pr => pr.Payments != null && pr.Payments.All(pay => pay.Amount > 0)),
                includeProperties: "Procedures,Procedures.Payments");
        }

        public async Task<IEnumerable<Patient>> GetPatientsWithPendingPaymentsAsync()
        {
            return await GetAllAsync(
                p => p.Procedures.Any(pr => pr.Payments == null || pr.Payments.Any(pay => pay.Amount <= 0)),
                includeProperties: "Procedures,Procedures.Payments");
        }
    }
}
