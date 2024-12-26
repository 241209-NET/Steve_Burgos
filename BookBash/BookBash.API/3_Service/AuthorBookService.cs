using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Service;

public class AuthorBookService : IAuthorBookService
{
    private readonly IAuthorBookRepository _authorBookRepository;

    public AuthorBookService(IAuthorBookRepository authorBookRepository) => _authorBookRepository = authorBookRepository;

    public AuthorBook CreateNewAuthorBook(AuthorBook authorBook)
    {
        return _authorBookRepository.CreateNewAuthorBook(authorBook);
    }

    public AuthorBook? DeleteAuthorBookByID(Guid id)
    {
        var authorBook = GetAuthorBookByID(id);
        if(authorBook is not null) _authorBookRepository.DeleteAuthorBookByID(id);
        return authorBook;
    }

    public IEnumerable<AuthorBook> GetAllAuthorBooks()
    {
        return _authorBookRepository.GetAllAuthorBooks();
    }

    public AuthorBook GetAuthorBookByID(Guid id)
    {
        return _authorBookRepository.GetAuthorBookByID(id);
    }
}