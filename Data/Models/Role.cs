using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public Guid Id { get; set; }
        public string? RoleName { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
