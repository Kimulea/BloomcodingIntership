using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Common.Dtos.Groups
{
    public class NewGroupDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name is too short")]
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
