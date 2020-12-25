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
    }
}
