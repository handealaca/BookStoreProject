using BookStoreProject.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BookStoreProject.Models.VM
{
    public class SiteBookVM
    {
      
        public List<BookCategory> bookcategories { get; set; }
        //public List<Book> books { get; set; }
        public IPagedList<Book> books { get; set; }

        public List<Person> people { get; set; }
        public List<Category> Categories { get; set; }

        public List<Category> subcategories { get; set; }
        public Comment Comment { get; set; }
        public Book Bookdetail { get; set; }
        public PersonVM PersonDetail { get; set; }
        public IPagedList<PersonVM> PeopleVM { get; set; }

    }
}
