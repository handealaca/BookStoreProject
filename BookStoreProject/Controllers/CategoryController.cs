﻿using BookStoreProject.Models.ORM.Context;
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
               
               
            }).ToList();

            return View(categories);
        }



        public IActionResult Add()
        {
            CategoryVM model = new CategoryVM();
            model.subcategories = _bookcontext.Categories.Where(q => q.IsDeleted == false & q.TopCategory != 0).ToList();
            model.topcategories = _bookcontext.Categories.Where(q => q.IsDeleted == false & q.TopCategory == 0).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(CategoryVM model, int[] categories)
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
