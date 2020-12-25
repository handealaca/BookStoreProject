using BookStoreProject.Models.Attributes;
using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.Types;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        //[RoleControl(EnumRole.AdminUserList)]
        public IActionResult Index()
        {
            List<AdminUserVM> admins = _bookcontext.AdminUsers.Where(q => q.IsDeleted == false).Select(q => new AdminUserVM()
            {
                UserID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                role = q.Role,
                Email = q.Email,
                Password = q.Password,
               LoginDate = q.Lastlogindate,
               
                IsDeleted = q.IsDeleted,
            }).ToList();


            List<EnumRole> role = new List<EnumRole>();
            role = Enum.GetValues(typeof(EnumRole)).Cast<EnumRole>().ToList();


            foreach (var item in admins)
            {
                item.rolelist = new List<string>();

                foreach (var data in role)
                {
                    if (item.role != null)
                    {
                        string[] userroles = item.role.Split(';');
                        foreach (var x in userroles)
                        {
                            if (!String.IsNullOrEmpty(x))
                            {
                                if (Convert.ToInt32(x) == Convert.ToInt32(data))
                                {
                                    item.rolelist.Add(data.ToString());
                                    //item.roleview = item.roleview + " - " + data.ToString();
                                }
                            }
                        }
                    }

                }

            }

            return View(admins);
        }

        

        [RoleControl(EnumRole.AdminUserAdd)]
        public IActionResult Add()
        {
            AdminUserVM model = new AdminUserVM();
            List<EnumRole> role = new List<EnumRole>();

            role = Enum.GetValues(typeof(EnumRole))
                                     .Cast<EnumRole>()
                                     .ToList();
            model.roles = role;

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AdminUserVM model, int[] rolearray)
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

                string rolenames = "";

                foreach (var item in rolearray)
                {
                    rolenames = rolenames + item.ToString() + ";";
                }
                adminUser.Role = rolenames;

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

