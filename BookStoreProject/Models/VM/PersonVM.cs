using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.Types;
using Microsoft.AspNetCore.Http;
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

        public IFormFile Coverimage { set; get; }

        public string Biography { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; } 
        public List<string> Duties { get; set; }
     
        public List<EnumDuty> EnumDuties { get; set; }
        public List<BookPerson> Books { get; set; }

        public string Imagepath { get; set; }
        public List<BookPerson> BookPeople { get; internal set; }
    }




}
