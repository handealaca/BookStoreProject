using BookStoreProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class BookVM
    {
        public int BookID { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Publisher field is required.")]
        public string Publisher { get; set; }
        public string PublishDate { get; set; } 
        public string Edition { get; set; }

        [Required(ErrorMessage = "Writer/Interpreter field is required.")]
        public string WriterName { get; set; }

        public DateTime AddDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public List<Comment> Comments { get; set; }
        public List<BookPerson> BookPersons { get; set; }
        public List<BookCategory> bookCategories { get; set; }
        public List<UserPoint> UserPoints { get; set; }
        [Required(ErrorMessage = "Writer field is required.")]
        public List<Person> people { get; set; }
        [Required(ErrorMessage = "Category field is required.")]
        public List<Category> categories { get; set; }
        
        

       




    }
}
