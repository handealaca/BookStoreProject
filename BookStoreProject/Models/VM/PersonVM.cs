﻿using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Required(ErrorMessage = "Name alanı boş geçilemez")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname alanı boş geçilemez")]
        public string SurName { get; set; }

        public string Biography { get; set; }
        public DateTime? BirthDate { get; set; }

        public string Duty { get; set; }
        public DateTime? AddDate { get; set; }
        public bool IsDeleted { get; set; }

   
      
    }


}