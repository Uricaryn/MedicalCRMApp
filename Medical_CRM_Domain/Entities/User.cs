using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Medical_CRM_Domain.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
