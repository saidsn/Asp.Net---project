using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Models
{
    public class OurProduct : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
