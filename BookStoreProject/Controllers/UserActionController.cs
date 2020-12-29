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
    public class UserActionController : SiteBaseController
    {
        private readonly BookContext _bookcontext;
        public UserActionController(BookContext bookcontext) : base(bookcontext)
        {
            _bookcontext = bookcontext;
        }

        public IActionResult Edit(int id)
        {
            User user = _bookcontext.Users.FirstOrDefault(q => q.ID == id);

            UserVM model = new UserVM();
            model.UserID = user.ID;
            model.Name = user.Name;
            model.SurName = user.SurName;
            model.UserEmail = user.Email;
            model.BirthDate = user.BirthDate;
            model.UserPassword = user.Password;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserVM model)
        {
            ViewBag.UserName = null;
            ViewBag.Email = null;

            if (ModelState.IsValid)
            {
                User user = _bookcontext.Users.FirstOrDefault(q => q.ID == model.UserID);
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.UserEmail;
                user.BirthDate = model.BirthDate;
                user.Password = model.UserPassword;

                _bookcontext.SaveChanges();

                return RedirectToRoute("default", new { controller = "SiteHome", action = "Index" });

            }
            return View();
        }


     

       

    }

}
