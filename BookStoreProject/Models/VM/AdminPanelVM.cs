using BookStoreProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class AdminPanelVM
    {
        public List<BookVM>Books { get; set; }
        public List<UserVM> Users { get; set; }
        public List<CategoryVM> Categories { get; set; }
        public List<PersonVM> People { get; set; }
    }
}
