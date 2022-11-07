using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Models
{
    public class Banner : BaseEntity
    {
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
