using BookStoreProject.Areas.Admin.Controllers;
using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    public class SiteHomeController : BaseController
    {
        private readonly BookContext _bookcontext;
        public SiteHomeController(BookContext bookcontext, IMemoryCache memoryCache) : base(bookcontext, memoryCache)
        {
            _bookcontext = bookcontext;
        }

        public IActionResult Index()
        {
            SiteHomeVM model = new SiteHomeVM();
            model.BookCovers = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPerson => BookPerson.Person).Where(q => q.IsDeleted == false).OrderByDescending(q => q.ID).Take(8).ToList();


            return View(model);
        }

        public IActionResult Books()
        {
            return View();
        }
    }
}


   