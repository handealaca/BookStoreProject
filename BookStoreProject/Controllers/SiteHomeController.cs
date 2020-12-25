using BookStoreProject.Areas.Admin.Controllers;
using BookStoreProject.Models.ORM.Context;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public IActionResult Books()
        {
            return View();
        }
    }
}


   