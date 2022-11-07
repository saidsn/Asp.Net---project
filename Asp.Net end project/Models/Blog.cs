using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Models
{
    public class Blog : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Admin { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }


    }
}
