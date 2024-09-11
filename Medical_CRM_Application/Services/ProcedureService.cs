using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.ProcedureDTOs;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.UnitOfWork;
using Medical_CRM_Domain.DTOs.PaymentDTOs;
using Medical_CRM_Domain.DTOs.ProcedureProductDTOs;
using Microsoft.EntityFrameworkCore;

namespace Medical_CRM_Application.Services
{
    public class ProcedureService : IProcedureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProcedureProductService _procedureProductService;

        public ProcedureService(IUnitOfWork unitOfWork, IMapper mapper, IProcedureProductService procedureProductService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _procedureProductService = procedureProductService;
        }

        public async Task<IEnumerable<ProcedureGetDto>> GetAllProceduresAsync()
        {
            var procedures = await _unitOfWork.Procedures.GetAllAsync();

            if (procedures == null || !procedures.Any())
            {
                return Enumerable.Empty<ProcedureGetDto>();
            }

            return _mapper.Map<IEnumerable<ProcedureGetDto>>(procedures);
        }

        public async Task<IEnumerable<ProcedureGetDto>> GetProceduresByPatientIdAsync(Guid patientId)
        {
            var procedures = await _unitOfWork.Procedures.GetProceduresByPatientIdAsync(patientId);

            if (procedures == null || !procedures.Any())
            {
                return Enumerable.Empty<ProcedureGetDto>();
            }

            return _mapper.Map<IEnumerable<ProcedureGetDto>>(procedures);
        }

        public async Task<ProcedureGetDto> GetProcedureByIdAsync(Guid id)
        {
            var procedure = await _unitOfWork.Procedures.GetByIdAsync(id);

            if (procedure == null)
            {
                throw new KeyNotFoundException("Procedure not found.");
            }

            return _mapper.Map<ProcedureGetDto>(procedure);
        }

        public async Task CreateProcedureAsync(ProcedureCreateDto procedureCreateDto)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Validate the operator exists
                var operatorExists = await _unitOfWork.Operators.GetByIdAsync(procedureCreateDto.PerformedByOperatorId);
                if (operatorExists == null)
                {
                    throw new Exception($"Operator with ID {procedureCreateDto.PerformedByOperatorId} does not exist.");
                }

                var procedure = _mapper.Map<Procedure>(procedureCreateDto);

                // Handle ProcedureProducts
                if (procedureCreateDto.Products != null && procedureCreateDto.Products.Any())
                {
                    foreach (var productDto in procedureCreateDto.Products)
                    {
                        // Retrieve the product from the database
                        var product = await _unitOfWork.Products.GetByIdAsync(productDto.ProductId);

                        // Check if the product exists and has enough stock
                        if (product == null)
                        {
                            throw new Exception($"Product with ID {productDto.ProductId} does not exist.");
                        }
                        if (product.QuantityInStock < productDto.QuantityUsed)
                        {
                            throw new Exception($"Insufficient stock for product {product.Name}. Available: {product.QuantityInStock}, required: {productDto.QuantityUsed}.");
                        }

                        // Decrease the product quantity
                        product.QuantityInStock -= productDto.QuantityUsed;

                        // Check if ProcedureProduct is already being tracked
                        var existingProcedureProduct = procedure.ProcedureProducts
                            .FirstOrDefault(pp => pp.ProductId == productDto.ProductId);

                        if (existingProcedureProduct == null)
                        {
                            // Add the ProcedureProduct entry if not tracked
                            var procedureProduct = new ProcedureProduct
                            {
                                ProcedureId = procedure.Id, // This will be set correctly after saving
                                ProductId = productDto.ProductId,
                                QuantityUsed = productDto.QuantityUsed
                            };

                            procedure.ProcedureProducts.Add(procedureProduct);
                        }
                        else
                        {
                            // Update the existing ProcedureProduct if already tracked
                            existingProcedureProduct.QuantityUsed += productDto.QuantityUsed;
                        }

                        // Update the product in the database
                        await _unitOfWork.Products.UpdateAsync(product);
                    }
                }

                // Save the Procedure to get the generated ID
                await _unitOfWork.Procedures.AddAsync(procedure);

                // Commit the changes
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                if (ex.InnerException != null)
                {
                    throw new ApplicationException($"An error occurred while creating the procedure: {ex.InnerException.Message}", ex);
                }
                else
                {
                    throw new ApplicationException($"An error occurred while creating the procedure: {ex.Message}", ex);
                }
            }
        }

        public async Task UpdateProcedureAsync(ProcedureUpdateDto procedureUpdateDto)
        {
            if (procedureUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(procedureUpdateDto), "Procedure update data is required.");
            }

            var existingProcedure = await _unitOfWork.Procedures
                .GetQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == procedureUpdateDto.Id);

            if (existingProcedure == null)
            {
                throw new KeyNotFoundException("Procedure not found for update.");
            }

            // Map the DTO to the existing procedure
            _mapper.Map(procedureUpdateDto, existingProcedure);

            try
            {
                // Update the procedure and commit changes
                await _unitOfWork.Procedures.UpdateAsync(existingProcedure);
                await _unitOfWork.CommitAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"An error occurred while updating the procedure: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        private async Task HandleProcedureProductsAsync(List<ProcedureProductUpdateDto> productDtos, Procedure procedure)
        {
            
            var existingProcedureProducts = await _procedureProductService.GetByProcedureIdAsync(procedure.Id);

            
            foreach (var existingProcedureProduct in existingProcedureProducts)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(existingProcedureProduct.ProductId);
                if (product != null)
                {
                    
                    product.QuantityInStock += existingProcedureProduct.QuantityUsed;
                    await _unitOfWork.Products.UpdateAsync(product);
                }

                
                await _procedureProductService.DeleteProcedureProductAsync(existingProcedureProduct);
                _unitOfWork.DetachEntity(existingProcedureProduct); 
            }

            
            await _unitOfWork.CommitAsync();

            
            foreach (var productDto in productDtos)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(productDto.ProductId);

                if (product == null)
                {
                    throw new Exception($"Product with ID {productDto.ProductId} does not exist.");
                }

                if (product.QuantityInStock < productDto.QuantityUsed)
                {
                    throw new Exception($"Insufficient stock for product {product.Name}. Available: {product.QuantityInStock}, required: {productDto.QuantityUsed}.");
                }

                // Yeni ProcedureProduct kaydı ekle
                var newProcedureProduct = new ProcedureProduct
                {
                    ProcedureId = procedure.Id,
                    ProductId = productDto.ProductId,
                    QuantityUsed = productDto.QuantityUsed
                };

                await _procedureProductService.AddProcedureProductAsync(newProcedureProduct);
                _unitOfWork.DetachEntity(newProcedureProduct); // Eklenen yeni nesneyi takipten çıkarıyoruz

                // Stok güncelleniyor
                product.QuantityInStock -= productDto.QuantityUsed;
                await _unitOfWork.Products.UpdateAsync(product);
            }

            // Tüm değişiklikleri kaydet
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProcedureAsync(Guid id)
        {
            // Fetch the procedure to ensure it exists
            var procedure = await _unitOfWork.Procedures.GetByIdAsync(id);
            if (procedure == null)
            {
                throw new KeyNotFoundException("Procedure not found for deletion.");
            }

            // Start a transaction to ensure atomicity of the delete operation
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Fetch related ProcedureProducts to handle their deletion
                var relatedProcedureProducts = await _procedureProductService.GetByProcedureIdAsync(procedure.Id);

                if (relatedProcedureProducts.Any())
                {
                    // Remove each related ProcedureProduct and adjust the stock accordingly
                    foreach (var procedureProduct in relatedProcedureProducts)
                    {
                        var product = await _unitOfWork.Products.GetByIdAsync(procedureProduct.ProductId);
                        if (product != null)
                        {
                            // Revert the stock adjustment from the procedure product usage
                            product.QuantityInStock += procedureProduct.QuantityUsed;
                            await _unitOfWork.Products.UpdateAsync(product);
                        }

                        // Delete the ProcedureProduct entry
                        await _procedureProductService.DeleteProcedureProductAsync(procedureProduct);
                    }

                    // Commit the changes related to ProcedureProducts and stock updates
                    await _unitOfWork.CommitAsync();
                }

                // Delete the procedure itself
                await _unitOfWork.Procedures.DeleteAsync(procedure);

                // Commit the transaction to save all changes
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Rollback the transaction in case of a database update exception
                await transaction.RollbackAsync();
                // Capture and throw a detailed exception message
                throw new Exception($"Error deleting procedure: {dbEx.InnerException?.Message ?? dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of any other exceptions
                await transaction.RollbackAsync();
                // Provide a generic error message with details from the exception
                throw new Exception("Error deleting procedure: " + ex.Message, ex);
            }
        }

    }
}
