using System;
using Xunit;
using NSubstitute;
using Bookshop.Repositories;
using Bookshop.Controllers;
using Bookshop.Utils;
using Bookshop.Models;
using System.Web.Mvc;


namespace BookshopTests.Controllers
{
    public class AuthorControllerTests
    {
        [Fact]
        public void Index_WhenCalledWithEmptyString_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            // Act
            ViewResult viewResult = authorController.Index(String.Empty) as ViewResult;

            // Assert
            Assert.NotNull(viewResult);
        }

        [Fact]
        public void Create_WhenCalled_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            // Act
            ViewResult result = authorController.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_WhenCalledWithAuthor_ShouldRedirectToIndex()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            Author testAuthor = new Author();
            testAuthor.Name = "Adam";
            testAuthor.Surname = "Kowalski";


            // Act
            ActionResult result = authorController.Create(testAuthor);

            // Assert
            Assert.Equal("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [Fact]
        public void Create_WhenCalledWithNullAuthor_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            Author testAuthor = null;

            // Act
            ViewResult result = authorController.Create(testAuthor) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();

            authorRepository.GetAuthorById(7).Returns(new Author());

            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            // Act
            ViewResult result = authorController.Edit(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_WhenCalledWithAuthor_ShouldRedirectToIndex()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            Author testAuthor = new Author();
            testAuthor.Name = "Adam";
            testAuthor.Surname = "Kowalski";


            // Act
            ActionResult result = authorController.Edit(testAuthor);

            // Assert
            Assert.Equal("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [Fact]
        public void Edit_WhenCalledWithNullAuthor_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            Author testAuthor = null;

            // Act
            ViewResult result = authorController.Edit(testAuthor) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();

            authorRepository.GetAuthorById(7).Returns(new Author());

            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            // Act
            ViewResult result = authorController.Delete(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_WhenCalledWithAuthor_ShouldRedirectToIndex()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            Author testAuthor = new Author();
            testAuthor.AuthorId = 1;
            testAuthor.Name = "Adam";
            testAuthor.Surname = "Kowalski";


            // Act
            ActionResult result = authorController.Delete(testAuthor);

            // Assert
            Assert.Equal("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [Fact]
        public void Details_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();

            authorRepository.GetAuthorById(7).Returns(new Author());

            AuthorController authorController = new AuthorController(authorRepository, searchUtility);

            // Act
            ViewResult result = authorController.Details(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

    }
}
