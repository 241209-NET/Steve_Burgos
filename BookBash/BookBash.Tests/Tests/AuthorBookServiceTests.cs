using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using BookBash.API.Model;
using BookBash.API.Repository;
using BookBash.API.Service;

namespace BookBash.API.Tests
{
    public class AuthorBookServiceTests
    {
        private readonly Mock<IAuthorBookRepository> _mockRepository;
        private readonly AuthorBookService _service;

        public AuthorBookServiceTests()
        {
            _mockRepository = new Mock<IAuthorBookRepository>();
            _service = new AuthorBookService(_mockRepository.Object);
        }

        [Fact]
        public void CreateNewAuthorBook_ShouldReturnAuthorBook_WhenSuccessfullyCreated()
        {
            var authorBook = new AuthorBook { AuthorID = Guid.NewGuid(), BookISBN = "1234567890" };
            _mockRepository.Setup(r => r.CreateNewAuthorBook(It.IsAny<AuthorBook>())).Returns(authorBook);

            var result = _service.CreateNewAuthorBook(authorBook);

            Assert.NotNull(result);
            Assert.Equal(authorBook.AuthorID, result.AuthorID);
            Assert.Equal(authorBook.BookISBN, result.BookISBN);
        }

        [Fact]
        public void DeleteAuthorBookByID_ShouldReturnAuthorBook_WhenAuthorBookExists()
        {
            var id = Guid.NewGuid();
            var authorBook = new AuthorBook { AuthorID = id, BookISBN = "1234567890" };

            _mockRepository.Setup(r => r.GetAuthorBookByID(id)).Returns(authorBook);
            _mockRepository.Setup(r => r.DeleteAuthorBookByID(id));

            var result = _service.DeleteAuthorBookByID(id);

            Assert.NotNull(result);
            Assert.Equal(id, result?.AuthorID);
            _mockRepository.Verify(r => r.DeleteAuthorBookByID(id), Times.Once);
        }

        [Fact]
        public void DeleteAuthorBookByID_ShouldReturnNull_WhenAuthorBookDoesNotExist()
        {
            var id = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetAuthorBookByID(id)).Returns((AuthorBook?)null);

            var result = _service.DeleteAuthorBookByID(id);

            Assert.Null(result);
            _mockRepository.Verify(r => r.DeleteAuthorBookByID(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void GetAllAuthorBooks_ShouldReturnAllAuthorBooks()
        {
            var authorBooks = new List<AuthorBook>
            {
                new AuthorBook { AuthorID = Guid.NewGuid(), BookISBN = "1234567890" },
                new AuthorBook { AuthorID = Guid.NewGuid(), BookISBN = "0987654321" }
            };

            _mockRepository.Setup(r => r.GetAllAuthorBooks()).Returns(authorBooks);

            var result = _service.GetAllAuthorBooks();

            Assert.NotNull(result);
            Assert.Equal(authorBooks.Count, result.Count());
            Assert.Contains(result, ab => ab.BookISBN == "1234567890");
            Assert.Contains(result, ab => ab.BookISBN == "0987654321");
        }

        [Fact]
        public void GetAuthorBookByID_ShouldReturnAuthorBook_WhenAuthorBookExists()
        {
            var id = Guid.NewGuid();
            var authorBook = new AuthorBook { AuthorID = id, BookISBN = "1234567890" };

            _mockRepository.Setup(r => r.GetAuthorBookByID(id)).Returns(authorBook);

            var result = _service.GetAuthorBookByID(id);

            Assert.NotNull(result);
            Assert.Equal(id, result?.AuthorID);
            Assert.Equal("1234567890", result?.BookISBN);
        }

        [Fact]
        public void GetAuthorBookByID_ShouldReturnNull_WhenAuthorBookDoesNotExist()
        {
            var id = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetAuthorBookByID(id)).Returns((AuthorBook?)null);

            var result = _service.GetAuthorBookByID(id);

            Assert.Null(result);
        }
    }
}
