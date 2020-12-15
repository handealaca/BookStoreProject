using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class AdminMenu 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string IconName { get; set; }
    }
}
