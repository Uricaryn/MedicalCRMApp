using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.PaymentDTOs;
using Medical_CRM_Domain.DTOs.ProcedureProductDTOs;
using Medical_CRM_Domain.Enums;

namespace Medical_CRM_Domain.DTOs.ProcedureDTOs
{
    public record ProcedureGetDto
    {
        public Guid Id { get; init; }

        public Guid PatientId { get; init; }

        public ProcedureType ProcedureType { get; init; }

        public DateTime ProcedureDate { get; init; }

        public string Description { get; init; }

        public TimeSpan Duration { get; init; }

        public string PostProcedureNotes { get; init; }

        public List<ProcedureProductDto> Products { get; init; }
    }
}
