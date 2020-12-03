using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
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
            List<BookVM> books = _bookcontext.Books.Where(q => q.IsDeleted == false).Select(w => new BookVM()
            {
                BookID = w.ID,
                Name  = w.Name,
                Publisher = w.Publisher,
                PublishDate = w.PublishDate,
                AddDate = w.AddDate,
                Edition = w.Edition,
                IsDeleted = w.IsDeleted
               
            }).ToList();
            return View(books);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookVM model)
        {
            Book book = new Book();
            book.Name = model.Name;
            book.Publisher = model.Publisher;
            book.PublishDate = model.PublishDate;
            book.Edition = model.Edition;
           

            ////Şimdilik adminuserid yi statik olarak veriyoruz
            //blog.AdminUserID = 1;

            _bookcontext.Books.Add(book);
            _bookcontext.SaveChanges();

            return RedirectToAction("Add","Book");


        }
    }
}
