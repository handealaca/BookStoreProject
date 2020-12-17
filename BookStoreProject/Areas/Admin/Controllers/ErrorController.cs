using BookStoreProject.Models.ORM.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ErrorController : BaseController
    {
        private readonly BookContext _bookcontext;
        public ErrorController(BookContext bookcontext, IMemoryCache memoryCache) : base(bookcontext, memoryCache)
        {
            _bookcontext = bookcontext;
        }
        public IActionResult NoAccess()
        {
            return View();
        }
    }
}
