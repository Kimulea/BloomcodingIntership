using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Domain
{
    public class Homework : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
    }
}
