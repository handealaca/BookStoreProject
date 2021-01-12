using BookStoreProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class SiteHomeVM
    {
        public List<Book> BookCovers { get; set; }
        public List<Category> Topcategories { get; set; }
        public List<Category> Subcategories { get; set; }

        public List<Person> People { get; set; }
        public List<User> Users { get; set; }

       
    }
}
