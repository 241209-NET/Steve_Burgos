using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Service;

public class BookService : IBookService
{

    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository) => _bookRepository = bookRepository;
    public Book CreateNewBook(Book book)
    {
        return _bookRepository.CreateNewBook(book);
    }

    public Book? DeleteBookByISBN(string isbn)
    {
        var book = GetBookByISBN(isbn);
        
        if(book is not null) _bookRepository.DeleteBookByISBN(isbn);
        
        return book;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _bookRepository.GetAllBooks();
    }

    public Book? GetBookByISBN(string isbn)
    {
        return _bookRepository.GetBookByISBN(isbn);
    }

    public IEnumerable<Book> GetBookByTitle(string title)
    {
        throw new NotImplementedException();
    }
}