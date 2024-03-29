﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_end_project.Helpers
{
    public class Paginate<T>
    {
        public List<T> Datas { get; set; }
        public int Currentpage { get; set; }
        public int TotalPage { get; set; }


        public Paginate(List<T> datas, int currentPage, int totalPage)
        {
            Datas = datas;
            Currentpage = currentPage;
            TotalPage = totalPage;
        }

        public bool HasPrevious
        {
            get
            {
                return Currentpage > 1;
            }
        }

        public bool HasNext
        {
            get
            {
                return Currentpage < TotalPage;
            }
        }
    }
}
