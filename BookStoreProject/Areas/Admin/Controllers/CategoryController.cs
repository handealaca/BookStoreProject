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
    public class CategoryController : BaseController
    {

        private readonly BookContext _bookcontext;
        public CategoryController(BookContext bookcontext, IMemoryCache memoryCache) : base(bookcontext, memoryCache)
        {
            _bookcontext = bookcontext;
        }


        [RoleControl(EnumRole.CategoryList)]
        public IActionResult Index()
        {

            List<CategoryVM> categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).Select(q => new CategoryVM()
            {
                CategoryID = q.ID,
                CategoryName = q.CategoryName,
                TopCategory = q.TopCategory,


            }).ToList();

            return View(categories);
        }


        [RoleControl(EnumRole.CategoryAdd)]
        public IActionResult Add()
        {
            CategoryVM model = new CategoryVM();

            model.topcategories = _bookcontext.Categories.Where(q => q.IsDeleted == false & q.TopCategory == 0).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(CategoryVM model, int? topcategories)
        {

            if (topcategories == null)
            {
                Category category = new Category();

                category.CategoryName = model.CategoryName;
                category.TopCategory = 0;

                _bookcontext.Categories.Add(category);
                _bookcontext.SaveChanges();

                int CategoryID = category.ID;


                Category category1 = new Category();
                category1.TopCategory = CategoryID;
                category1.CategoryName = model.subcategoryname;

                _bookcontext.Categories.Add(category1);
                _bookcontext.SaveChanges();
            }

            else
            {
                Category category2 = new Category();
                category2.CategoryName = model.subcategoryname;
                category2.TopCategory = topcategories;

                _bookcontext.Categories.Add(category2);
                _bookcontext.SaveChanges();
            }

            return RedirectToAction("Index", "Category");



        }

        [RoleControl(EnumRole.CategoryEdit)]
        public IActionResult Edit(int id)
        {
            Category category = _bookcontext.Categories.FirstOrDefault(q => q.ID == id);


            CategoryVM model = new CategoryVM();
            model.CategoryID = category.ID;
            model.CategoryName = category.CategoryName;
            model.TopCategory = category.TopCategory;

            model.topcategories = _bookcontext.Categories.Where(q => q.IsDeleted == false & q.TopCategory == 0).ToList();
            return View(model);

        }

        [HttpPost]
        public IActionResult Edit(CategoryVM model, int? topcategories)
        {
            Category category = _bookcontext.Categories.FirstOrDefault(q => q.ID == model.CategoryID);

            category.CategoryName = model.CategoryName;

            if (topcategories == null)
            {

                category.TopCategory = 0;
            }
            else
            {
                category.TopCategory = topcategories;
            }

            _bookcontext.SaveChanges();

            return RedirectToAction("Index", "Category");
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category = _bookcontext.Categories.FirstOrDefault(q => q.ID == id);

            category.IsDeleted = true;
            _bookcontext.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult CategoryDetail(int id)
        {
            List<BookVM> books = _bookcontext.BookCategories.Include(q => q.Book).ThenInclude(Book => Book.BookPersons).ThenInclude(BookPersons => BookPersons.Person).Where(q => q.CategoryID == id).Select(q => new BookVM()
            {
                BookID = q.BookID,
                Name = q.Book.Name,
                Publisher = q.Book.Publisher,
                PublishDate = q.Book.PublishDate,
                Edition = q.Book.Edition,
                BookPersons = q.Book.BookPersons

            }).ToList();

            return View(books);
        }

    }
}
