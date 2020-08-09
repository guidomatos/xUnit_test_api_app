using Library.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Data.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = new List<Book>()
            {
                new Book()
                {
                    Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    Title = "Managing Oneself",
                    Description ="We live in an age of unprecedented opportunity",
                    Author= "Peter Ducker"
                },
                new Book()
                {
                    Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                    Title = "Evolutionary Psychology",
                    Description ="Evolutionary Psychology: The New Science of the Mind",
                    Author= "David Buss"
                },
                new Book()
                {
                    Id = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
                    Title = "How to Win Friends & Influence People",
                    Description ="Millions of people around the world have improved",
                    Author= "Dale Carnegie"
                },
                new Book()
                {
                    Id = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247483"),
                    Title = "The Selfish Gene",
                    Description ="Professor Dawkins articulates a gene's eye view",
                    Author= "Richard Dawkins"
                },
                new Book()
                {
                    Id = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247484"),
                    Title = "The Lessons of History",
                    Description ="Will and Ariel Durant have succeeded in distilling",
                    Author= "Will & Ariel Durant"
                }
            };
        }

        public IEnumerable<Book> GetAll() => _books.ToList();

        public Book Add(Book newBook)
        {
            _books.Add(newBook);
            return newBook;
        }

        public Book GetById(Guid id)
        {
            return _books.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {
            Book book = _books.Where(x => x.Id == id).FirstOrDefault();
            _books.Remove(book);
        }
    }
}
