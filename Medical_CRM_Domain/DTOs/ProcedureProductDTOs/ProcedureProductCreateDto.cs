using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.ProcedureProductDTOs
{
    public record ProcedureProductCreateDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; init; }

        [Required(ErrorMessage = "Quantity used is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int QuantityUsed { get; init; }
    }
}
