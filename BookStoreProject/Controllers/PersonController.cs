using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStoreProject.Models.ORM.Entities.Person;

namespace BookStoreProject.Controllers
{
    public class PersonController : Controller
    {

        private readonly BookContext _bookcontext;
        public PersonController(BookContext bookContext)
        {
            _bookcontext = bookContext;
        }

       

        public IActionResult Index()
        {

            List<PersonVM> books = _bookcontext.People.Where(q => q.IsDeleted == false).Select(q => new PersonVM()
            {
                PersonID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                Biography = q.Biography,
                BirthDate = q.BirthDate,
                AddDate = q.AddDate,
                IsDeleted = q.IsDeleted,
                //duty=q.Duty.Interpreter|Duty.Writer,
            }).ToList();

            //var duties = from Duty duty in Enum.GetValues(typeof(Duty))
            //                 select new { ID = duty, Name = duty.ToString() };

            //ViewBag.Duties = duties;



            return View(books);
        }
    }
}
