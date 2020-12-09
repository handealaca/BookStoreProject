using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class PersonDuty:BaseEntity
    {
        public int DutyID { get; set; }
        public int PersonID { get; set; }
        [ForeignKey("PersonID")]
        public Person Person { get; set; }
    }
}
