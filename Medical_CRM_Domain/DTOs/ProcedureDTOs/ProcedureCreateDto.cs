using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.PaymentDTOs;
using Medical_CRM_Domain.DTOs.ProcedureProductDTOs;
using Medical_CRM_Domain.Enums;

namespace Medical_CRM_Domain.DTOs.ProcedureDTOs
{
    public record ProcedureCreateDto
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; init; }

        [Required(ErrorMessage = "Procedure type is required.")]
        public ProcedureType ProcedureType { get; init; }

        [Required(ErrorMessage = "Procedure date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime ProcedureDate { get; init; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; init; }

        public TimeSpan? Duration { get; init; }

        [MaxLength(500, ErrorMessage = "Post-procedure notes cannot exceed 500 characters.")]
        public string? PostProcedureNotes { get; init; }

        [Required(ErrorMessage = "Performed By Operator ID is required.")]
        public Guid PerformedByOperatorId { get; init; }

        public List<ProcedureProductCreateDto> Products { get; init; } = new();

    }
}
