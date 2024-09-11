using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.ProductDTOs
{
    public record ProductGetDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public int QuantityInStock { get; init; }

        public DateTime ExpiryDate { get; init; }

        public string Supplier { get; init; }
    }
}
