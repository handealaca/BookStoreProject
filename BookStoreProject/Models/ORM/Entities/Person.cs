using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStoreProject.Models.VM.PersonVM;

namespace BookStoreProject.Models.ORM.Entities
{
    public class Person :BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Biography { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<Book> Books { get; set; }
        public List<BookPerson> BookPeople { get; set; }
        public List<PersonDuty> PersonDuties { get; set; }
        public string Imagepath { get; set; }
    }
}
