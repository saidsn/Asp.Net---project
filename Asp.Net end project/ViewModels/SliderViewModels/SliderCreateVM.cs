﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.ViewModels.SliderViewModels
{
    public class SliderCreateVM
    {
        public int Id { get; set; }
        public string Sale { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Can't be empty")]
        public List<IFormFile> Photos { get; set; }
    }
}
