using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class User : BaseEntity

    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserPoint> UserPoints { get; set; }

        public DateTime Lastlogindate { get; set; }

    }
}
