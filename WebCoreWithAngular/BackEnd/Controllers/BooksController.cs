using System.Collections.Generic;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        [HttpGet, Authorize]
        public IEnumerable<Book> Get()
        {
            var currentUser = HttpContext.User;
            var resultBookList = new Book[]
            {
                new Book {Author = "Ray Bradbury", Title = "Fahrenheit 451"},
                new Book {Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude"},
                new Book {Author = "George Orwell", Title = "1984"},
                new Book {Author = "Anais Nin", Title = "Delta of Venus"}
            };

            return resultBookList;
        }

        [Route("b2")]
        [HttpGet, Authorize(Roles = "Admin")]
        public IEnumerable<Book> Get2()
        {
            var currentUser = HttpContext.User;
            var resultBookList = new Book[]
            {
                new Book {Author = "Ray Bradbury", Title = "Fahrenheit 451"},
                new Book {Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude"},
                new Book {Author = "George Orwell", Title = "1984"},
                new Book {Author = "Anais Nin", Title = "Delta of Venus"}
            };

            return resultBookList;
        }
    }
}