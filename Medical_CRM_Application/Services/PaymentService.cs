using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.PaymentDTOs;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Medical_CRM_Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentGetDto>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();

            // Eğer ödeme bulunamazsa boş bir liste döner
            if (payments == null || !payments.Any())
            {
                return Enumerable.Empty<PaymentGetDto>();
            }

            return _mapper.Map<IEnumerable<PaymentGetDto>>(payments);
        }

        public async Task<IEnumerable<PaymentGetDto>> GetPaymentsByPatientIdAsync(Guid patientId)
        {
            var payments = await _unitOfWork.Payments.GetPaymentsByPatientIdAsync(patientId);

            // Eğer ödemeler bulunamazsa boş bir liste döner
            if (payments == null || !payments.Any())
            {
                return Enumerable.Empty<PaymentGetDto>();
            }

            return _mapper.Map<IEnumerable<PaymentGetDto>>(payments);
        }

        public async Task<PaymentGetDto> GetPaymentByIdAsync(Guid id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);

            // Eğer ödeme bulunamazsa hata fırlatılır
            if (payment == null)
            {
                throw new KeyNotFoundException("Payment not found.");
            }

            return _mapper.Map<PaymentGetDto>(payment);
        }

        public async Task CreatePaymentAsync(PaymentCreateDto paymentCreateDto)
        {
            // Validation checks
            if (paymentCreateDto == null)
            {
                throw new ArgumentNullException(nameof(paymentCreateDto), "Payment data is required.");
            }

            var payment = _mapper.Map<Payment>(paymentCreateDto);

            try
            {
                await _unitOfWork.Payments.AddAsync(payment);
                await _unitOfWork.CommitAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Log the inner exception for further details
                if (dbEx.InnerException != null)
                {
                    throw new Exception("Error creating payment: " + dbEx.InnerException.Message, dbEx);
                }
                else
                {
                    throw new Exception("Error creating payment: " + dbEx.Message, dbEx);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating payment: " + ex.Message, ex);
            }
        }

        public async Task UpdatePaymentAsync(PaymentUpdateDto paymentUpdateDto)
        {
            if (paymentUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(paymentUpdateDto), "Payment update data is required.");
            }

            // Fetch the existing payment to ensure it exists
            var existingPayment = await _unitOfWork.Payments.GetByIdAsync(paymentUpdateDto.Id);

            if (existingPayment == null)
            {
                throw new KeyNotFoundException("Payment not found for update.");
            }

            // Validate that the referenced Procedure exists
            if (paymentUpdateDto.ProcedureId != null)
            {
                var procedureExists = await _unitOfWork.Procedures.GetByIdAsync(paymentUpdateDto.ProcedureId);
                if (procedureExists == null)
                {
                    throw new Exception($"Procedure with ID {paymentUpdateDto.ProcedureId} does not exist.");
                }
            }

            // Begin a transaction to ensure the update process is atomic
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Map the updated data from DTO to the existing payment entity
                _mapper.Map(paymentUpdateDto, existingPayment);

                // Attempt to update the payment entity in the database
                await _unitOfWork.Payments.UpdateAsync(existingPayment);

                // Commit the transaction to save all changes
                await _unitOfWork.CommitAsync();
                await transaction.CommitAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Rollback the transaction in case of a database update exception
                await transaction.RollbackAsync();

                // Log the inner exception for more specific debugging information
                var errorMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                // _logger.LogError(dbEx, "Error updating payment: {ErrorMessage}", errorMessage); // Assuming logger setup

                // Throw an exception with detailed context
                throw new Exception($"Error updating payment: {errorMessage}", dbEx);
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of any other exceptions
                await transaction.RollbackAsync();

                // Provide a generic error message with details from the exception
                // _logger.LogError(ex, "Error updating payment: {ErrorMessage}", ex.Message); // Assuming logger setup

                throw new Exception("Error updating payment: " + ex.Message, ex);
            }
        }



        public async Task DeletePaymentAsync(Guid id)
        {
            var payment = await _unitOfWork.Payments.GetByIdAsync(id);

            if (payment == null)
            {
                throw new KeyNotFoundException("Payment not found for deletion.");
            }

            try
            {
                await _unitOfWork.Payments.DeleteAsync(payment);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting payment: " + ex.Message);
            }
        }
    }
}
