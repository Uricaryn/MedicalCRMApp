using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Enums;

namespace Medical_CRM_Domain.Entities
{
    public class Procedure
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public ProcedureType ProcedureType { get; set; }
        public DateTime ProcedureDate { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string PostProcedureNotes { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public Patient Patient { get; set; }

        public Guid PerformedByOperatorId { get; set; }
        public Operator PerformedByOperator { get; set; }

        public ICollection<ProcedureProduct> ProcedureProducts { get; set; } = new List<ProcedureProduct>();

        public Procedure()
        {
            ProcedureProducts = new List<ProcedureProduct>();
            Payments = new List<Payment>();
        }
    }
}
