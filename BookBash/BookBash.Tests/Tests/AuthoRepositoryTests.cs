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
    public class AuthorRepositoryTests
    {
        private readonly Mock<BookBashContext> _mockContext;
        private readonly Mock<DbSet<Author>> _mockAuthorDbSet;
        private readonly AuthorRepository _repository;

        public AuthorRepositoryTests()
        {
            // Initialize the mock context and DbSet
            _mockContext = new Mock<BookBashContext>();
            _mockAuthorDbSet = new Mock<DbSet<Author>>();

            // Mock IQueryable<Author> as we can't mock LINQ extension methods like ToList directly
            var authors = new List<Author>
            {
                new Author { ID = Guid.NewGuid(), Name = "Author 1" },
                new Author { ID = Guid.NewGuid(), Name = "Author 2" }
            }.AsQueryable();

            _mockAuthorDbSet.As<IQueryable<Author>>().Setup(m => m.Provider).Returns(authors.Provider);
            _mockAuthorDbSet.As<IQueryable<Author>>().Setup(m => m.Expression).Returns(authors.Expression);
            _mockAuthorDbSet.As<IQueryable<Author>>().Setup(m => m.ElementType).Returns(authors.ElementType);
            _mockAuthorDbSet.As<IQueryable<Author>>().Setup(m => m.GetEnumerator()).Returns(authors.GetEnumerator());

            // Setup mock context to return the mocked DbSet
            _mockContext.Setup(c => c.Authors).Returns(_mockAuthorDbSet.Object);

            // Initialize the repository with the mock context
            _repository = new AuthorRepository(_mockContext.Object);
        }

        // Test: GetAllAuthors returns authors when Authors DbSet is not empty
        [Fact]
        public void GetAllAuthors_ShouldReturnAuthors_WhenAuthorsExist()
        {
            var result = _repository.GetAllAuthors();

            // Assert that the result is not empty and contains the correct number of authors
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        // Test: GetAllAuthors throws an exception when Authors DbSet is null
        [Fact]
        public void GetAllAuthors_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.Authors).Returns((DbSet<Author>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.GetAllAuthors());
            Assert.Equal("Authors DbSet is null.", exception.Message);
        }

        // Test: CreateNewAuthor returns the author when successfully created
        [Fact]
        public void CreateNewAuthor_ShouldReturnAuthor_WhenCreatedSuccessfully()
        {
            var newAuthor = new Author { ID = Guid.NewGuid(), Name = "New Author" };

            // Setup to simulate adding the author
            _mockContext.Setup(c => c.Authors.Add(It.IsAny<Author>())).Verifiable();
            _mockContext.Setup(c => c.SaveChanges()).Verifiable();

            var result = _repository.CreateNewAuthor(newAuthor);

            // Assert that the author was added and saved
            Assert.NotNull(result);
            Assert.Equal("New Author", result.Name);

            _mockContext.Verify(c => c.Authors.Add(It.IsAny<Author>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Test: CreateNewAuthor throws an exception when Authors DbSet is null
        [Fact]
        public void CreateNewAuthor_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.Authors).Returns((DbSet<Author>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.CreateNewAuthor(new Author { ID = Guid.NewGuid(), Name = "Test Author" }));
            Assert.Equal("Authors DbSet is null.", exception.Message);
        }

        // Test: GetAuthorByID returns author when found
        [Fact]
        public void GetAuthorByID_ShouldReturnAuthor_WhenAuthorExists()
        {
            var ID = Guid.NewGuid();
            var author = new Author { ID = ID, Name = "Author 1" };

            _mockContext.Setup(c => c.Authors.Find(ID)).Returns(author);

            var result = _repository.GetAuthorByID(ID);

            Assert.NotNull(result);
            Assert.Equal("Author 1", result?.Name);
        }

        // Test: GetAuthorByID returns null when author does not exist
        [Fact]
        public void GetAuthorByID_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            var ID = Guid.NewGuid();

            _mockContext.Setup(c => c.Authors.Find(ID)).Returns((Author)null);

            var result = _repository.GetAuthorByID(ID);

            Assert.Null(result);
        }

        // Test: DeleteAuthorByID deletes author and returns the author when successful
        [Fact]
        public void DeleteAuthorByID_ShouldReturnAuthor_WhenAuthorDeleted()
        {
            var ID = Guid.NewGuid();
            var author = new Author { ID = ID, Name = "Author 1" };

            // Setup to return an author when Find is called
            _mockContext.Setup(c => c.Authors.Find(ID)).Returns(author);
            _mockContext.Setup(c => c.Authors.Remove(author)).Verifiable();
            _mockContext.Setup(c => c.SaveChanges()).Verifiable();

            var result = _repository.DeleteAuthorByID(ID);

            // Assert that the author was deleted and saved
            Assert.NotNull(result);
            Assert.Equal("Author 1", result.Name);

            _mockContext.Verify(c => c.Authors.Remove(author), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Test: DeleteAuthorByID throws an exception when the author is not found
        [Fact]
        public void DeleteAuthorByID_ShouldThrowException_WhenAuthorNotFound()
        {
            var ID = Guid.NewGuid();

            _mockContext.Setup(c => c.Authors.Find(ID)).Returns((Author)null);

            var exception = Assert.Throws<Exception>(() => _repository.DeleteAuthorByID(ID));
            Assert.Equal($"Author with ID {ID} not found.", exception.Message);
        }

        // Test: DeleteAuthorByID throws an exception when Authors DbSet is null
        [Fact]
        public void DeleteAuthorByID_ShouldThrowException_WhenDbSetIsNull()
        {
            _mockContext.Setup(c => c.Authors).Returns((DbSet<Author>)null);

            var exception = Assert.Throws<InvalidOperationException>(() => _repository.DeleteAuthorByID(Guid.NewGuid()));
            Assert.Equal("Authors DbSet is null.", exception.Message);
        }
    }
}
