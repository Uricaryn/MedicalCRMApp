using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.UserDTOs
{
    public record UserCreateDto
    {
        [Required]
        public string UserName { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; init; }

        [Required]
        public string FullName { get; init; }

        public List<string> Roles { get; init; } = new();
    }
}
