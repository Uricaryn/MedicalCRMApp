using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.StockTransactionDTOs
{
    public record StockTransactionUpdateDto
    {
        [Required(ErrorMessage = "Transaction ID is required.")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; init; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; init; }

        [Required(ErrorMessage = "Transaction date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format.")]
        public DateTime TransactionDate { get; init; }

        [Required(ErrorMessage = "Transaction type is required.")]
        [MaxLength(50, ErrorMessage = "Transaction type cannot exceed 50 characters.")]
        public string TransactionType { get; init; }

        [MaxLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        public string Supplier { get; init; }

        [MaxLength(50, ErrorMessage = "Document number cannot exceed 50 characters.")]
        public string DocumentNumber { get; init; }
    }
}
