using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
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
                Password = q.Password,
                Comments = q.Comments,
                AddDate = q.AddDate,
                UpdateDate = q.UpdateDate,
                IsDeleted = q.IsDeleted,
                BirthDate = q.BirthDate
            }).ToList();

            return View(users);
        }


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

            return RedirectToAction("Index");

        }
    }
}



