using BookStoreProject.Models.Attributes;
using BookStoreProject.Models.ORM.Context;
using BookStoreProject.Models.ORM.Entities;
using BookStoreProject.Models.Types;
using BookStoreProject.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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

        public SiteBookVM SiteBookVM { get; private set; }

        public IActionResult Index(int? page)
        {
            var pagenumber = page ?? 1;

            List<Book> modelbook = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPerson => BookPerson.Person).Where(q => q.IsDeleted == false).OrderBy(q => q.Name).ToList();

            List<Category> modelcategory = _bookcontext.Categories.Where(q => q.IsDeleted == false).OrderBy(q => q.CategoryName).ToList();

            List<Person> modelpeople = _bookcontext.People.Include(q => q.BookPeople.Where(q => q.DutyID == 0)).Where(q => q.IsDeleted == false).OrderBy(q=>q.Name).ToList();

            //var pagedsitebook = modelbook.ToPagedList(pagenumber, 10);

            SiteBookVM sitebook = new SiteBookVM();
            //sitebook.books = pagedsitebook.ToList();
            sitebook.Categories = modelcategory;
            sitebook.people = modelpeople;
            sitebook.books = modelbook.ToPagedList(pagenumber,9);


            return View(sitebook);
        }

        [HttpPost]
        public IActionResult Search(string keywords, int? catalog, int? category, int? scategory, int? page)
        {
            var pagenumber = page ?? 1;

            List<Category> modelcategory = _bookcontext.Categories.Where(q => q.IsDeleted == false).OrderBy(q => q.CategoryName).ToList();



            List<Person> modelpeople = _bookcontext.People.Include(q => q.BookPeople.Where(q => q.DutyID == 0)).Where(q => q.IsDeleted == false).OrderBy(q => q.Name).ToList();

            var data = _bookcontext.Books.Include(x => x.BookCategories).ThenInclude(BookCategory => BookCategory.Category).Include(x => x.BookPersons).ThenInclude(BookPerson => BookPerson.Person).OrderBy(q => q.Name).Where(x => x.IsDeleted == false).ToList();



            if (!String.IsNullOrEmpty(keywords))
            {

                data = data.Where(x => x.Name.ToLower().Contains(keywords.ToLower()) || x.BookPersons.Where(x => x.Person.Name.ToLower().Contains(keywords.ToLower())).Any() || x.BookPersons.Where(x => x.Person.SurName.ToLower().Contains(keywords.ToLower())).Any() && x.IsDeleted == false).Where(Book => Book.IsDeleted == false).ToList();

            }

            if (catalog != null)
            {

                data = data.Where(x => x.BookCategories.Where(x => x.Category.TopCategory == catalog || x.CategoryID == catalog).Any())
                    .Where(Book => Book.IsDeleted == false).ToList();


            }

            if (category != null)
            {

                data = data.Where(x => x.BookCategories.Where(x => x.CategoryID == category || x.Category.TopCategory == category).Any())
                     .Where(Book => Book.IsDeleted == false).ToList();


            }

            if (scategory != null)
            {

                data = data.Where(x => x.BookCategories.Where(x => x.CategoryID == scategory).Any())
                     .Where(Book => Book.IsDeleted == false).ToList();


            }


            SiteBookVM sitebook = new SiteBookVM();
            sitebook.books = data.ToPagedList(pagenumber,9);
            sitebook.Categories = modelcategory;
            sitebook.people = modelpeople;



            return View("Index", new SiteBookVM { books = data.ToPagedList(pagenumber,9), Categories = modelcategory, people = modelpeople });

        }


        public IActionResult Listclick(int id,int? page)
        {
            var pagenumber = page ?? 1;

            List<Category> modelcategory = _bookcontext.Categories.Where(q => q.IsDeleted == false).OrderBy(q => q.CategoryName).ToList();

            List<Person> modelpeople = _bookcontext.People.Include(q => q.BookPeople.Where(q => q.DutyID == 0)).Where(q => q.IsDeleted == false).OrderBy(q => q.Name).ToList();

            List<Book> data = _bookcontext.Books.Include(x => x.BookCategories).ThenInclude(BookCategory => BookCategory.Category).Include(x => x.BookPersons).ThenInclude(BookPerson => BookPerson.Person).OrderBy(q => q.Name).Where(x => x.IsDeleted == false).ToList();

            data = data.Where(x => x.BookCategories.Where(x => x.Category.TopCategory == id || x.CategoryID == id).Any())
                    .Where(Book => Book.IsDeleted == false).ToList();


            SiteBookVM sitebook = new SiteBookVM();
            sitebook.books = data.ToPagedList(pagenumber, 9);
            sitebook.Categories = modelcategory;
            sitebook.people = modelpeople;

            return View("Index", new SiteBookVM { books = data.ToPagedList(pagenumber, 9), Categories = modelcategory, people = modelpeople });

        }

        public IActionResult WriterListclick(int id, int? page)
        {
            var pagenumber = page ?? 1;
            List<Category> modelcategory = _bookcontext.Categories.Where(q => q.IsDeleted == false).OrderBy(q => q.CategoryName).ToList();

            List<Person> modelpeople = _bookcontext.People.Include(q => q.BookPeople.Where(q => q.DutyID == 0)).Where(q => q.IsDeleted == false).OrderBy(q => q.Name).ToList();

            List<Book> data = _bookcontext.Books.Include(x => x.BookCategories).ThenInclude(BookCategory => BookCategory.Category).Include(x => x.BookPersons).ThenInclude(BookPerson => BookPerson.Person).OrderBy(q => q.Name).Where(x => x.IsDeleted == false).ToList();

            data = data.Where(x => x.BookPersons.Where(x => x.PersonID == id).Any())
                    .Where(Book => Book.IsDeleted == false).ToList();


            SiteBookVM sitebook = new SiteBookVM();
            sitebook.books = data.ToPagedList(pagenumber, 9);
            sitebook.Categories = modelcategory;
            sitebook.people = modelpeople;

            return View("Index", new SiteBookVM { books = data.ToPagedList(pagenumber, 9), Categories = modelcategory, people = modelpeople });

        }


        [SiteAuth]
        public IActionResult BookDetail(int id)
        {
            List<Category> modelcategory = _bookcontext.Categories.Where(q => q.IsDeleted == false).OrderBy(q => q.CategoryName).ToList();

            Book modelbook = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons.Where(q => q.IsDeleted == false)).ThenInclude(BookPersons => BookPersons.Person).Include(q => q.Comments.Where(q => q.IsDeleted == false && q.BookID == id)).ThenInclude(Comments => Comments.User).Include(q => q.UserPoints.Where(q => q.IsDeleted == false)).ThenInclude(UserPoints => UserPoints.USer).FirstOrDefault(q => q.ID == id);

            List<Person> modelpeople = _bookcontext.People.Include(q => q.BookPeople.Where(q => q.DutyID == 0)).Where(q => q.IsDeleted == false).OrderBy(q => q.Name).ToList();

            

            SiteBookVM sitebook = new SiteBookVM();
            sitebook.Bookdetail = modelbook;
            sitebook.Categories = modelcategory;
            sitebook.people = modelpeople;
            return View(sitebook);
        }


        [HttpPost]
        public IActionResult AddComment(SiteBookVM model)
        {

            Comment comment = new Comment();
            comment.UserID = Convert.ToInt32(TempData["UserID"]);
            comment.Header = model.Comment.Header;
            comment.Content = model.Comment.Content;
            comment.BookID = model.Bookdetail.ID;


            _bookcontext.Comments.Add(comment);
            _bookcontext.SaveChanges();
           

            return RedirectToAction("BookDetail", "SiteBook", new { id = model.Bookdetail.ID });
        }


        public IActionResult EditComment(int id)
        {
            Comment modelcomment = _bookcontext.Comments.FirstOrDefault(q => q.ID == id);

            List<Category> modelcategory = _bookcontext.Categories.Where(q => q.IsDeleted == false).OrderBy(q => q.CategoryName).ToList();

            Book modelbook = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons.Where(q => q.IsDeleted == false)).ThenInclude(BookPersons => BookPersons.Person).Include(q => q.Comments.Where(q => q.IsDeleted == false && q.BookID == modelcomment.BookID)).ThenInclude(Comments => Comments.User).Include(q => q.UserPoints.Where(q => q.IsDeleted == false)).ThenInclude(UserPoints => UserPoints.USer).FirstOrDefault(q => q.ID == modelcomment.BookID);

            List<Person> modelpeople = _bookcontext.People.Include(q => q.BookPeople.Where(q => q.DutyID == 0)).Where(q => q.IsDeleted == false).OrderBy(q => q.Name).ToList();

            SiteBookVM sitebook = new SiteBookVM();
            sitebook.Bookdetail = modelbook;
            sitebook.Categories = modelcategory;
            sitebook.people = modelpeople;
            sitebook.Comment = modelcomment;

            return View("BookDetail", new SiteBookVM { Bookdetail = modelbook, Categories = modelcategory, people = modelpeople, Comment=modelcomment });
        }


        [HttpPost]
        public IActionResult EditComment(SiteBookVM model)
        {
            Comment comment = _bookcontext.Comments.FirstOrDefault(q => q.ID == model.Comment.ID);
            comment.Header = model.Comment.Header;
            comment.Content = model.Comment.Content;
            comment.UpdateDate = DateTime.Now;

            _bookcontext.SaveChanges();

            return RedirectToAction("BookDetail", "SiteBook", new { id = comment.BookID });

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

            Book book = _bookcontext.Books.Include(q => q.UserPoints).Where(q => q.IsDeleted == false).FirstOrDefault(q => q.ID == bookid);

            int totalpoint = book.TotalPoint + userpoint.Point;
            book.TotalPoint = totalpoint;

            _bookcontext.SaveChanges();

            double rated = book.UserPoints.Count();
            double avrpoint = totalpoint / rated;
            double average = Math.Round(avrpoint, 0, MidpointRounding.AwayFromZero);

            book.AvrPoint = average;
            _bookcontext.SaveChanges();

            return Json(average);

        }

    }
}
