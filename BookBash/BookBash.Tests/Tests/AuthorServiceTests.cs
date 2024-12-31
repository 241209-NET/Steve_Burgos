using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using BookBash.API.Model;
using BookBash.API.Repository;
using BookBash.API.Service;

namespace BookBashTest
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _mockAuthorRepository;
        private readonly IAuthorService _authorService;

        public AuthorServiceTests()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _authorService = new AuthorService(_mockAuthorRepository.Object);
        }

        [Fact]
        public void GetAllAuthors_ReturnsAllAuthors()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { ID = Guid.NewGuid(), Name = "Author 1" },
                new Author { ID = Guid.NewGuid(), Name = "Author 2" }
            };
            _mockAuthorRepository.Setup(repo => repo.GetAllAuthors()).Returns(authors);

            // Act
            var result = _authorService.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, author => author.Name == "Author 1");
            Assert.Contains(result, author => author.Name == "Author 2");
        }

        [Fact]
        public void CreateNewAuthor_CreatesAuthor()
        {
            // Arrange
            var newAuthor = new Author { ID = Guid.NewGuid(), Name = "New Author" };
            _mockAuthorRepository.Setup(repo => repo.CreateNewAuthor(It.IsAny<Author>())).Returns(newAuthor);

            // Act
            var result = _authorService.CreateNewAuthor(newAuthor);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Author", result.Name);
        }

        [Fact]
        public void GetAuthorByID_ReturnsAuthor_WhenExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author { ID = authorId, Name = "Existing Author" };
            _mockAuthorRepository.Setup(repo => repo.GetAuthorByID(authorId)).Returns(author);

            // Act
            var result = _authorService.GetAuthorByID(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Existing Author", result?.Name);
        }

        [Fact]
        public void GetAuthorByID_ReturnsNull_WhenDoesNotExist()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            _mockAuthorRepository.Setup(repo => repo.GetAuthorByID(authorId)).Returns((Author?)null);

            // Act
            var result = _authorService.GetAuthorByID(authorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteAuthorByID_DeletesAuthor_WhenExists()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author { ID = authorId, Name = "Author to Delete" };
            _mockAuthorRepository.Setup(repo => repo.GetAuthorByID(authorId)).Returns(author);
            _mockAuthorRepository.Setup(repo => repo.DeleteAuthorByID(authorId)).Verifiable();

            // Act
            var result = _authorService.DeleteAuthorByID(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Author to Delete", result?.Name);
            _mockAuthorRepository.Verify(repo => repo.DeleteAuthorByID(authorId), Times.Once);
        }

        [Fact]
        public void DeleteAuthorByID_ReturnsNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            _mockAuthorRepository.Setup(repo => repo.GetAuthorByID(authorId)).Returns((Author?)null);

            // Act
            var result = _authorService.DeleteAuthorByID(authorId);

            // Assert
            Assert.Null(result);
            _mockAuthorRepository.Verify(repo => repo.DeleteAuthorByID(It.IsAny<Guid>()), Times.Never);
        }
    }
}
