using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.AppointmentDTOs;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.UnitOfWork;

namespace Medical_CRM_Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentGetDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.Appointments.GetAllAsync();

           
            if (appointments == null || !appointments.Any())
            {
                return Enumerable.Empty<AppointmentGetDto>();
            }

            return _mapper.Map<IEnumerable<AppointmentGetDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentGetDto>> GetAppointmentsByPatientIdAsync(Guid patientId)
        {
            var appointments = await _unitOfWork.Appointments.GetAppointmentsByPatientIdAsync(patientId);

            
            if (appointments == null || !appointments.Any())
            {
                return Enumerable.Empty<AppointmentGetDto>();
            }

            return _mapper.Map<IEnumerable<AppointmentGetDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentGetDto>> GetUpcomingAppointmentsAsync(DateTime currentDate)
        {
            var appointments = await _unitOfWork.Appointments.GetUpcomingAppointmentsAsync(currentDate);

            
            if (appointments == null || !appointments.Any())
            {
                return Enumerable.Empty<AppointmentGetDto>();
            }

            return _mapper.Map<IEnumerable<AppointmentGetDto>>(appointments);
        }

        public async Task<AppointmentGetDto> GetAppointmentByIdAsync(Guid id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

            
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            return _mapper.Map<AppointmentGetDto>(appointment);
        }

        public async Task CreateAppointmentAsync(AppointmentCreateDto appointmentCreateDto)
        {
            
            if (appointmentCreateDto == null)
            {
                throw new ArgumentNullException(nameof(appointmentCreateDto), "Appointment data is required.");
            }

            var appointment = _mapper.Map<Appointment>(appointmentCreateDto);

            
            try
            {
                await _unitOfWork.Appointments.AddAsync(appointment);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating appointment: " + ex.Message);
            }
        }

        public async Task UpdateAppointmentAsync(AppointmentUpdateDto appointmentUpdateDto)
        {
            if (appointmentUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(appointmentUpdateDto), "Appointment update data is required.");
            }

            var existingAppointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentUpdateDto.Id);

            if (existingAppointment == null)
            {
                throw new KeyNotFoundException("Appointment not found for update.");
            }

            var appointment = _mapper.Map(appointmentUpdateDto, existingAppointment);

            try
            {
                await _unitOfWork.Appointments.UpdateAsync(appointment);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating appointment: " + ex.Message);
            }
        }

        public async Task DeleteAppointmentAsync(Guid id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found for deletion.");
            }

            try
            {
                await _unitOfWork.Appointments.DeleteAsync(appointment);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting appointment: " + ex.Message);
            }
        }
    }
}
