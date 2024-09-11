using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_CRM_Domain.Entities
{
    public class Role
    {
        public Guid RoleId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        // Navigation property to Users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
