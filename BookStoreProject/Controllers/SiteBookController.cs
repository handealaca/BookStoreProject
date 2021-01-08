﻿using BookStoreProject.Models.Attributes;
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
    [SiteAuth]
    public class SiteBookController : SiteBaseController
    {

        private readonly BookContext _bookcontext;
        public SiteBookController(BookContext bookcontext) : base(bookcontext)
        {
            _bookcontext = bookcontext;
        }


        public IActionResult Index()
        {

            List<BookVM> model = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPerson => BookPerson.Person).Where(q => q.IsDeleted == false).OrderByDescending(q => q.ID).Select(q => new BookVM()
            {
                BookID = q.ID,
                Name = q.Name,
                PublishDate = q.PublishDate,
                Publisher = q.Publisher,
                Edition = q.Edition,
                Imagepath = q.Imagepath,
                BookPersons = q.BookPersons.Where(q => q.IsDeleted == false).ToList(),
                UserPoints = q.UserPoints.Where(q => q.IsDeleted == false).ToList(),
                categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList()

            }).ToList();

            List<CategoryVM> model2 = _bookcontext.Categories.Where(q => q.IsDeleted == false).Select(q => new CategoryVM() { 
            
            CategoryName = q.CategoryName,
            CategoryID = q.ID,
            TopCategory = q.TopCategory,
            
                      
            
            }).ToList();

            SiteBookVM sitebook = new SiteBookVM();
            sitebook.BookVM = model;
            sitebook.CategoryVM = model2;

            return View(sitebook);
        }

        
       [SiteAuth]
        public IActionResult BookDetail(int id)
        {
            Book book = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPersons => BookPersons.Person).Include(q=>q.Comments).ThenInclude(Comments=>Comments.User).Include(q=>q.UserPoints).ThenInclude(UserPoints => UserPoints.USer).FirstOrDefault(q => q.ID == id);

            BookVM model = new BookVM();
            model.BookID = book.ID;
            model.Name = book.Name;
            model.PublishDate = book.PublishDate;
            model.Publisher = book.Publisher;
            model.Edition = book.Edition;
            model.Imagepath = book.Imagepath;
            model.UserPoints = book.UserPoints;
            model.AvrPoint = book.AvrPoint;

            model.BookCategories = book.BookCategories.Where(q => q.IsDeleted == false).ToList();
           
            //string []joined = string.Join(",", book.BookCategories.Where(q => q.IsDeleted == false).ToList()).ToArray();
            //model.BookCategories = joined;
           
           
            model.BookPersons = book.BookPersons.Where(q => q.IsDeleted == false).ToList();
            model.Comments = book.Comments.Where(q => q.IsDeleted == false && q.BookID==id).ToList();
            model.UserPoints = book.UserPoints.Where(q => q.IsDeleted == false).ToList();

            return View(model);
        }


        [HttpPost]
        public IActionResult AddComment(CommentVM model)
        {

            Comment comment = new Comment();
            comment.UserID = Convert.ToInt32(TempData["UserID"]);
            comment.Header = model.Header;
            comment.Content = model.Content;
            comment.BookID = model.BookID;
           

            _bookcontext.Comments.Add(comment);
            _bookcontext.SaveChanges();

            return RedirectToAction("BookDetail", "SiteBook", new { id = model.BookID });
        }


        public IActionResult EditComment (int id)
        {
            Comment comment = _bookcontext.Comments.FirstOrDefault(q => q.ID == id);

            CommentVM model = new CommentVM();
            model.Header = comment.Header;
            model.Content = comment.Content;
            model.BookID = comment.BookID;
            model.CommentID = comment.ID;
            //return View("BookDetail", model);

            return View(model);
        }


        [HttpPost]
        public IActionResult EditComment (CommentVM model)
        {
            Comment comment = _bookcontext.Comments.FirstOrDefault(q => q.ID == model.CommentID);
            comment.Header = model.Header;
            comment.Content = model.Content;
            comment.BookID = model.BookID;
            comment.UserID = Convert.ToInt32(TempData["UserID"]);
            comment.UpdateDate = DateTime.Now;

            _bookcontext.SaveChanges();

            return RedirectToAction("BookDetail", "SiteBook", new { id = model.BookID });

        }

        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            Comment comment = _bookcontext.Comments.FirstOrDefault(q => q.ID == id);

          
            comment.IsDeleted = true;

            _bookcontext.SaveChanges();


            return Redirect("/SiteBook/BookDetail/" + comment.BookID);

        }

        [HttpPost]
        public IActionResult AddPoint(int bookid, int point)
        {
            UserPoint userpoint = new UserPoint();
            userpoint.BookID = bookid;
            userpoint.Point = point;
            userpoint.UserID = Convert.ToInt32(TempData["UserID"]);

            _bookcontext.UserPoints.Add(userpoint);
            _bookcontext.SaveChanges();

            int userpointid = userpoint.ID;

            Book book = _bookcontext.Books.Include(q=>q.UserPoints).Where(q => q.IsDeleted == false).FirstOrDefault(q => q.ID == bookid);

            int totalpoint= book.TotalPoint + userpoint.Point;
            book.TotalPoint = totalpoint;

            _bookcontext.SaveChanges();

            double rated = book.UserPoints.Count();
            double avrpoint = totalpoint/rated;
            double average = Math.Round(avrpoint, 0, MidpointRounding.AwayFromZero);

            book.AvrPoint = average;
            _bookcontext.SaveChanges();

            return Json(average);

        }

    }
}
