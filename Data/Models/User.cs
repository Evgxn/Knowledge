using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        public User()
        {
            Tests = new HashSet<Test>();
        }

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
