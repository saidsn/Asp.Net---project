﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Controllers
{
    public class BlogDetailController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
