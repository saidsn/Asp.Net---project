using Asp.Net_end_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public OurProduct OurProduct { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Banner> Banners { get; set; }
        public TopSeller TopSeller { get; set; }
        public SingleBanner SingleBanner { get; set; }
        public OurBlog OurBlog { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Brand> Brands { get; set; }

    }
}
