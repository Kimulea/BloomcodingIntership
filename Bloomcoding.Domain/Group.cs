using Bloomcoding.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Domain
{
    public class Group : BaseEntity 
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
