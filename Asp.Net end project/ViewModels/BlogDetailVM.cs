﻿using Asp.Net_end_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.ViewModels
{
    public class BlogDetailVM
    {
        public Blog Blog { get; set; }
        public IEnumerable<Blog> ResentPosts { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
