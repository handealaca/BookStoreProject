using BookStoreProject.Models.Attributes;
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

namespace BookStoreProject.Controllers
{
    [SiteAuth]
    public class SitePersonController : SiteBaseController
    {
        private readonly BookContext _bookcontext;
        public SitePersonController(BookContext bookcontext) : base(bookcontext)
        {
            _bookcontext = bookcontext;
        }
        public IActionResult Index()
        {
            List<PersonVM> model = _bookcontext.People.Include(q => q.BookPeople).ThenInclude(BookPeople => BookPeople.Book).Where(q => q.IsDeleted == false).OrderBy(q => q.Name).Select(q => new PersonVM()
            {
                PersonID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                Biography = q.Biography,
                BirthDate = q.BirthDate,
                Imagepath = q.Imagepath,
                BookPeople = q.BookPeople.Where(q => q.IsDeleted == false).ToList(),
                Duties = q.PersonDuties.Where(q => q.IsDeleted == false).Select(q => q.DutyID == Convert.ToInt32(EnumDuty.Writer) ? EnumDuty.Writer.ToString() : EnumDuty.Interpreter.ToString()).ToList(),

            }).ToList();

            //model.Categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
            //sitebook.topcategories = _bookcontext.Categories.Where(q => q.TopCategory == 0 && q.IsDeleted == false).ToList();
            return View(model);
        }

        public IActionResult PersonDetail(int id)
        {
            Person person = _bookcontext.People.Include(q => q.BookPeople).ThenInclude(BookPeople => BookPeople.Book).ThenInclude(BookPeople => BookPeople.BookPersons).Include(q => q.PersonDuties).FirstOrDefault(q => q.ID == id);

            PersonVM model = new PersonVM();
            model.Name = person.Name;
            model.SurName = person.SurName;
            model.BirthDate = person.BirthDate;
            model.Biography = person.Biography;
            model.Duties = person.PersonDuties.Where(q => q.IsDeleted == false).Select(q => q.DutyID == Convert.ToInt32(EnumDuty.Writer) ? EnumDuty.Writer.ToString() : EnumDuty.Interpreter.ToString()).ToList();
            model.Imagepath = person.Imagepath;
            model.BookPeople = person.BookPeople;
            
            

           

            return View(model);
        }
    }
}
