using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.OperatorDTOs
{
    public record OperatorCreateDto
    {
        [Required(ErrorMessage = "Full name is required.")]
        [MaxLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; init; }

        [Required(ErrorMessage = "Operator code is required.")]
        [MaxLength(50, ErrorMessage = "Operator code cannot exceed 50 characters.")]
        public string OperatorCode { get; init; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; init; }
    }
}
