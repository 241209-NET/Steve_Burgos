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
    public class BookBookListServiceTests
    {
        private readonly Mock<IBookBookListRepository> _mockRepository;
        private readonly BookBookListService _service;

        public BookBookListServiceTests()
        {
            _mockRepository = new Mock<IBookBookListRepository>();
            _service = new BookBookListService(_mockRepository.Object);
        }

        [Fact]
        public void CreateNewBookBookList_ShouldReturnBookBookList_WhenSuccessfullyCreated()
        {
            var bookBookList = new BookBookList { BookISBN = "1234567890", BookListID = Guid.NewGuid() };
            _mockRepository.Setup(r => r.CreateNewBookBookList(It.IsAny<BookBookList>())).Returns(bookBookList);

            var result = _service.CreateNewBookBookList(bookBookList);

            Assert.NotNull(result);
            Assert.Equal(bookBookList.BookISBN, result.BookISBN);
            Assert.Equal(bookBookList.BookListID, result.BookListID);
        }

        [Fact]
        public void DeleteBookBookListByID_ShouldReturnBookBookList_WhenBookBookListExists()
        {
            var id = Guid.NewGuid();
            var isbn = "1234567890";
            var bookBookList = new BookBookList { BookISBN = isbn, BookListID = id };

            _mockRepository.Setup(r => r.GetBookBookListByID(id, isbn)).Returns(bookBookList);
            _mockRepository.Setup(r => r.DeleteBookBookListByID(id, isbn));

            var result = _service.DeleteBookBookListByID(id, isbn);

            Assert.NotNull(result);
            Assert.Equal(isbn, result?.BookISBN);
            Assert.Equal(id, result?.BookListID);
            _mockRepository.Verify(r => r.DeleteBookBookListByID(id, isbn), Times.Once);
        }

        [Fact]
        public void DeleteBookBookListByID_ShouldReturnNull_WhenBookBookListDoesNotExist()
        {
            var id = Guid.NewGuid();
            var isbn = "1234567890";
            _mockRepository.Setup(r => r.GetBookBookListByID(id, isbn)).Returns((BookBookList?)null);

            var result = _service.DeleteBookBookListByID(id, isbn);

            Assert.Null(result);
            _mockRepository.Verify(r => r.DeleteBookBookListByID(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GetAllBookBookLists_ShouldReturnAllBookBookLists()
        {
            var bookBookLists = new List<BookBookList>
            {
                new BookBookList { BookISBN = "1234567890", BookListID = Guid.NewGuid() },
                new BookBookList { BookISBN = "0987654321", BookListID = Guid.NewGuid() }
            };

            _mockRepository.Setup(r => r.GetAllBookBookLists()).Returns(bookBookLists);

            var result = _service.GetAllBookBookLists();

            Assert.NotNull(result);
            Assert.Equal(bookBookLists.Count, result.Count());
            Assert.Contains(result, bbl => bbl.BookISBN == "1234567890");
            Assert.Contains(result, bbl => bbl.BookISBN == "0987654321");
        }

        [Fact]
        public void GetBookBookListByID_ShouldReturnBookBookList_WhenBookBookListExists()
        {
            var id = Guid.NewGuid();
            var isbn = "1234567890";
            var bookBookList = new BookBookList { BookISBN = isbn, BookListID = id };

            _mockRepository.Setup(r => r.GetBookBookListByID(id, isbn)).Returns(bookBookList);

            var result = _service.GetBookBookListByID(id, isbn);

            Assert.NotNull(result);
            Assert.Equal(isbn, result?.BookISBN);
            Assert.Equal(id, result?.BookListID);
        }

        [Fact]
        public void GetBookBookListByID_ShouldReturnNull_WhenBookBookListDoesNotExist()
        {
            var id = Guid.NewGuid();
            var isbn = "1234567890";
            _mockRepository.Setup(r => r.GetBookBookListByID(id, isbn)).Returns((BookBookList?)null);

            var result = _service.GetBookBookListByID(id, isbn);

            Assert.Null(result);
        }
    }
}
