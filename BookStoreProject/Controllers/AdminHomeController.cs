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
    public class AdminHomeController : Controller
    {
        private readonly BookContext _bookcontext;
        public AdminHomeController(BookContext bookContext)
        {
            _bookcontext = bookContext;
        }
        public IActionResult Index()
        {
            List<Book> books = _bookcontext.Books.Where(q => q.IsDeleted == false).ToList();
            List<User> users = _bookcontext.Users.Where(q => q.IsDeleted == false).ToList();
            List<Person> people = _bookcontext.People.Where(q => q.IsDeleted == false).ToList();
            List<Category> categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).ToList();

            AdminPanelVM adminpanel = new AdminPanelVM();
            adminpanel.Books = books;
            adminpanel.Categories = categories;
            adminpanel.People = people;
            adminpanel.Users = users;

            return View(adminpanel);
        }
    }
}
