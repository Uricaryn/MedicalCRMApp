using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.AppointmentDTOs;

namespace Medical_CRM_Domain.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentGetDto>> GetAllAppointmentsAsync();
        Task<IEnumerable<AppointmentGetDto>> GetAppointmentsByPatientIdAsync(Guid patientId);
        Task<IEnumerable<AppointmentGetDto>> GetUpcomingAppointmentsAsync(DateTime currentDate);
        Task<AppointmentGetDto> GetAppointmentByIdAsync(Guid id);
        Task CreateAppointmentAsync(AppointmentCreateDto appointmentCreateDto);
        Task UpdateAppointmentAsync(AppointmentUpdateDto appointmentUpdateDto);
        Task DeleteAppointmentAsync(Guid id);
    }
}
