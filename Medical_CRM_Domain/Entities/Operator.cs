using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.Entities
{
    public class Operator
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string OperatorCode { get; set; } // Operatörü tanımlamak için benzersiz bir kod (ID dışında)
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Operatörün gerçekleştirdiği işlemler
        public ICollection<Procedure> Procedures { get; set; }

        public Operator()
        {
            Procedures = new List<Procedure>();
        }
    }
}
