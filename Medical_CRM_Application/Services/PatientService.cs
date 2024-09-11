using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.PatientDTOs;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.UnitOfWork;

namespace Medical_CRM_Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientGetDto>> GetAllPatientsAsync()
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();

            // Eğer hasta bulunamazsa boş bir liste döner
            if (patients == null || !patients.Any())
            {
                return Enumerable.Empty<PatientGetDto>();
            }

            return _mapper.Map<IEnumerable<PatientGetDto>>(patients);
        }

        public async Task<PatientGetDto> GetPatientByIdAsync(Guid id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            // Eğer hasta bulunamazsa hata fırlatılır
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }

            return _mapper.Map<PatientGetDto>(patient);
        }

        public async Task CreatePatientAsync(PatientCreateDto patientCreateDto)
        {
            // Giriş doğrulaması
            if (patientCreateDto == null)
            {
                throw new ArgumentNullException(nameof(patientCreateDto), "Patient data is required.");
            }

            var patient = _mapper.Map<Patient>(patientCreateDto);

            try
            {
                await _unitOfWork.Patients.AddAsync(patient);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating patient: " + ex.Message);
            }
        }

        public async Task UpdatePatientAsync(PatientUpdateDto patientUpdateDto)
        {
            if (patientUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(patientUpdateDto), "Patient update data is required.");
            }

            var existingPatient = await _unitOfWork.Patients.GetByIdAsync(patientUpdateDto.Id);

            // Mevcut hasta kontrolü
            if (existingPatient == null)
            {
                throw new KeyNotFoundException("Patient not found for update.");
            }

            var patient = _mapper.Map(patientUpdateDto, existingPatient);

            try
            {
                await _unitOfWork.Patients.UpdateAsync(patient);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating patient: " + ex.Message);
            }
        }

        public async Task DeletePatientAsync(Guid id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found for deletion.");
            }

            try
            {
                await _unitOfWork.Patients.DeleteAsync(patient);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting patient: " + ex.Message);
            }
        }
    }
}
