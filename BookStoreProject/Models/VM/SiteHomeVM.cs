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

        //public List<Category> getsubcategory(int id)
        //{
        //    List<Category> categories = new List<Category>();

        //    var Subcategories = categories.Where(q => q.IsDeleted == false && q.TopCategory == id).ToList();
        //    return (Subcategories);
        //}

    }
}
