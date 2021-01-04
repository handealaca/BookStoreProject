using BookStoreProject.Models.ORM.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    public class SiteErrorController : SiteBaseController
    {
        private readonly BookContext _bookcontext;
        public SiteErrorController(BookContext bookcontext) : base(bookcontext)
        {
            _bookcontext = bookcontext;
        }
        public IActionResult NoAccess()
        {
            return View();
        }
    }
}
