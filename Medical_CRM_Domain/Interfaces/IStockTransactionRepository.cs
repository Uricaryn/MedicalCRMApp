using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;

namespace Medical_CRM_Domain.Interfaces
{
    public interface IStockTransactionRepository : IGenericRepository<StockTransaction>
    {
        Task<IEnumerable<StockTransaction>> GetTransactionsByProductIdAsync(Guid productId);
        Task<IEnumerable<StockTransaction>> GetIncomingTransactionsAsync();
        Task<IEnumerable<StockTransaction>> GetOutgoingTransactionsAsync();
    }
}
