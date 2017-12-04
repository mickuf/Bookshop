using Bookshop.Controllers;
using Bookshop.Models;
using Bookshop.Repositories;
using Bookshop.Utils;
using NSubstitute;
using System;
using System.Web.Mvc;
using Xunit;

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
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();
            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility, imageFileUtility);

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
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();
            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility, imageFileUtility);

            // Act
            ViewResult result = bookController.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }



        [Fact]
        public void Edit_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();

            bookRepository.GetBookById(7).Returns(new Book());

            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility, imageFileUtility);

            // Act
            ViewResult result = bookController.Edit(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();

            bookRepository.GetBookById(7).Returns(new Book());

            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility, imageFileUtility);

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
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();
            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility, imageFileUtility);

            Book testBook = new Book
            {
                Id = 7,
                Title = "Gra o Tron"
            };


            // Act
            ActionResult result = bookController.Delete(testBook);

            // Assert
            Assert.Equal("Index", ((RedirectToRouteResult) result).RouteValues["action"]);
        }

        [Fact]
        public void Details_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IBookRepository bookRepository = Substitute.For<IBookRepository>();
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();

            bookRepository.GetBookById(7).Returns(new Book());

            BookController bookController = new BookController(bookRepository, authorRepository, searchUtility, imageFileUtility);

            // Act
            ViewResult result = bookController.Details(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

    }
}

