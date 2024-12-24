using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Service;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

    public IEnumerable<Author> GetAllAuthors()
    {
        return _authorRepository.GetAllAuthors();
    }

    public Author CreateNewAuthor(Author author)
    {
        return _authorRepository.CreateNewAuthor(author);
    }

    public Author? GetAuthorByID(Guid id)
    {
        return _authorRepository.GetAuthorByID(id);
    }

    public Author? DeleteAuthorByID(Guid id)
    {
        var author = GetAuthorByID(id);
        if(author is not null) _authorRepository.DeleteAuthorByID(id);
        return author;

    }

    public IEnumerable<Author> GetAuthorByName(string name)
    {
        throw new NotImplementedException();
    }

}