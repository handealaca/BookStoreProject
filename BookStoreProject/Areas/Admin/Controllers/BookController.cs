﻿using BookStoreProject.Models.Attributes;
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

namespace BookStoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : BaseController
    {
        private readonly BookContext _bookcontext;
        public BookController(BookContext bookcontext, IMemoryCache memoryCache): base(bookcontext, memoryCache)
        {
            _bookcontext = bookcontext;
        }


        [RoleControl(EnumRole.BookList)]
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
                Edition = q.Edition,
                Imagepath=q.Imagepath,

                BookCategories = q.BookCategories.Where(q => q.IsDeleted == false).ToList(),


            }).ToList();

            return View(books);
        }

        //[RoleControl(EnumRole.BookAdd)]
        public IActionResult Add()
        {
            BookVM model = new BookVM();
            model.categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
            model.people = _bookcontext.People.Where(q => q.IsDeleted == false).ToList();
            return View(model);
        }


        [HttpPost]
        public IActionResult Add(BookVM model, int[] categories, int[] people, int[]? interparray)
        {
            model.Imagepath = "";

            if (model.Coverimage != null)
            {
                var guid = Guid.NewGuid().ToString();

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/Admin/Coverimg", guid + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Coverimage.CopyTo(stream);
                }

                model.Imagepath = guid + ".jpg";
            }


            if (ModelState.IsValid)
            {
                Book book = new Book();
                book.Name = model.Name;
                book.Publisher = model.Publisher;
                book.PublishDate = model.PublishDate;
                book.Imagepath = model.Imagepath;
                book.Edition = model.Edition;
                book.Summary = model.Summary;

                _bookcontext.Books.Add(book);
                _bookcontext.SaveChanges();

                int BookID = book.ID;
                foreach (var item in categories)
                {
                    BookCategory bookCategory = new BookCategory();
                    bookCategory.CategoryID = item;
                    bookCategory.BookID = BookID;

                    _bookcontext.BookCategories.Add(bookCategory);
                }

                foreach (var item in people)
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

                return Redirect("/Admin/Book/");

            }
            
            else
            {
                ViewBag.categorybag = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
                ViewBag.personbag = _bookcontext.People.Where(q => q.IsDeleted == false).ToList();

                return View();
            }
           
        }


        //[RoleControl(EnumRole.BookEdit)]
        public IActionResult Edit(int id)
        {
            Book book = _bookcontext.Books.Include(x => x.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(x => x.BookPersons).ThenInclude(BookPersons => BookPersons.Person).FirstOrDefault(q => q.ID == id);

            BookVM model = new BookVM();
            model.BookID = book.ID;
            model.Name = book.Name;
            model.Publisher = book.Publisher;
            model.PublishDate = book.PublishDate;
            model.Edition = book.Edition;
            model.BookCategories = book.BookCategories.Where(q => q.IsDeleted == false).ToList();
            model.categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
            model.BookPersons = book.BookPersons.Where(q => q.IsDeleted == false).ToList();
            model.people = _bookcontext.People.Where(q => q.IsDeleted == false).ToList();
            model.Imagepath = book.Imagepath;
            model.Summary = book.Summary;

            return View(model);
        }



        [HttpPost]
        public IActionResult Edit(BookVM model, int[] categories, int[] people, int[]? interparray)
        {
            Book book = _bookcontext.Books.FirstOrDefault(q => q.ID == model.BookID);

            book.Name = model.Name;
            book.Publisher = model.Publisher;
            book.PublishDate = model.PublishDate;
            book.Edition = model.Edition;
            book.Summary = model.Summary;

            if (model.Coverimage != null)
            {
                model.Imagepath = "";
                
                var guid = Guid.NewGuid().ToString();

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/Admin/Coverimg", guid + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Coverimage.CopyTo(stream);
                }

                model.Imagepath = guid + ".jpg";
                
                
            book.Imagepath = model.Imagepath;

            }
                     

            _bookcontext.SaveChanges();

            int BookID = book.ID;


            List<BookCategory> bookcategories = _bookcontext.BookCategories.Where(q => q.BookID == model.BookID).ToList();

            foreach (var item in bookcategories)
            {
                _bookcontext.BookCategories.Remove(item);
            }

            foreach (var item in categories)
            {
                BookCategory bookCategory = new BookCategory();
                bookCategory.CategoryID = item;
                bookCategory.BookID = BookID;

                _bookcontext.BookCategories.Add(bookCategory);
            }


            List<BookPerson> bookpeople = _bookcontext.BookPeople.Where(q => q.BookID == model.BookID).ToList();

            foreach (var item in bookpeople)
            {
                item.IsDeleted = true;
                _bookcontext.BookPeople.Remove(item);
            }

            _bookcontext.SaveChanges();

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
                BookPerson bookPerson1 = new BookPerson();
                bookPerson1.PersonID = item;
                bookPerson1.BookID = BookID;
                bookPerson1.DutyID = 1;

                _bookcontext.BookPeople.Add(bookPerson1);
            }

            _bookcontext.SaveChanges();

            return Redirect("/Admin/Book/");
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
