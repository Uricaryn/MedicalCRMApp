using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.OperatorDTOs
{
    public record OperatorGetDto
    {
        [MaxLength(100)]
        public string? FullName { get; init; }

        [MaxLength(50)]
        public string? OperatorCode { get; init; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; init; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; init; }
    }
}
