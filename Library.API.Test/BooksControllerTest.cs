using Library.API.Controllers;
using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.API.Test
{
    public class BooksControllerTest
    {
        BooksController _controller;
        IBookService _service;

        public BooksControllerTest()
        {
            _service = new BookService();
            _controller = new BooksController(_service);
        }

        [Fact]
        public void GetAllTest() {
            //arrange
            //act
            var result = _controller.Get();
            //assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as ObjectResult;
            Assert.IsType<List<Book>>(list.Value);

            var listBooks = list.Value as List<Book>;
            Assert.Equal(5, listBooks.Count);
        }

        [Theory]
        [InlineData("0f8fad5b-d9cb-469f-a165-70867728950e", "0f8fad5b-d9cb-469f-a165-70867728950f")]
        public void GetBookByIdTest(string guid1, string guid2)
        {
            //arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            //act
            var notFoundResult = _controller.Get(invalidGuid);
            var okResult = _controller.Get(validGuid);

            //assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);

            Assert.IsType<OkObjectResult>(okResult.Result);

            var item = okResult.Result as OkObjectResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(validGuid, bookItem.Id);
            Assert.Equal("Managing Oneself", bookItem.Title);
        }

        [Fact]
        public void AddBookTest() {
            //arrange
            var completeBook = new Book()
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"
            };
            //act
            var createdResponse = _controller.Post(completeBook);
            //assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

            var item = createdResponse as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(completeBook.Author, bookItem.Author);
            Assert.Equal(completeBook.Title, bookItem.Title);
            Assert.Equal(completeBook.Description, bookItem.Description);

            //arrange
            var incompleteBook = new Book()
            {
                Author = "Author",
                Description = "Description"
            };
            //act
            _controller.ModelState.AddModelError("Title", "Title is a required field");
            var badResponse = _controller.Post(incompleteBook);
            //assert
            Assert.IsType<BadRequestObjectResult>(badResponse);

        }

        [Theory]
        [InlineData("0f8fad5b-d9cb-469f-a165-70867728950e", "0f8fad5b-d9cb-469f-a165-70867728950f")]
        public void RemoveBookByIdTest(string guid1, string guid2)
        {
            //arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            //act
            var notFoundResult = _controller.Remove(invalidGuid);
            //assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(5, _service.GetAll().Count());

            //act
            var okResult = _controller.Remove(validGuid);
            //assert
            Assert.IsType<OkResult>(okResult);
            Assert.Equal(4, _service.GetAll().Count());
        }

    }
}
