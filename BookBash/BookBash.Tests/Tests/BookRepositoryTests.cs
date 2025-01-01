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
    public class BookRepositoryTests
    {
        private readonly Mock<BookBashContext> _mockContext;
        private readonly Mock<DbSet<Book>> _mockBookDbSet;
        private readonly BookRepository _repository;

        public BookRepositoryTests()
        {
            // Initialize the mock context and DbSet
            _mockContext = new Mock<BookBashContext>();
            _mockBookDbSet = new Mock<DbSet<Book>>();

            // Mock IQueryable<Book> as we can't mock LINQ extension methods like ToList directly
            var books = new List<Book>
            {
                new Book { ISBN = "12345", Title = "Book 1" },
                new Book { ISBN = "67890", Title = "Book 2" }
            }.AsQueryable();

            _mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(books.Provider);
            _mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            _mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            _mockBookDbSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            // Setup mock context to return the mocked DbSet
            _mockContext.Setup(c => c.Books).Returns(_mockBookDbSet.Object);

            // Initialize the repository with the mock context
            _repository = new BookRepository(_mockContext.Object);
        }

        // Test: GetAllBooks returns books when Books DbSet is not empty
        [Fact]
        public void GetAllBooks_ShouldReturnBooks_WhenBooksExist()
        {
            var result = _repository.GetAllBooks();

            // Assert that the result is not empty and contains the correct number of books
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

         // Test: GetAllBooks throws an exception when Books DbSet is null
        [Fact]
        public void GetAllBooks_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.Books).Returns((DbSet<Book>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.GetAllBooks());
            Assert.Equal("Book DbSet is null.", exception.Message);  // Corrected message
        }

        // Test: DeleteBookByISBN throws an exception when Books DbSet is null
        [Fact]
        public void DeleteBookByISBN_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.Books).Returns((DbSet<Book>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.DeleteBookByISBN("12345"));
            Assert.Equal("Book DbSet is null.", exception.Message);  // Corrected message
        }

        // Test: CreateNewBook returns the book when successfully created
        [Fact]
        public void CreateNewBook_ShouldReturnBook_WhenCreatedSuccessfully()
        {
            var newBook = new Book { ISBN = "112233", Title = "New Book" };

            // Setup to simulate adding the book
            _mockContext.Setup(c => c.Books.Add(It.IsAny<Book>())).Verifiable();
            _mockContext.Setup(c => c.SaveChanges()).Verifiable();

            var result = _repository.CreateNewBook(newBook);

            // Assert that the book was added and saved
            Assert.NotNull(result);
            Assert.Equal("New Book", result.Title);

            _mockContext.Verify(c => c.Books.Add(It.IsAny<Book>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Test: CreateNewBook throws an exception when Books DbSet is null
        [Fact]
        public void CreateNewBook_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.Books).Returns((DbSet<Book>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.CreateNewBook(new Book { ISBN = "99999", Title = "Test Book" }));
            Assert.Equal("Books DbSet is null.", exception.Message);  // Corrected message
        }

        // Test: GetBookByISBN returns book when found
        [Fact]
        public void GetBookByISBN_ShouldReturnBook_WhenBookExists()
        {
            var isbn = "12345";
            var book = new Book { ISBN = isbn, Title = "Book 1" };

            _mockContext.Setup(c => c.Books.Find(isbn)).Returns(book);

            var result = _repository.GetBookByISBN(isbn);

            Assert.NotNull(result);
            Assert.Equal("Book 1", result.Title);
        }

        // Test: GetBookByISBN throws an exception when book does not exist
        [Fact]
        public void GetBookByISBN_ShouldThrowException_WhenBookDoesNotExist()
        {
            var isbn = "00000";

            _mockContext.Setup(c => c.Books.Find(isbn)).Returns((Book)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.GetBookByISBN(isbn));
            Assert.Equal("Book not found.", exception.Message);  // Corrected message
        }

        // Test: DeleteBookByISBN deletes book and returns the book when successful
        [Fact]
        public void DeleteBookByISBN_ShouldReturnBook_WhenBookDeleted()
        {
            var isbn = "12345";
            var book = new Book { ISBN = isbn, Title = "Book 1" };

            // Setup to return the book when Find is called
            _mockContext.Setup(c => c.Books.Find(isbn)).Returns(book);
            _mockContext.Setup(c => c.Books.Remove(book)).Verifiable();
            _mockContext.Setup(c => c.SaveChanges()).Verifiable();

            var result = _repository.DeleteBookByISBN(isbn);

            // Assert that the book was deleted and saved
            Assert.NotNull(result);
            Assert.Equal("Book 1", result.Title);

            _mockContext.Verify(c => c.Books.Remove(book), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Test: DeleteBookByISBN throws an exception when the book is not found
        [Fact]
        public void DeleteBookByISBN_ShouldThrowException_WhenBookNotFound()
        {
            var isbn = "00000";

            _mockContext.Setup(c => c.Books.Find(isbn)).Returns((Book)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.DeleteBookByISBN(isbn));  // Corrected to InvalidOperationException
            Assert.Equal("Book not found.", exception.Message);  // Corrected message
        }

       
    }
}
