using BookStoreProject.Models.Attributes;
using BookStoreProject.Models.ORM.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    //[SiteAuth]
    public class SiteBaseController : Controller
    {
        private readonly BookContext _bookcontext;
        //private readonly IMemoryCache _memoryCache2;

        public SiteBaseController(BookContext bookContext/*, IMemoryCache memoryCache*/)
        {
            _bookcontext = bookContext;
            //_memoryCache2 = memoryCache;

        }




        public override void OnActionExecuting(ActionExecutingContext context2)
        {


            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.Claims.ToArray()[3].Value == "User")
                {
                    //if (!_memoryCache2.TryGetValue(CacheKeys.Entry, out cacheEntry))
                    //{
                    //    // Key not in cache, so get data.
                    //    cacheEntry = DateTime.Now;

                    //    // Set cache options.
                    //    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    //        // Keep in cache for this time, reset time if accessed.
                    //        .SetSlidingExpiration(TimeSpan.FromSeconds(3));

                    //    // Save data in cache.
                    //    _cache.Set(CacheKeys.Entry, cacheEntry, cacheEntryOptions);
                    //}

                   TempData["UserEmail"] = HttpContext.User.Claims.ToArray()[0].Value;

                    TempData["UserId"] = HttpContext.User.Claims.ToArray()[2].Value;

                    TempData["UserName"] = HttpContext.User.Claims.ToArray()[1].Value;
                }

            }

            base.OnActionExecuting(context2);

        }
        public override void OnActionExecuted(ActionExecutedContext context2)
        {
            base.OnActionExecuted(context2);
        }


    }
}
