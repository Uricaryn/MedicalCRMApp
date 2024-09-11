using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.ProcedureProductDTOs;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Interfaces;
using Medical_CRM_Domain.Services;

namespace Medical_CRM_Application.Services
{
    public class ProcedureProductService : IProcedureProductService
    {
        private readonly IProcedureProductRepository _procedureProductRepository;

        public ProcedureProductService(IProcedureProductRepository procedureProductRepository)
        {
            _procedureProductRepository = procedureProductRepository;
        }

        public async Task<IEnumerable<ProcedureProduct>> GetByProcedureIdAsync(Guid procedureId)
        {
            return await _procedureProductRepository.GetByProcedureIdAsync(procedureId);
        }

        public async Task AddProcedureProductAsync(ProcedureProduct procedureProduct)
        {
            await _procedureProductRepository.AddAsync(procedureProduct);
        }

        public async Task UpdateProcedureProductAsync(ProcedureProduct procedureProduct)
        {
            await _procedureProductRepository.UpdateAsync(procedureProduct);
        }

        public async Task DeleteProcedureProductAsync(ProcedureProduct procedureProduct)
        {
            await _procedureProductRepository.DeleteAsync(procedureProduct);
        }

        public async Task<ProcedureProduct> GetByProcedureIdAndProductIdAsync(Guid procedureId, Guid productId)
        {
            return await _procedureProductRepository.GetAsync(
             pp => pp.ProcedureId == procedureId && pp.ProductId == productId);
        }

        public async Task<ProcedureProductDto> GetByProcedureProductIdAsync(string procedureProductId)
        {
           
            var procedureProduct = await _procedureProductRepository.GetByProcedureProductIdAsync(procedureProductId);

            
            if (procedureProduct == null)
            {
                
                return null;
            }

            
            var procedureProductDto = new ProcedureProductDto
            {
                ProcedureProductId = procedureProduct.ProcedureProductId,
                ProcedureId = procedureProduct.ProcedureId,
                ProductId = procedureProduct.ProductId,
                QuantityUsed = procedureProduct.QuantityUsed
            };

            return procedureProductDto;
        }
    }
}
