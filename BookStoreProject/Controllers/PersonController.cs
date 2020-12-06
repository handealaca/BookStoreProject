using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.Types;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            List<PersonVM> people = _bookcontext.People.Where(q => q.IsDeleted == false).Select(q => new PersonVM()
            {
                PersonID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                Biography = q.Biography,
                BirthDate = q.BirthDate,
                AddDate = q.AddDate,
                IsDeleted = q.IsDeleted,
                UpdateDate = q.UpdateDate,
                Duty = q.Duty == Convert.ToInt32(EnumDuty.Writer) ? EnumDuty.Writer.ToString() : EnumDuty.Interpreter.ToString()
            }).ToList();

            return View(people);

            //duty=q.Duty.Interpreter|Duty.Writer,

            //var duties = from Duty duty in Enum.GetValues(typeof(Duty))
            //                 select new { ID = duty, Name = duty.ToString() };

            //ViewBag.Duties = duties;

        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(PersonVM model)
        {
            Person person = new Person();
            person.Name = model.Name;
            person.SurName = model.SurName;
            person.Biography = model.Biography;
            person.BirthDate = model.BirthDate;
            person.UpdateDate = model.UpdateDate;
            person.IsDeleted = model.IsDeleted;

            _bookcontext.People.Add(person);
            _bookcontext.SaveChanges();

            return RedirectToAction("Add", "Person");

        }
                
        public IActionResult BookDetail(int id)
        {
            List<BookVM> books = _bookcontext.BookPeople.Include(q => q.Book).Include(q => q.Person).Where(q => q.PersonID == id).Select(q => new BookVM()
            {
                BookID = q.BookID,
                Name = q.Book.Name,
                Publisher = q.Book.Publisher,
                PublishDate = q.Book.PublishDate,
                WriterName = q.Person.Name + " " + q.Person.SurName,


            }).ToList();

            return View(books);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Person person = _bookcontext.People.FirstOrDefault(x => x.ID == id);
            person.IsDeleted = true;

            _bookcontext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Person person = _bookcontext.People.FirstOrDefault(x => x.ID == id);

            PersonVM model = new PersonVM();
            model.PersonID = person.ID;
            model.Name = person.Name;
            model.SurName = person.SurName;
            model.BirthDate = person.BirthDate;
            model.Biography = person.Biography;
            model.Duty = person.Duty.ToString();
            model.UpdateDate = person.UpdateDate;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PersonVM model)
        {
            Person person = _bookcontext.People.FirstOrDefault(q => q.ID == model.PersonID);

            if (ModelState.IsValid)
            {
                //person.ID = model.PersonID;
                person.Name = model.Name;
                person.SurName = model.SurName;
                person.Biography = model.Biography;
                person.BirthDate = model.BirthDate;
                person.Duty = Convert.ToInt32(model.Duty);
                
            }
                _bookcontext.SaveChanges();  
                return RedirectToAction("Index", "Person");
           
          
        }


    }
}
