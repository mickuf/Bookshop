using System;
using Xunit;
using NSubstitute;
using Bookshop.Repositories;
using Bookshop.Controllers;
using Bookshop.Utils;
using Bookshop.Models;
using Bookshop.ViewModels;
using System.Web.Mvc;
using System.Collections.Generic;

namespace BookshopTests.Controllers
{
    public class BookControllerTests
    {
        [Fact]
        public void Index_WhenCalledWithEmptyString_ShouldNotReturnNull()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility);

            // Act
            ViewResult viewResult = bookController.Index(String.Empty) as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }

        [Fact]
        public void Create_WhenCalled_ShouldNotReturnNull()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility);

            // Act
            ViewResult result = bookController.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_WhenCalledWithBookModifyViewModel_ShouldRedirectToIndex()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility);

            BookModifyViewModel testBook = new BookModifyViewModel();
            testBook.Id = 7;
            testBook.Title = "Gra o Tron";


            // Act
            ActionResult result = bookController.Create(testBook);

            // Assert
            Assert.Equal("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [Fact]
        public void Edit_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();

            bookRepository.GetBookById(7).Returns(new Book());

            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility);

            // Act
            ViewResult result = bookController.Edit(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_WhenCalledWithBookModifyViewModel_ShouldRedirectToIndex()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility);

            BookModifyViewModel testBook = new BookModifyViewModel();
            testBook.Id = 7;
            testBook.Title = "Gra o Tron";


            // Act
            ActionResult result = bookController.Edit(testBook);

            // Assert
            Assert.Equal("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }


        [Fact]
        public void Delete_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();

            bookRepository.GetBookById(7).Returns(new Book());

            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility);

            // Act
            ViewResult result = bookController.Delete(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_WhenCalledWithAuthor_ShouldRedirectToIndex()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility);

            Book testBook = new Book();
            testBook.Id = 7;
            testBook.Title = "Gra o Tron";


            // Act
            ActionResult result = bookController.Delete(testBook);

            // Assert
            Assert.Equal("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [Fact]
        public void Details_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();

            bookRepository.GetBookById(7).Returns(new Book());

            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility);

            // Act
            ViewResult result = bookController.Details(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

    }
}

