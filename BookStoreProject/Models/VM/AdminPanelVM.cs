using BookStoreProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class AdminPanelVM
    {
        public List<Book>Books { get; set; }
        public List<User> Users { get; set; }
        public List<Category> Categories { get; set; }
        public List<Person> People { get; set; }
    }
}
