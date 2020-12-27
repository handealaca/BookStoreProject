using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class UserLoginVM
    {
        public LoginVM Login { get; set; }
        public UserVM register { get; set; }
    }
}
