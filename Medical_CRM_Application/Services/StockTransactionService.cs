using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.StockTransactionDTOs;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.UnitOfWork;

namespace Medical_CRM_Application.Services
{
    public class StockTransactionService : IStockTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StockTransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockTransactionGetDto>> GetAllStockTransactionsAsync()
        {
            var stockTransactions = await _unitOfWork.StockTransactions.GetAllAsync();

            if (stockTransactions == null || !stockTransactions.Any())
            {
                return Enumerable.Empty<StockTransactionGetDto>();
            }

            return _mapper.Map<IEnumerable<StockTransactionGetDto>>(stockTransactions);
        }

        public async Task<IEnumerable<StockTransactionGetDto>> GetStockTransactionsByProductIdAsync(Guid productId)
        {
            var stockTransactions = await _unitOfWork.StockTransactions.GetTransactionsByProductIdAsync(productId);

            if (stockTransactions == null || !stockTransactions.Any())
            {
                return Enumerable.Empty<StockTransactionGetDto>();
            }

            return _mapper.Map<IEnumerable<StockTransactionGetDto>>(stockTransactions);
        }

        public async Task<StockTransactionGetDto> GetStockTransactionByIdAsync(Guid id)
        {
            var stockTransaction = await _unitOfWork.StockTransactions.GetByIdAsync(id);

            if (stockTransaction == null)
            {
                throw new KeyNotFoundException("Stock transaction not found.");
            }

            return _mapper.Map<StockTransactionGetDto>(stockTransaction);
        }

        public async Task CreateStockTransactionAsync(StockTransactionCreateDto stockTransactionCreateDto)
        {
            if (stockTransactionCreateDto == null)
            {
                throw new ArgumentNullException(nameof(stockTransactionCreateDto), "Stock transaction data is required.");
            }

            // Retrieve the related product
            var product = await _unitOfWork.Products.GetByIdAsync(stockTransactionCreateDto.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            // Map the DTO to the StockTransaction entity
            var stockTransaction = _mapper.Map<StockTransaction>(stockTransactionCreateDto);

            // Adjust the product quantity based on the transaction
            product.QuantityInStock += stockTransaction.Quantity; // Increase or decrease quantity based on the transaction

            try
            {
                // Save the transaction and update the product quantity
                await _unitOfWork.StockTransactions.AddAsync(stockTransaction);
                await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating stock transaction: " + ex.Message);
            }
        }

        public async Task UpdateStockTransactionAsync(StockTransactionUpdateDto stockTransactionUpdateDto)
        {
            if (stockTransactionUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(stockTransactionUpdateDto), "Stock transaction update data is required.");
            }

            var existingTransaction = await _unitOfWork.StockTransactions.GetByIdAsync(stockTransactionUpdateDto.Id);

            if (existingTransaction == null)
            {
                throw new KeyNotFoundException("Stock transaction not found for update.");
            }

            // Retrieve the related product
            var product = await _unitOfWork.Products.GetByIdAsync(existingTransaction.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            // Revert the old transaction quantity effect
            product.QuantityInStock -= existingTransaction.Quantity;

            // Update the stock transaction with new values
            var updatedTransaction = _mapper.Map(stockTransactionUpdateDto, existingTransaction);

            // Apply the new transaction quantity
            product.QuantityInStock += updatedTransaction.Quantity;

            try
            {
                // Save the updated transaction and adjust product quantity
                await _unitOfWork.StockTransactions.UpdateAsync(updatedTransaction);
                await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating stock transaction: " + ex.Message);
            }
        }

        public async Task DeleteStockTransactionAsync(Guid id)
        {
            var stockTransaction = await _unitOfWork.StockTransactions.GetByIdAsync(id);

            if (stockTransaction == null)
            {
                throw new KeyNotFoundException("Stock transaction not found for deletion.");
            }

            // Retrieve the related product
            var product = await _unitOfWork.Products.GetByIdAsync(stockTransaction.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            // Revert the stock change before deletion
            product.QuantityInStock -= stockTransaction.Quantity;

            try
            {
                await _unitOfWork.StockTransactions.DeleteAsync(stockTransaction);
                await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting stock transaction: " + ex.Message);
            }
        }
    }
}
