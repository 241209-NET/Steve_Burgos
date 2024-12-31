using Moq;
using Xunit;
using BookBash.API.Model;
using BookBash.API.Repository;
using BookBash.API.Service;
using System;
using System.Collections.Generic;

namespace BookBash.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly Mock<IBookListRepository> _mockBookListRepository;
        private readonly Mock<IBookBookListRepository> _mockBookBookListRepository;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _mockBookListRepository = new Mock<IBookListRepository>();
            _mockBookBookListRepository = new Mock<IBookBookListRepository>();

            _bookService = new BookService(_mockBookRepository.Object, _mockBookListRepository.Object, _mockBookBookListRepository.Object);
        }

        #region GetAllBooks

        [Fact]
        public void GetAllBooks_ShouldReturnListOfBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { ISBN = "123456789", Title = "Book 1" },
                new Book { ISBN = "987654321", Title = "Book 2" }
            };

            _mockBookRepository.Setup(repo => repo.GetAllBooks()).Returns(books);

            // Act
            var result = _bookService.GetAllBooks();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, b => b.ISBN == "123456789");
            Assert.Contains(result, b => b.ISBN == "987654321");
        }

        #endregion

        #region GetBookByISBN

        [Fact]
        public void GetBookByISBN_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { ISBN = "123456789", Title = "Book 1" };
            _mockBookRepository.Setup(repo => repo.GetBookByISBN("123456789")).Returns(book);

            // Act
            var result = _bookService.GetBookByISBN("123456789");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("123456789", result.ISBN);
            Assert.Equal("Book 1", result.Title);
        }

        [Fact]
        public void GetBookByISBN_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            _mockBookRepository.Setup(repo => repo.GetBookByISBN("999999999")).Returns((Book)null);

            // Act
            var result = _bookService.GetBookByISBN("999999999");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region CreateNewBook

        [Fact]
        public void CreateNewBook_ShouldCallCreateMethodOnce()
        {
            // Arrange
            var newBook = new Book { ISBN = "123456789", Title = "New Book" };

            _mockBookRepository.Setup(repo => repo.CreateNewBook(It.IsAny<Book>())).Returns(newBook);

            // Act
            var result = _bookService.CreateNewBook(newBook);

            // Assert
            _mockBookRepository.Verify(repo => repo.CreateNewBook(It.IsAny<Book>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("123456789", result.ISBN);
        }

        #endregion

        #region DeleteBookByISBN

        [Fact]
        public void DeleteBookByISBN_ShouldDeleteBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { ISBN = "123456789", Title = "Book to Delete" };

            _mockBookRepository.Setup(repo => repo.GetBookByISBN("123456789")).Returns(book);
            _mockBookRepository.Setup(repo => repo.DeleteBookByISBN("123456789")).Verifiable();

            // Act
            var result = _bookService.DeleteBookByISBN("123456789");

            // Assert
            _mockBookRepository.Verify(repo => repo.DeleteBookByISBN("123456789"), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("123456789", result.ISBN);
        }

        [Fact]
        public void DeleteBookByISBN_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            _mockBookRepository.Setup(repo => repo.GetBookByISBN("999999999")).Returns((Book)null);

            // Act
            var result = _bookService.DeleteBookByISBN("999999999");

            // Assert
            _mockBookRepository.Verify(repo => repo.DeleteBookByISBN(It.IsAny<string>()), Times.Never);
            Assert.Null(result);
        }

        #endregion
    }
}
