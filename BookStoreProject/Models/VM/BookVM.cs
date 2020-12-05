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

        public string WriterName { get; set; }
        public DateTime PublishDate { get; set; }
        public string Edition { get; set; }
        public DateTime AddDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
