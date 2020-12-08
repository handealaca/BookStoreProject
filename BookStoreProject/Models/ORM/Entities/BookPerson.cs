using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class BookPerson:BaseEntity
    {
        public int BookID { get; set; }
        [ForeignKey("BookID")]
        public Book Book { get; set; }

        public int PersonID { get; set; }
        [ForeignKey("PersonID")]
        public Person Person { get; set; }

        //public int DutyID { get; set; }
    }
}
