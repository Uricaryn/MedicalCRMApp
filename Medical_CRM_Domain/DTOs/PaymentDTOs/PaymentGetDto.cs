using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.DTOs.PaymentDTOs
{
    public record PaymentGetDto
    {
        public Guid Id { get; init; }

        public Guid PatientId { get; init; }

        public Guid ProcedureId { get; init; }

        public decimal Amount { get; init; }

        public DateTime PaymentDate { get; init; }

        public string PaymentMethod { get; init; }
    }
}
