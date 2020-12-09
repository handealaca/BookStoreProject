using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.Types;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BookStoreProject.Models.ORM.Entities.Person;

namespace BookStoreProject.Models.VM
{
    public class PersonVM
    {
        public int PersonID { get; set; }

        [Required(ErrorMessage = "Name must be written")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname must be written")]
        public string SurName { get; set; }

        public string Biography { get; set; }
        public DateTime? BirthDate { get; set; } 
        public string Duty { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public List<EnumDuty> EnumDuties { get; set; }


    }

    //public enum Duty
    //{
    //    Writer = 1,
    //    Interpreter = 2
    //}


}
