using BookBash.API.Data;
using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Repository;

public class BookRepository : IBookRepository
{

    private readonly BookBashContext _bookContext;

    public BookRepository(BookBashContext bookContext) => _bookContext = bookContext;
    public Book CreateNewBook(Book book)
    {
         if (_bookContext.Books == null)
        {
            throw new InvalidOperationException("Books DbSet is null.");
        }
        _bookContext.Books.Add(book);
        _bookContext.SaveChanges();
        return book;
    }

    public Book DeleteBookByISBN(string isbn)
    {
         Book? book = GetBookByISBN(isbn);

        if (book == null)
        {
            throw new Exception($"Book with ISBN {isbn} not found.");
        }
        if (_bookContext.Books == null)
        {
            throw new InvalidOperationException("Book DbSet is null.");
        }

        _bookContext.Books.Remove(book);

        _bookContext.SaveChanges();

        return book;

    }

    public IEnumerable<Book> GetAllBooks()
    {
         if (_bookContext.Books == null)
        {
            throw new InvalidOperationException("Book DbSet is null.");
        }

        return _bookContext.Books.ToList();
    }

    public Book GetBookByISBN(string isbn)
    {
         if (_bookContext.Books == null)
        {
            // Handle the case where Authors is null
            throw new InvalidOperationException("Book DbSet is null.");
        }

        Book? book = _bookContext.Books.Find(isbn);

        if(book == null)
        {
            // Handle the case where Authors is null
            throw new InvalidOperationException("Book not found.");
        }
        return book;
        
    }

    public IEnumerable<Book> GetBookByTitle(string title)
    {
        throw new NotImplementedException();
    }
}
