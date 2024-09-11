using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.StockTransactionDTOs
{
    public record StockTransactionGetDto
    {
        public Guid Id { get; init; }

        public Guid ProductId { get; init; }

        public int Quantity { get; init; }

        public DateTime TransactionDate { get; init; }

        public string TransactionType { get; init; }

        public string Supplier { get; init; }

        public string DocumentNumber { get; init; }
    }
}
