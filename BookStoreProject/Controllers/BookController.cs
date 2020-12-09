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
    public class BookController : Controller
    {
        private readonly BookContext _bookcontext;
        public BookController(BookContext bookContext)
        {
            _bookcontext = bookContext;
        }
        public IActionResult Index()
        {

            //var data = _bookcontext.Books.Where(q => q.IsDeleted == false).Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ToList();

            List<BookVM> books = _bookcontext.Books.Where(q => q.IsDeleted == false).Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).Select(q => new BookVM()
            {
                BookID = q.ID,
                Name = q.Name,
                //people = q.BookPersons.Select(q => q.Person).ToList(),
                writers = q.BookPersons.Where(q => q.DutyID == 0).Select(q => q.Person).ToList(),
                interpreters = q.BookPersons.Where(q => q.DutyID == 1).Select(q => q.Person).ToList(),
                Publisher = q.Publisher,
                PublishDate = q.PublishDate,
                AddDate = q.AddDate,
                Edition = q.Edition,
                IsDeleted = q.IsDeleted,
                Comments = q.Comments,
                bookCategories=q.BookCategories,
                //categories = q.BookCategories.Select(q => q.Category).ToList(),
                UserPoints = q.UserPoints

            }).ToList();

            //ViewBag.kategorikitap = _bookcontext.BookCategories.Include(q => q.Category).ToList();
            return View(books);
        }
        public IActionResult Add()
        {
            BookVM model = new BookVM();
            model.categories = _bookcontext.Categories.ToList();
            model.people = _bookcontext.People.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(BookVM model,int[] catarray,int[] writerarray,int[] ?interparray)
        {

            Book book = new Book();
            book.Name = model.Name;
            book.Publisher = model.Publisher;
            book.PublishDate = model.PublishDate;
            book.Edition = model.Edition;
            book.UpdateDate = model.UpdateDate;
            book.AddDate = model.AddDate;


            _bookcontext.Books.Add(book);
            _bookcontext.SaveChanges();

            int BookID = book.ID;
            model.categories = _bookcontext.Categories.ToList();
            model.people = _bookcontext.People.ToList();

            foreach (var item in catarray)
            {
                BookCategory bookCategory = new BookCategory();
                bookCategory.CategoryID = item;
                bookCategory.BookID = BookID;

                _bookcontext.BookCategories.Add(bookCategory);
                
            }
            foreach (var item in writerarray)
            {
                BookPerson bookperson = new BookPerson();
                bookperson.PersonID = item;
                bookperson.BookID = BookID;
                bookperson.DutyID = 0;


                _bookcontext.BookPeople.Add(bookperson);

            }
            foreach (var item in interparray)
            {
                BookPerson bookperson = new BookPerson();
                bookperson.PersonID = item;
                bookperson.BookID = BookID;
                bookperson.DutyID = 1;


                _bookcontext.BookPeople.Add(bookperson);

            }


            //foreach (var item in personarray)
            //{
            //    BookPerson bookperson = new BookPerson();
            //    bookperson.PersonID = item;
            //    bookperson.BookID = BookID;


            //    _bookcontext.BookPeople.Add(bookperson);

            //}

            _bookcontext.SaveChanges();


            //return RedirectToAction("Index", "Book");
            return View();

        }


        public IActionResult Edit(int id)
        {
            Book book = _bookcontext.Books.Include(x => x.BookCategories).Include(x => x.BookPersons).FirstOrDefault(q => q.ID == id);

            BookVM model = new BookVM();
            model.BookID = book.ID;
            model.Name = book.Name;
            model.Publisher = book.Publisher;
            model.PublishDate = book.PublishDate;
            model.Edition = book.Edition;
            model.categories = book.BookCategories.Select(q=>q.Category).ToList();
            model.people = book.BookPersons.Select(q => q.Person).ToList();

            return View(model);

        }

    }
}
