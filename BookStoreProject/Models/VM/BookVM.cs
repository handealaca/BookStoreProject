using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class BookVM
    {
        public int BookID { get; set; }

        public string Name { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public string Edition { get; set; }

        public DateTime? AddDate { get; set; }

        public bool IsDeleted { get; set; }



    }
}
