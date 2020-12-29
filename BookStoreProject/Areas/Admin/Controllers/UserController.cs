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
    public class UserController : BaseController
    {
        private readonly BookContext _bookcontext;
        public UserController(BookContext bookcontext, IMemoryCache memoryCache) : base(bookcontext, memoryCache)
        {
            _bookcontext = bookcontext;
        }


        [RoleControl(EnumRole.UserList)]
        public IActionResult Index()
        {
            List<UserVM> users = _bookcontext.Users.Where(q => q.IsDeleted == false).Select(q => new UserVM()
            {
                UserID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                UserEmail = q.Email,
                UserPassword = q.Password,
                Comments = q.Comments,
                Lastlogindate = q.Lastlogindate,
               
                
                IsDeleted = q.IsDeleted,
                BirthDate = q.BirthDate
            }).ToList();

            return View(users);
        }


        [RoleControl(EnumRole.CommentDetail)]
        public IActionResult CommentDetail(int id)
        {
            List<CommentVM> comments = _bookcontext.Comments.Include(q=>q.User).Include(q=>q.Book).Where(q => q.UserID == id).Select(q => new CommentVM()
            {
                CommentID=q.ID,
                UserID = q.UserID,
                BookName = q.Book.Name,
                Content = q.Content,
                UserName = q.User.Name+" "+q.User.SurName,
                AddDate = q.AddDate,
                UpdateDate = q.UpdateDate,
            }).ToList();

            return View(comments);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            User user = _bookcontext.Users.FirstOrDefault(q => q.ID == id);
           
                user.IsDeleted = true;
                _bookcontext.SaveChanges();

            return RedirectToAction("Index","User");

        }


        
    }
}



