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
    public class BookListServiceTests
    {
        private readonly Mock<IBookListRepository> _mockRepository;
        private readonly BookListService _service;

        public BookListServiceTests()
        {
            _mockRepository = new Mock<IBookListRepository>();
            _service = new BookListService(_mockRepository.Object);
        }

        [Fact]
        public void CreateNewBookList_ShouldReturnBookList_WhenSuccessfullyCreated()
        {
            var bookList = new BookList { ID = Guid.NewGuid(), Name = "Test List" };
            _mockRepository.Setup(r => r.CreateNewBookList(It.IsAny<BookList>())).Returns(bookList);

            var result = _service.CreateNewBookList(bookList);

            Assert.NotNull(result);
            Assert.Equal(bookList.ID, result.ID);
            Assert.Equal(bookList.Name, result.Name);
        }

        [Fact]
        public void DeleteBookListByID_ShouldReturnBookList_WhenBookListExists()
        {
            var id = Guid.NewGuid();
            var bookList = new BookList { ID = id, Name = "Test List" };

            _mockRepository.Setup(r => r.GetBookListByID(id)).Returns(bookList);
            _mockRepository.Setup(r => r.DeleteBookListByID(id));

            var result = _service.DeleteBookListByID(id);

            Assert.NotNull(result);
            Assert.Equal(id, result?.ID);
            Assert.Equal("Test List", result?.Name);
            _mockRepository.Verify(r => r.DeleteBookListByID(id), Times.Once);
        }

        [Fact]
        public void DeleteBookListByID_ShouldReturnNull_WhenBookListDoesNotExist()
        {
            var id = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetBookListByID(id)).Returns((BookList?)null);

            var result = _service.DeleteBookListByID(id);

            Assert.Null(result);
            _mockRepository.Verify(r => r.DeleteBookListByID(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public void GetAllBookLists_ShouldReturnAllBookLists()
        {
            var bookLists = new List<BookList>
            {
                new BookList { ID = Guid.NewGuid(), Name = "Test List 1" },
                new BookList { ID = Guid.NewGuid(), Name = "Test List 2" }
            };

            _mockRepository.Setup(r => r.GetAllBookLists()).Returns(bookLists);

            var result = _service.GetAllBookLists();

            Assert.NotNull(result);
            Assert.Equal(bookLists.Count, result.Count());
            Assert.Contains(result, bl => bl.Name == "Test List 1");
            Assert.Contains(result, bl => bl.Name == "Test List 2");
        }

        [Fact]
        public void GetBookListByID_ShouldReturnBookList_WhenBookListExists()
        {
            var id = Guid.NewGuid();
            var bookList = new BookList { ID = id, Name = "Test List" };

            _mockRepository.Setup(r => r.GetBookListByID(id)).Returns(bookList);

            var result = _service.GetBookListByID(id);

            Assert.NotNull(result);
            Assert.Equal(id, result?.ID);
            Assert.Equal("Test List", result?.Name);
        }

        [Fact]
        public void GetBookListByID_ShouldReturnNull_WhenBookListDoesNotExist()
        {
            var id = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetBookListByID(id)).Returns((BookList?)null);

            var result = _service.GetBookListByID(id);

            Assert.Null(result);
        }

        [Fact]
        public void GetBookListByName_ShouldThrowNotImplementedException()
        {
            var exception = Assert.Throws<NotImplementedException>(() => _service.GetBookListByName("Test List"));
            Assert.Equal("The method or operation is not implemented.", exception.Message);
        }
    }
}
