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

            List<BookVM> books = _bookcontext.Books.Where(q => q.IsDeleted == false).Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPerson => BookPerson.Person).Select(q => new BookVM()
            {
                BookID = q.ID,
                Name = q.Name,
                BookPersons = q.BookPersons.Where(q => q.IsDeleted == false).ToList(),
                Publisher = q.Publisher,
                PublishDate = q.PublishDate,
                AddDate = q.AddDate,
                Edition = q.Edition,
                IsDeleted = q.IsDeleted,

                bookCategories = q.BookCategories.Where(q => q.IsDeleted == false).ToList(),


            }).ToList();

            return View(books);
        }
        public IActionResult Add()
        {
            BookVM model = new BookVM();
            model.categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
            model.people = _bookcontext.People.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(BookVM model, int[] catarray, int[] writerarray, int[]? interparray)
        {
           
            if (ModelState.IsValid)
            {
                Book book = new Book();
                book.Name = model.Name;
                book.Publisher = model.Publisher;
                book.PublishDate = model.PublishDate;
                book.Edition = model.Edition;
                //book.UpdateDate = model.UpdateDate;
                //book.AddDate = model.AddDate;

                _bookcontext.Books.Add(book);
                _bookcontext.SaveChanges();

                int BookID = book.ID;
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
                _bookcontext.SaveChanges();
                
                return RedirectToAction("Index", "Book");

            }
            
            else
            {
                ViewBag.categorybag = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
                ViewBag.personbag = _bookcontext.People.Where(q => q.IsDeleted == false).ToList();

                return View();
            }
           
        }


        public IActionResult Edit(int id)
        {
            Book book = _bookcontext.Books.Include(x => x.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(x => x.BookPersons).ThenInclude(BookPersons => BookPersons.Person).FirstOrDefault(q => q.ID == id);

            BookVM model = new BookVM();
            model.BookID = book.ID;
            model.Name = book.Name;
            model.Publisher = book.Publisher;
            model.PublishDate = book.PublishDate;
            model.Edition = book.Edition;
            model.bookCategories = book.BookCategories.Where(q => q.IsDeleted == false).ToList();
            model.categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
            model.BookPersons = book.BookPersons.Where(q => q.IsDeleted == false).ToList();
            model.people = _bookcontext.People.Where(q => q.IsDeleted == false).ToList();

            return View(model);
        }



        [HttpPost]
        public IActionResult Edit(BookVM model, int[] categories, int[] people, int[]? interparray)
        {
            Book book = _bookcontext.Books.Include(x => x.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(x => x.BookPersons).FirstOrDefault(q => q.ID == model.BookID);

            book.Name = model.Name;
            book.Publisher = model.Publisher;
            book.PublishDate = model.PublishDate;
            book.Edition = model.Edition;

            _bookcontext.SaveChanges();

            int BookID = book.ID;

            List<BookCategory> bookcategories = book.BookCategories.ToList();

            foreach (var item in bookcategories)
            {
                item.IsDeleted = true;
            }

            foreach (var item in categories)
            {
                BookCategory bookCategory = new BookCategory();
                bookCategory.CategoryID = item;
                bookCategory.BookID = BookID;

                _bookcontext.BookCategories.Add(bookCategory);
            }


            List<BookPerson> bookpeople = book.BookPersons.ToList();

            foreach (var item in bookpeople)
            {
                item.IsDeleted = true;
            }

            foreach (var item in people)
            {
                BookPerson bookPerson = new BookPerson();
                bookPerson.PersonID = item;
                bookPerson.BookID = BookID;
                bookPerson.DutyID = 0;

                _bookcontext.BookPeople.Add(bookPerson);
            }

            foreach (var item in interparray)
            {
                BookPerson bookPerson = new BookPerson();
                bookPerson.PersonID = item;
                bookPerson.BookID = BookID;
                bookPerson.DutyID = 1;

                _bookcontext.BookPeople.Add(bookPerson);
            }

            _bookcontext.SaveChanges();

            return RedirectToAction("Index", "Book");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            Book book = _bookcontext.Books.FirstOrDefault(x => x.ID == id);
            book.IsDeleted = true;

            _bookcontext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
