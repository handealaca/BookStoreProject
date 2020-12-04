using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    public class UserController : Controller
    {
        private readonly BookContext _bookcontext;
        public UserController(BookContext bookContext)
        {
            _bookcontext = bookContext;
        }
        public IActionResult Index()
        {
            List<UserVM> users = _bookcontext.Users.Where(q => q.IsDeleted == false).Select(q => new UserVM()
            {
                UserID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                Email = q.Email,
                Password=q.Password,
                Comments=q.Comments,
                AddDate=q.AddDate,
                IsDeleted = q.IsDeleted,
                BirthDate = q.BirthDate
            }).ToList();

            return View(users);
        }
    }
}
