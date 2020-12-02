using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public List<BookCategory> BookCategories { get; set; }

    }
}
