﻿using Asp.Net_end_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.ViewModels
{
    public class ProductDetailVM
    {
        public Product Products { get; set; }
        public Dictionary<string,string> Settings { get; set; }
    }
}
