using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.Entities
{
    public class ProcedureProduct
    {
        public string ProcedureProductId { get; set; }
        public Guid ProcedureId { get; set; }
        public Procedure Procedure { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int QuantityUsed { get; set; }
    }
}
