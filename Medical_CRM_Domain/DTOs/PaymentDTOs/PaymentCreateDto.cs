using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.PaymentDTOs
{
    public record PaymentCreateDto
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; init; }

        [Required(ErrorMessage = "Procedure ID is required.")]
        public Guid ProcedureId { get; init; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; init; }

        [Required(ErrorMessage = "Payment date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime PaymentDate { get; init; }

        [Required(ErrorMessage = "Payment method is required.")]
        [MaxLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
        public string PaymentMethod { get; init; }
    }
}
