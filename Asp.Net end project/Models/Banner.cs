﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Models
{
    public class Banner : BaseEntity
    {
        public string Image { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Can't be empty")]
        public IFormFile Photo { get; set; }
    }
}
