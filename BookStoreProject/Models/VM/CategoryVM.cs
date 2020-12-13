using BookStoreProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class CategoryVM
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category Name field is required.")]
        public string CategoryName { get; set; }
        public DateTime AddDate { get; set; }= DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public List<Category> topcategories { get; set; }

     
        public List<BookCategory> Books { get; set; }

        public string subcategoryname { get; set; }
        public int? TopCategory { get; set; }
    }
}
