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



        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category();

                category.CategoryName = model.CategoryName;

                _bookcontext.Categories.Add(category);
                _bookcontext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();

        }


        public IActionResult Edit(int id)
        {
            Category category = _bookcontext.Categories.FirstOrDefault(q => q.ID == id);


            CategoryVM model = new CategoryVM();
            model.CategoryID = category.ID;
            model.CategoryName = category.CategoryName;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CategoryVM model)
        {
            Category category = _bookcontext.Categories.FirstOrDefault(q => q.ID == model.CategoryID);

            if (ModelState.IsValid)
            {
                category.CategoryName = model.CategoryName;
                _bookcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category = _bookcontext.Categories.FirstOrDefault(q => q.ID == id);

            category.IsDeleted = true;
            _bookcontext.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
