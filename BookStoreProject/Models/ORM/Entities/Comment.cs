using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class Comment : BaseEntity
    {
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public int BookID { get; set; }
        [ForeignKey("BookID")]
        public Book Book { get; set; }
        public string Content { get; set; }
        public string Header { get; set; }
    }
}
