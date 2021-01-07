using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class SiteBookVM
    {
        public List<BookVM> BookVM { get; set; }

        public List<CategoryVM> CategoryVM { get; set; }
    }
}
