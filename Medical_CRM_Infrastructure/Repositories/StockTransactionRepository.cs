using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Interfaces;
using Medical_CRM_Infrastructure.Context;

namespace Medical_CRM_Infrastructure.Repositories
{
    public class StockTransactionRepository : GenericRepository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StockTransaction>> GetTransactionsByProductIdAsync(Guid productId)
        {
            return await GetAllAsync(st => st.ProductId == productId);
        }

        public async Task<IEnumerable<StockTransaction>> GetIncomingTransactionsAsync()
        {
            return await GetAllAsync(st => st.TransactionType == "IN");
        }

        public async Task<IEnumerable<StockTransaction>> GetOutgoingTransactionsAsync()
        {
            return await GetAllAsync(st => st.TransactionType == "OUT");
        }
    }
}
