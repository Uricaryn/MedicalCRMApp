using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.AppointmentDTOs
{
    public record AppointmentUpdateDto
    {
        [Required(ErrorMessage = "Appointment ID is required.")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; init; }

        [Required(ErrorMessage = "Appointment date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = "Procedure name is required.")]
        [MaxLength(100, ErrorMessage = "Procedure name cannot exceed 100 characters.")]
        public string ProcedureName { get; init; }

        [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; init; }

        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; init; }
    }
}
