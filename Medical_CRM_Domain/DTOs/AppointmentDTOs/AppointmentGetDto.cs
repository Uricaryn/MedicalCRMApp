using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Enums;

namespace Medical_CRM_Domain.DTOs.AppointmentDTOs
{
    public record AppointmentGetDto
    {
        public Guid Id { get; init; }

        public Guid PatientId { get; init; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime Date { get; init; }

        [MaxLength(100)]
        public ProcedureType ProcedureName { get; init; }

        [MaxLength(500)]
        public string Notes { get; init; }

        [MaxLength(50)]
        public string Status { get; init; }
    }
}
