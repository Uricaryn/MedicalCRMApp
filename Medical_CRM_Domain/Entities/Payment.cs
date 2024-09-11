using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid ProcedureId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        public Patient Patient { get; set; }
        public Procedure Procedure { get; set; }
    }
}
