using BookBash.API.Data;
using BookBash.API.Model;

namespace BookBash.API.Repository;

public class AuthorBookRepository : IAuthorBookRepository
{
    private readonly BookBashContext _authorBookContext;

    public AuthorBookRepository(BookBashContext authorBookContext) => _authorBookContext = authorBookContext;

    public AuthorBook CreateNewAuthorBook(AuthorBook authorBook)
    {
        if (_authorBookContext.AuthorBooks == null)
        {
            throw new InvalidOperationException("AuthorBookss DbSet is null.");
        }
        _authorBookContext.AuthorBooks.Add(authorBook);
        _authorBookContext.SaveChanges();
        return authorBook;
    }

    public void DeleteAuthorBookByID(Guid id)
    {
         AuthorBook? authorBook = GetAuthorBookByID(id);

        if (authorBook == null)
        {
            throw new Exception($"AuthorBook with ID {id} not found.");
        }
        if (_authorBookContext.AuthorBooks == null)
        {
            throw new InvalidOperationException("AuthorBooks DbSet is null.");
        }

        _authorBookContext.AuthorBooks.Remove(authorBook);

        _authorBookContext.SaveChanges();

    }

    public IEnumerable<AuthorBook> GetAllAuthorBooks()
    {
        if (_authorBookContext.AuthorBooks == null)
        {
            throw new InvalidOperationException("AuthorBooks DbSet is null.");
        }

        return _authorBookContext.AuthorBooks.ToList();
    }

    public AuthorBook GetAuthorBookByID(Guid id)
    {
        // Checks before code
         if (_authorBookContext.AuthorBooks == null)
        {
            throw new InvalidOperationException("AuthorBooks DbSet is null.");
        }

        AuthorBook? authorBook = _authorBookContext.AuthorBooks.Find(id);
        
        //checks after code
        if (authorBook == null)
        {
            throw new InvalidOperationException("AuthorBooks is could not be found.");
        }

        return authorBook;
    }
}