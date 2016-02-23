using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;

namespace BookStore_Web_.Models.VM
{
    class xxxViewmodel
    {
       
            public  IPagedList<BookStore_Web_.Models.Absence_Table> data { get; set; }
            public bool  isboss { get; set; }
    }
}
