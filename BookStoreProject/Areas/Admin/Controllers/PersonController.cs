using BookStoreProject.Models.Attributes;
using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.Types;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static BookStoreProject.Models.ORM.Entities.Person;

namespace BookStoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController : BaseController
    {
        private readonly BookContext _bookcontext;
        public PersonController(BookContext bookcontext, IMemoryCache memoryCache) : base(bookcontext, memoryCache)
        {
            _bookcontext = bookcontext;
        }

        [RoleControl(EnumRole.PersonList)]
        public IActionResult Index()
        {

            List<PersonVM> people = _bookcontext.People.Where(q => q.IsDeleted == false).Include(q => q.PersonDuties).Select(q => new PersonVM()
            {
                PersonID = q.ID,
                Name = q.Name,
                SurName = q.SurName.ToUpper(),
                Biography = q.Biography,
                BirthDate = q.BirthDate,
                Imagepath = q.Imagepath,
                Duties = q.PersonDuties.Where(q => q.IsDeleted == false).Select(q => q.DutyID == Convert.ToInt32(EnumDuty.Writer) ? EnumDuty.Writer.ToString() : EnumDuty.Interpreter.ToString()).ToList(),
            }).ToList();

            return View(people);
        }


        [RoleControl(EnumRole.PersonAdd)]
        public IActionResult Add()
        {
            PersonVM model = new PersonVM();
            List<EnumDuty> modelduty = new List<EnumDuty>();

            modelduty.Add(EnumDuty.Interpreter);
            modelduty.Add(EnumDuty.Writer);

            model.EnumDuties = modelduty;

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(PersonVM model, int[] dutyarray)
        {
            model.Imagepath = "";

            if (model.Coverimage != null)
            {
                var guid = Guid.NewGuid().ToString();

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                     "wwwroot/Admin/writerimg", guid + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Coverimage.CopyTo(stream);
                }

                model.Imagepath = guid + ".jpg";
            }

            if (ModelState.IsValid)
            {

                Person person = new Person();
                person.Name = model.Name;
                person.SurName = model.SurName;
                person.Biography = model.Biography;
                person.BirthDate = model.BirthDate;
                person.Imagepath = model.Imagepath;

                _bookcontext.People.Add(person);
                _bookcontext.SaveChanges();

                int PersonID = person.ID;

                foreach (var item in dutyarray)
                {
                    PersonDuty personduty = new PersonDuty();

                    personduty.PersonID = PersonID;
                    personduty.DutyID = item;

                    _bookcontext.PeopleDuty.Add(personduty);
                }

                _bookcontext.SaveChanges();

                return Redirect("/Admin/Person/");
            }
            return View();
        }



        public IActionResult BookDetail(int id)
        {
            List<BookVM> books = _bookcontext.BookPeople.Include(q => q.Book).Include(q => q.Person).Where(q => q.PersonID == id).Select(q => new BookVM()
            {
                BookID = q.BookID,
                Name = q.Book.Name,
                Publisher = q.Book.Publisher,
                PublishDate = q.Book.PublishDate,
                //WriterName = q.Person.Name + " " + q.Person.SurName


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



        [RoleControl(EnumRole.PersonEdit)]
        public IActionResult Edit(int id)
        {

            Person person = _bookcontext.People.Include(q => q.PersonDuties).FirstOrDefault(x => x.ID == id);

            PersonVM model = new PersonVM();
            model.PersonID = person.ID;
            model.Name = person.Name;
            model.SurName = person.SurName;
            model.BirthDate = person.BirthDate;
            model.Biography = person.Biography;
            model.Duties = person.PersonDuties.Where(q => q.IsDeleted == false).Select(q => q.DutyID == Convert.ToInt32(EnumDuty.Writer) ? EnumDuty.Writer.ToString() : EnumDuty.Interpreter.ToString()).ToList();
            model.Imagepath = person.Imagepath;

            List<EnumDuty> modelduty = new List<EnumDuty>();

            modelduty.Add(EnumDuty.Interpreter);
            modelduty.Add(EnumDuty.Writer);

            model.EnumDuties = modelduty;
            return View(model);

                    }


        [HttpPost]
        public IActionResult Edit(PersonVM model, int[] dutyarray)
        {
            Person person = _bookcontext.People.Include(q => q.PersonDuties).FirstOrDefault(q => q.ID == model.PersonID);
            if (ModelState.IsValid)
            {

                person.Name = model.Name;
                person.SurName = model.SurName;
                person.Biography = model.Biography;
                person.BirthDate = model.BirthDate;



                if (model.Coverimage != null)

                {
                    model.Imagepath = "";
                    var guid = Guid.NewGuid().ToString();

                    var path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/Admin/writerimg", guid + ".jpg");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        model.Coverimage.CopyTo(stream);
                    }

                    model.Imagepath = guid + ".jpg";

                    person.Imagepath = model.Imagepath;
                }


                _bookcontext.SaveChanges();

                int PersonID = person.ID;

                List<PersonDuty> personduties = person.PersonDuties.ToList();
                foreach (var item in personduties)
                {
                    item.IsDeleted = true;
                }
                foreach (var item in dutyarray)
                {
                    PersonDuty personduty = new PersonDuty();

                    personduty.PersonID = PersonID;
                    personduty.DutyID = item;

                    _bookcontext.PeopleDuty.Add(personduty);
                }


            }
            _bookcontext.SaveChanges();

            return Redirect("/Admin/Person/");
            //Redirect("/SiteAccount/");


        }


    }
}
