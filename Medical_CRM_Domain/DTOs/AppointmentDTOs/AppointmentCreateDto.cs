using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Enums;

namespace Medical_CRM_Domain.DTOs.AppointmentDTOs
{
    public record AppointmentCreateDto
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; init; }

        [Required(ErrorMessage = "Appointment date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = "Procedure name is required.")]
        public ProcedureType ProcedureName { get; init; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; init; }

        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; init; }
    }
}
