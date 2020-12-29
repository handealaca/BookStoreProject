using BookStoreProject.Models.Attributes;
using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Areas.Admin.Controllers
{
    [AdminAuth]
    public class BaseController : Controller
    {
        private readonly BookContext _bookcontext;
        private readonly IMemoryCache _memoryCache;

        public BaseController(BookContext bookContext, IMemoryCache memoryCache)
        {
            _bookcontext = bookContext;
            _memoryCache = memoryCache;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.Claims.ToArray()[3].Value == "Admin")
                {
                    List<AdminMenu> menus = new List<AdminMenu>();

                    bool isExist = _memoryCache.TryGetValue("adminmenus", out menus);

                    if (!isExist)
                    {

                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60));


                        menus = _bookcontext.AdminMenus.ToList();

                        _memoryCache.Set("adminmenus", menus, cacheEntryOptions);
                    }


                    ViewBag.Email = HttpContext.User.Claims.ToArray()[0].Value;
                    ViewBag.Name = HttpContext.User.Claims.ToArray()[1].Value;
                    ViewBag.menus = menus;
                }

            }

            base.OnActionExecuting(context);

        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
