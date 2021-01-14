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

            List<CategoryVM> model2 = _bookcontext.Categories.Where(q => q.IsDeleted == false).Select(q => new CategoryVM()
            {

                CategoryName = q.CategoryName,
                CategoryID = q.ID,
                TopCategory = q.TopCategory,



            }).ToList();

            SiteBookVM sitebook = new SiteBookVM();
            sitebook.BookVM = model;
            sitebook.categories = model2;


            sitebook.Categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();
            sitebook.topcategories = _bookcontext.Categories.Where(q => q.TopCategory == 0).ToList();
            sitebook.subcategories = _bookcontext.Categories.Where(q => q.TopCategory != null).ToList();

            return View(sitebook);
        }

       
        public IActionResult Search(string keywords, int? catalog, int? category)
        {
             var data = _bookcontext.Books.Include(x => x.BookCategories).ThenInclude(BookCategory => BookCategory.Category).Include(x => x.BookPersons).ThenInclude(BookPerson => BookPerson.Person).OrderBy(q => q.Name).Where(x => x.IsDeleted == false).ToList();
            //List<BookPerson> data = _bookcontext.BookPeople.Where(q => q.IsDeleted == false && q.Person.Name == keywords || q.Person.SurName == keywords).Include(q => q.Person).Include(q => q.Book).ThenInclude(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).ToList();
            //List<Person> data1 = _bookcontext.People.ToList();
            //List<Category> data2 = _bookcontext.Categories.ToList();

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

                data = data.Where(x => x.BookCategories.Where(x => x.CategoryID == category).Any())
                     .Where(Book => Book.IsDeleted == false).ToList();


            }
            var SiteBookVM = new SiteBookVM

            {
                books = data
               
            };
            SiteBookVM.Categories = _bookcontext.Categories.Where(q=> q.TopCategory == catalog && q.IsDeleted == false ).ToList();
            SiteBookVM.topcategories = _bookcontext.Categories.Where(q => q.TopCategory == 0).ToList();
            //SiteBookVM.subcategories = _bookcontext.Categories.Where(q => q.TopCategory == catalog && q.SubCategory == category).ToList();



            return View(SiteBookVM);



            //return View(data);

            //if (!String.IsNullOrEmpty(keywords))
            //{
            //    List<BookVM> books = _bookcontext.Books.Where(q => q.Name.Contains(keywords) && q.IsDeleted == false).Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPerson => BookPerson.Person).OrderBy(q => q.Name).Select(q => new BookVM()
            //    {
            //        BookID = q.ID,
            //        Name = q.Name,
            //        PublishDate = q.PublishDate,
            //        Publisher = q.Publisher,
            //        Edition = q.Edition,
            //        Imagepath = q.Imagepath,
            //        BookPersons = q.BookPersons.Where(q => q.IsDeleted == false).ToList(),
            //        UserPoints = q.UserPoints.Where(q => q.IsDeleted == false).ToList(),
            //        categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList()

            //    }).ToList();



            //    List<PersonVM> people = _bookcontext.People.Where(q => q.Name.Contains(keywords) || q.SurName.Contains(keywords) && q.IsDeleted == false).Include(q => q.BookPeople).ThenInclude(q => q.Book).OrderBy(q => q.Name).Select(q => new PersonVM()
            //    {
            //        PersonID = q.ID,
            //        Name = q.Name,
            //        SurName = q.SurName,
            //        Biography = q.Biography,
            //        BirthDate = q.BirthDate,
            //        Imagepath = q.Imagepath,
            //        BookPeople = q.BookPeople.Where(q => q.IsDeleted == false).ToList(),
            //        Duties = q.PersonDuties.Where(q => q.IsDeleted == false).Select(q => q.DutyID == Convert.ToInt32(EnumDuty.Writer) ? EnumDuty.Writer.ToString() : EnumDuty.Interpreter.ToString()).ToList(),

            //    }).ToList();



            //    List<BookCategory> categories = _bookcontext.BookCategories.Where(q => q.Category.CategoryName.Contains(keywords) && q.IsDeleted == false).Include(q => q.Book).ThenInclude(q => q.BookPersons).ThenInclude(q => q.Person).ToList();

            //    List<CategoryVM> model2 = _bookcontext.Categories.Where(q => q.IsDeleted == false).Select(q => new CategoryVM()
            //    {

            //        CategoryName = q.CategoryName,
            //        CategoryID = q.ID,
            //        TopCategory = q.TopCategory,



            //    }).ToList();

            //    SiteBookVM model = new SiteBookVM();
            //    model.BookVM = books;
            //    model.bookcategories = categories;
            //    model.PersonVM = people;
            //    model.categories = model2;

            //    return View("Index", new SiteBookVM { BookVM = books, bookcategories = categories , PersonVM = people, categories = model2 });
            //}

            //else
            //if (catalog != null || category != null)
            //{
            //    List<CategoryVM> categories = _bookcontext.Categories.Where(q => q.ID == catalog || q.TopCategory==catalog || q.ID == category).Include(q => q.BookCategories).ThenInclude(q => q.Book).ThenInclude(q => q.BookPersons).ThenInclude(q => q.Person).Select(q => new CategoryVM()
            //    {

            //        CategoryName = q.CategoryName,
            //        CategoryID = q.ID,
            //        TopCategory = q.TopCategory,

            //        Books = q.BookCategories



            //    }).ToList();


            //    SiteBookVM model = new SiteBookVM();
            //    model.CategoryVM = categories;


            //    return RedirectToAction("Index");
            //}
            //{
            //    return RedirectToAction("NoAccess", "SiteError");
            //}
        
        }


        [SiteAuth]
        public IActionResult BookDetail(int id)
        {
            Book book = _bookcontext.Books.Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPersons => BookPersons.Person).Include(q => q.Comments).ThenInclude(Comments => Comments.User).Include(q => q.UserPoints).ThenInclude(UserPoints => UserPoints.USer).FirstOrDefault(q => q.ID == id);

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
            model.Comments = book.Comments.Where(q => q.IsDeleted == false && q.BookID == id).ToList();
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


        public IActionResult EditComment(int id)
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
        public IActionResult EditComment(CommentVM model)
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
