using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    public class SiteUserController : SiteBaseController
    {
        private readonly BookContext _bookcontext;
        public SiteUserController(BookContext bookcontext, IMemoryCache memoryCache) : base(bookcontext, memoryCache)
        {
            _bookcontext = bookcontext;
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserLoginVM model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Password = model.register.Password;
                user.Email = model.register.Email;

                _bookcontext.Users.Add(user);
                _bookcontext.SaveChanges();

                int userid = user.ID;
                return RedirectToRoute("default", new { controller = "SiteUser", action = "Edit",id= userid });

            }
            return RedirectToRoute("default", new { controller = "SiteLogin", action = "Index" });

        }

        public IActionResult Edit(int id)
        {
            User user = _bookcontext.Users.FirstOrDefault(q => q.ID == id);

            UserVM model = new UserVM();
            model.UserID = user.ID;
            model.Name = user.Name;
            model.SurName = user.SurName;
            model.Email = user.Email;
            model.BirthDate = user.BirthDate;
            model.Password = user.Password;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit (UserVM model)
        {
            if (ModelState.IsValid)
            {
                User user = _bookcontext.Users.FirstOrDefault(q => q.ID == model.UserID);
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;
                user.BirthDate = model.BirthDate;
                user.Password = model.Password;

                _bookcontext.SaveChanges();

                return RedirectToRoute("default", new { controller = "SiteHome", action = "Index"});

            }
            return View();
        }
    }
}
