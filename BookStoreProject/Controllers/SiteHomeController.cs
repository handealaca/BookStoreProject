using BookStoreProject.Controllers;
using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    //[Authorize]
    public class SiteHomeController : SiteBaseController
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

            model.Topcategories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Getsubcategory (int catalog)
        {
            List<Category> subcategories = _bookcontext.Categories.Where(q => q.IsDeleted == false && q.TopCategory == catalog).ToList();
            return Json(subcategories);
        }

        //[HttpPost]
        //public IActionResult Index(int catalog)
        //{
        //    SiteHomeVM model = new SiteHomeVM();
        //    model.BookCovers = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPerson => BookPerson.Person).Where(q => q.IsDeleted == false).OrderByDescending(q => q.ID).Take(8).ToList();

        //    model.Topcategories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
        //    model.Subcategories= _bookcontext.Categories.Where(q => q.IsDeleted == false && q.TopCategory==catalog).ToList();

        //    return View(model);
        //}
        public IActionResult Books()
        {
            SiteHomeVM model = new SiteHomeVM();
            model.BookCovers = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPerson => BookPerson.Person).Where(q => q.IsDeleted == false).OrderByDescending(q => q.ID).Take(8).ToList();

            model.Topcategories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();

            return View(model);
        }

        

    }
}


