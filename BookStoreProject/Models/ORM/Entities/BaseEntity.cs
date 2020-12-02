using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
