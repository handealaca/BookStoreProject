using BookStoreProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class SiteBookVM
    {
        public List<BookVM> BookVM { get; set; }

        public List<CategoryVM> categories { get; set; }

        public List<BookCategory> bookcategories { get; set; }

        public List<PersonVM> PersonVM { get; set; }
    }
}
