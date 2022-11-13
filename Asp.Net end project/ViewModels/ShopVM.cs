using Asp.Net_end_project.Helpers;
using Asp.Net_end_project.Models;
using Asp.Net_end_project.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.ViewModels
{
    public class ShopVM
    {
        public IEnumerable<Product> Products { get; set; }
        //public Paginate<ProductListVM> Products { get; set; }
        public IEnumerable<Categories> Categories { get; set; }

    }
}
