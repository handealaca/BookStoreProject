﻿using BookStoreProject.Areas.Admin.Controllers;
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

namespace BookStoreProject.Controllers
{
    public class AdminHomeController : BaseController
    {
        private readonly BookContext _bookcontext;
        public AdminHomeController(BookContext bookcontext, IMemoryCache memoryCache): base(bookcontext,memoryCache )
        {
            _bookcontext = bookcontext;
        }

        public IActionResult Index()
        {
            List<BookVM> books = _bookcontext.Books.Where(q => q.IsDeleted == false).Include(q=>q.BookCategories).ThenInclude(BookCategories => BookCategories.Category).Include(q => q.BookPersons).ThenInclude(BookPerson => BookPerson.Person).Select(q => new BookVM()
            {
                BookID = q.ID,
                Name = q.Name,
                BookPersons = q.BookPersons.Where(q => q.IsDeleted == false).ToList(),
                Publisher = q.Publisher,
                PublishDate = q.PublishDate,
                BookCategories = q.BookCategories.Where(q => q.IsDeleted == false).ToList(),
            }).ToList();

            List<UserVM> users = _bookcontext.Users.Where(q => q.IsDeleted == false).Select(q => new UserVM()
            {
                UserID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                UserEmail = q.Email,
                BirthDate = q.BirthDate
            }).ToList();

            List<PersonVM> people = _bookcontext.People.Where(q => q.IsDeleted == false).Include(q => q.BookPeople).ThenInclude(BookPeople => BookPeople.Book).Include(q=>q.PersonDuties).Select(q => new PersonVM()
            {
                PersonID = q.ID,
                Name = q.Name,
                SurName = q.SurName,
                BirthDate = q.BirthDate,
                Duties = q.PersonDuties.Where(q=>q.IsDeleted==false).Select(q=>q.DutyID == Convert.ToInt32(EnumDuty.Writer) ? EnumDuty.Writer.ToString() : EnumDuty.Interpreter.ToString()).ToList(),
                Books = q.BookPeople.Where(q=>q.IsDeleted== false).ToList()
            }).ToList();

            List<CategoryVM> categories = _bookcontext.Categories.Where(q => q.IsDeleted == false).Include(q => q.BookCategories).ThenInclude(BookCategories => BookCategories.Book).Select(q => new CategoryVM()
            {
                CategoryID = q.ID,
                CategoryName = q.CategoryName,
                Books=q.BookCategories.Where(q => q.IsDeleted == false).ToList()
            }).ToList();

            AdminPanelVM adminpanel = new AdminPanelVM();
            adminpanel.Books = books;
            adminpanel.Categories = categories;
            adminpanel.People = people;
            adminpanel.Users = users;

            return View(adminpanel);
        }
    }
}
