using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.PaymentDTOs;

namespace Medical_CRM_Domain.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentGetDto>> GetAllPaymentsAsync();
        Task<IEnumerable<PaymentGetDto>> GetPaymentsByPatientIdAsync(Guid patientId);
        Task<PaymentGetDto> GetPaymentByIdAsync(Guid id);
        Task CreatePaymentAsync(PaymentCreateDto paymentCreateDto);
        Task UpdatePaymentAsync(PaymentUpdateDto paymentUpdateDto);
        Task DeletePaymentAsync(Guid id);
    }
}
