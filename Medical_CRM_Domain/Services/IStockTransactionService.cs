using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.StockTransactionDTOs;

namespace Medical_CRM_Domain.Services
{
    public interface IStockTransactionService
    {
        Task<IEnumerable<StockTransactionGetDto>> GetAllStockTransactionsAsync();
        Task<IEnumerable<StockTransactionGetDto>> GetStockTransactionsByProductIdAsync(Guid productId);
        Task<StockTransactionGetDto> GetStockTransactionByIdAsync(Guid id);
        Task CreateStockTransactionAsync(StockTransactionCreateDto stockTransactionCreateDto);
        Task UpdateStockTransactionAsync(StockTransactionUpdateDto stockTransactionUpdateDto);
        Task DeleteStockTransactionAsync(Guid id);
    }
}
