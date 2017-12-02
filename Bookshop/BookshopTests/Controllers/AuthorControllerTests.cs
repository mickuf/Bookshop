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
    public class AuthorControllerTests
    {
        [Fact]
        public void Index_WhenCalledWithEmptyString_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility, imageFileUtility);

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
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility, imageFileUtility);

            // Act
            ViewResult result = authorController.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_WhenCalledWithNullAuthor_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility, imageFileUtility);

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
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();

            authorRepository.GetAuthorById(7).Returns(new Author());

            AuthorController authorController = new AuthorController(authorRepository, searchUtility, imageFileUtility);

            // Act
            ViewResult result = authorController.Edit(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_WhenCalledWithNullAuthor_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility, imageFileUtility);

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
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();

            authorRepository.GetAuthorById(7).Returns(new Author());

            AuthorController authorController = new AuthorController(authorRepository, searchUtility, imageFileUtility);

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
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();
            AuthorController authorController = new AuthorController(authorRepository, searchUtility, imageFileUtility);

            Author testAuthor = new Author
            {
                AuthorId = 1,
                Name = "Adam",
                Surname = "Kowalski"
            };


            // Act
            ActionResult result = authorController.Delete(testAuthor);

            // Assert
            Assert.Equal("Index", ((RedirectToRouteResult) result).RouteValues["action"]);
        }

        [Fact]
        public void Details_WhenCalledWithIntParameter_ShouldNotReturnNull()
        {
            // Arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
            ISearchUtility searchUtility = Substitute.For<ISearchUtility>();
            IImageFileUtility imageFileUtility = Substitute.For<IImageFileUtility>();

            authorRepository.GetAuthorById(7).Returns(new Author());

            AuthorController authorController = new AuthorController(authorRepository, searchUtility, imageFileUtility);

            // Act
            ViewResult result = authorController.Details(7) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

    }
}
