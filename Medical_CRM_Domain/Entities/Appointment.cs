using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime Date { get; set; }
        public string ProcedureName { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public Patient Patient { get; set; }
    }
}
