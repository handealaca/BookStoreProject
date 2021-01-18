using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class CommentVM
    {
        public int CommentID { get; set; }
        public DateTime AddDate { get; set; }= DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public int UserID { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
        public int BookID { get; set; }
        public string Header { get; set; }

    }
}
