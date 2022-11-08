using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Models
{
    public class Product : BaseEntity
    {
        
        public string Title { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal DiscountPrice { get; set; }
        public string Description { get; set; }
        public int SellerCount { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }


    }
}
