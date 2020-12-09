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

        [Required(ErrorMessage = "Name alanı boş geçilemez")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Publisher alanı boş geçilemez")]
        public string Publisher { get; set; }
        public string PublishDate { get; set; }
        public string Edition { get; set; }
        public string WriterName { get; set; }

        public DateTime AddDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public List<Comment> Comments { get; set; }
        public List<BookPerson> BookPersons { get; set; }
        public List<BookCategory> bookCategories { get; set; }
        public List<UserPoint> UserPoints { get; set; }
        public List<Person> people { get; set; }
        public List<Category> categories { get; set; }

        public List<int> DutyID { get; set; }

        public List<Person> writers { get; set; }
        public List<Person> interpreters { get; set; }
    }
}
