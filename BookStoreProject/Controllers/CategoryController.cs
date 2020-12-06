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
    public class CategoryController : Controller
    {

        private readonly BookContext _bookcontext;
        public CategoryController(BookContext bookContext)
        {
            _bookcontext = bookContext;
        }
        public IActionResult Index()
        {

            List<CategoryVM> categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).Select(q => new CategoryVM()
            {
                CategoryID = q.ID,
                CategoryName = q.CategoryName,
                AddDate = q.AddDate,
                BookCategories = q.BookCategories
            }).ToList();

            return View(categories);
        }

        [HttpPost]
        public IActionResult Add(CategoryVM model)
        {
            Category category = new Category();

            category.CategoryName = model.CategoryName;
            category.BookCategories = model.BookCategories;

            _bookcontext.Categories.Add(category);
            _bookcontext.SaveChanges();


            return Json()
            return RedirectToAction("Add", "Person");
        }
    }
}
