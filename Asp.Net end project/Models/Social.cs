using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Models
{
    public class Social : BaseEntity
    {
        public string Image { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
