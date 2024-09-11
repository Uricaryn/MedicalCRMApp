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
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(Guid patientId)
        {
            return await GetAllAsync(a => a.PatientId == patientId, includeProperties: "Patient");
        }

        public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(DateTime currentDate)
        {
            return await GetAllAsync(a => a.Date >= currentDate, includeProperties: "Patient");
        }
    }
}
