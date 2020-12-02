using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public string Edition { get; set; }
        public List<Comment> Comments { get; set; }
        public List<BookCategory> BookCategories { get; set; }
        public List<UserPoint> UserPoints { get; set; }
        public List<BookPerson> BookPersons { get; set; }
        public List<BookGenre> BookGenres { get; set; }

    }
}
