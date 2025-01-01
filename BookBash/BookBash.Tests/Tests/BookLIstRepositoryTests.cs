using Moq;
using Xunit;
using BookBash.API.Model;
using BookBash.API.Repository;
using BookBash.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookBash.API.Tests.Repository
{
    public class BookListRepositoryTests
    {
        private readonly Mock<BookBashContext> _mockContext;
        private readonly Mock<DbSet<BookList>> _mockBookListDbSet;
        private readonly BookListRepository _repository;

        public BookListRepositoryTests()
        {
            // Initialize the mock context and DbSet
            _mockContext = new Mock<BookBashContext>();
            _mockBookListDbSet = new Mock<DbSet<BookList>>();

            // Mock IQueryable<BookList> as we can't mock LINQ extension methods like ToList directly
            var bookLists = new List<BookList>
            {
                new BookList { ID = Guid.NewGuid(), Name = "List 1" },
                new BookList { ID = Guid.NewGuid(), Name = "List 2" }
            }.AsQueryable();

            _mockBookListDbSet.As<IQueryable<BookList>>().Setup(m => m.Provider).Returns(bookLists.Provider);
            _mockBookListDbSet.As<IQueryable<BookList>>().Setup(m => m.Expression).Returns(bookLists.Expression);
            _mockBookListDbSet.As<IQueryable<BookList>>().Setup(m => m.ElementType).Returns(bookLists.ElementType);
            _mockBookListDbSet.As<IQueryable<BookList>>().Setup(m => m.GetEnumerator()).Returns(bookLists.GetEnumerator());

            // Setup mock context to return the mocked DbSet
            _mockContext.Setup(c => c.BookLists).Returns(_mockBookListDbSet.Object);

            // Initialize the repository with the mock context
            _repository = new BookListRepository(_mockContext.Object);
        }

        // Test: GetAllBookLists returns book lists when BookLists DbSet is not empty
        [Fact]
        public void GetAllBookLists_ShouldReturnBookLists_WhenBookListsExist()
        {
            var result = _repository.GetAllBookLists();

            // Assert that the result is not empty and contains the correct number of book lists
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        // Test: GetAllBookLists throws an exception when BookList DbSet is null
        [Fact]
        public void GetAllBookLists_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.BookLists).Returns((DbSet<BookList>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.GetAllBookLists());
            Assert.Equal("BookList DbSet is null.", exception.Message);  // Adjusted to match the message in code
        }

        // Test: CreateNewBookList returns the book list when successfully created
        [Fact]
        public void CreateNewBookList_ShouldReturnBookList_WhenCreatedSuccessfully()
        {
            var newBookList = new BookList { ID = Guid.NewGuid(), Name = "New List" };

            // Setup to simulate adding the book list
            _mockContext.Setup(c => c.BookLists.Add(It.IsAny<BookList>())).Verifiable();
            _mockContext.Setup(c => c.SaveChanges()).Verifiable();

            var result = _repository.CreateNewBookList(newBookList);

            // Assert that the book list was added and saved
            Assert.NotNull(result);
            Assert.Equal("New List", result.Name);

            _mockContext.Verify(c => c.BookLists.Add(It.IsAny<BookList>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Test: CreateNewBookList throws an exception when BookList DbSet is null
        [Fact]
        public void CreateNewBookList_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.BookLists).Returns((DbSet<BookList>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.CreateNewBookList(new BookList { ID = Guid.NewGuid(), Name = "Test List" }));
            Assert.Equal("BookLists DbSet is null.", exception.Message);  // Adjusted to match the message in code
        }

        // Test: GetBookListByID returns book list when found
        [Fact]
        public void GetBookListByID_ShouldReturnBookList_WhenBookListExists()
        {
            var ID = Guid.NewGuid();
            var bookList = new BookList { ID = ID, Name = "List 1" };

            _mockContext.Setup(c => c.BookLists.Find(ID)).Returns(bookList);

            var result = _repository.GetBookListByID(ID);

            Assert.NotNull(result);
            Assert.Equal("List 1", result?.Name);
        }

        // Test: GetBookListByID returns null when book list does not exist
        [Fact]
        public void GetBookListByID_ShouldThrowException_WhenBookListDoesNotExist()
        {
            var ID = Guid.NewGuid();

            _mockContext.Setup(c => c.BookLists.Find(ID)).Returns((BookList)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.GetBookListByID(ID));
            Assert.Equal("BookList not found.", exception.Message);  // Adjusted to match the message in code
        }

        // Test: DeleteBookListByID deletes book list and returns the book list when successful
        [Fact]
        public void DeleteBookListByID_ShouldReturnBookList_WhenBookListDeleted()
        {
            var ID = Guid.NewGuid();
            var bookList = new BookList { ID = ID, Name = "List 1" };

            // Setup to return a book list when Find is called
            _mockContext.Setup(c => c.BookLists.Find(ID)).Returns(bookList);
            _mockContext.Setup(c => c.BookLists.Remove(bookList)).Verifiable();
            _mockContext.Setup(c => c.SaveChanges()).Verifiable();

            var result = _repository.DeleteBookListByID(ID);

            // Assert that the book list was deleted and saved
            Assert.NotNull(result);
            Assert.Equal("List 1", result.Name);

            _mockContext.Verify(c => c.BookLists.Remove(bookList), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Test: DeleteBookListByID throws an exception when the book list is not found
        [Fact]
        public void DeleteBookListByID_ShouldThrowException_WhenBookListNotFound()
        {
            var ID = Guid.NewGuid();

            _mockContext.Setup(c => c.BookLists.Find(ID)).Returns((BookList)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.DeleteBookListByID(ID));
            Assert.Equal("BookList not found.", exception.Message);  // Adjusted to match the message in code
        }

        // Test: DeleteBookListByID throws an exception when BookList DbSet is null
        [Fact]
        public void DeleteBookListByID_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.BookLists).Returns((DbSet<BookList>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.DeleteBookListByID(Guid.NewGuid()));
            Assert.Equal("BookList DbSet is null.", exception.Message);  // Adjusted to match the message in code
        }
    }
}
