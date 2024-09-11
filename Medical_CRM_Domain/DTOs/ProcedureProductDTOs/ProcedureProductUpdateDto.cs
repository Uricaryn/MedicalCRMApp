using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.ProcedureProductDTOs
{
    public record ProcedureProductUpdateDto
    {
        public string ProcedureProductId { get; init; }

        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; init; }

        [Required(ErrorMessage = "Quantity used is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int QuantityUsed { get; init; }

        // Add an identifier or flag to distinguish between existing and new entries if needed
        public bool IsNew { get; init; } // Optional, to differentiate between new and existing ProcedureProducts
    }
}
