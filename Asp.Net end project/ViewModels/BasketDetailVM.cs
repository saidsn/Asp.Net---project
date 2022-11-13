using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.ViewModels
{
    public class BasketDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Count { get; set; }

        public decimal Total { get; set; }

       


    }
}
