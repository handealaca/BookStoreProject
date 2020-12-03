using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class Person :BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Biography { get; set; }
        public DateTime? BirthDate { get; set; }

        public List<Book> Books { get; set; }

        public enum Duty { Writer = 1, Interpreter = 2 };
        public List<BookPerson> BookPeople { get; set; }
    }
}
