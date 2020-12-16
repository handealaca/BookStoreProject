using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : BaseController
    {
        private readonly BookContext _bookcontext;
        public AdminUserController(BookContext bookcontext, IMemoryCache memoryCache) : base(bookcontext, memoryCache)
        {
            _bookcontext = bookcontext;
        }

        public IActionResult Index()
        {
            List<AdminUserVM> admins = _bookcontext.AdminUsers.Where(q => q.IsDeleted == false).Select(q => new AdminUserVM()
            {
                UserID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
               
                Email = q.Email,
                Password = q.Password,
               
                AddDate = q.AddDate,
                UpdateDate = q.UpdateDate,
                IsDeleted = q.IsDeleted,
               
            }).ToList();

            return View(admins);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AdminUserVM model)
        {
            if (ModelState.IsValid)
            {
                AdminUser adminUser = new AdminUser();
                adminUser.Name = model.Name;
                adminUser.SurName = model.SurName;
                adminUser.Email = model.Email;
                adminUser.Password = model.Password;

                _bookcontext.AdminUsers.Add(adminUser);
                _bookcontext.SaveChanges();
                return RedirectToAction("Index", "AdminUser");
            }
            else
            {
                return View();
            }

        }
    }

}

