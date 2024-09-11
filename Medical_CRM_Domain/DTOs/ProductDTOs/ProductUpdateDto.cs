using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.ProductDTOs
{
    public record ProductUpdateDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; init; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; init; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; init; }

        [Required(ErrorMessage = "Quantity in stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int QuantityInStock { get; init; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime ExpiryDate { get; init; }

        [MaxLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        public string Supplier { get; init; }
    }
}
