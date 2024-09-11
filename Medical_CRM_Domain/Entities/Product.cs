using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string ProductType { get; set; }
        
        public DateTime ExpiryDate { get; set; }
        public string Supplier { get; set; }

        public ICollection<StockTransaction> StockTransactions { get; set; }

        public Product()
        {
            StockTransactions = new List<StockTransaction>();
        }
    }
}
