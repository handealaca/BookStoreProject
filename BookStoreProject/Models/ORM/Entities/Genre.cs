using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.ORM.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public int TopGenreID { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}
