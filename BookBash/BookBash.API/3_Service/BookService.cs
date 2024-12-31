using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Service;

public class BookService : IBookService
{

    private readonly IBookRepository _bookRepository;
    private readonly IBookListRepository _bookListRepository;
    private readonly IBookBookListRepository _bookBookListRepository;

    public BookService(IBookRepository bookRepository, IBookListRepository bookListRepository, IBookBookListRepository bookBookListRepository)
    {
        _bookRepository = bookRepository;
        _bookListRepository = bookListRepository;
        _bookBookListRepository = bookBookListRepository;
    }
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

    public IEnumerable<Book> GetBookFromBookList(Guid bookListID)
    {
        IEnumerable<Book> books= _bookRepository.GetAllBooks();
        IEnumerable<BookBookList> bookBookLists= _bookBookListRepository.GetAllBookBookLists();


        bookBookLists = bookBookLists.Where(bbl => bbl.BookListID == bookListID);

        books = bookBookLists
            .Join(books, 
                bbl => bbl.BookISBN,   
                book => book.ISBN,  
                (bbl, book) => book); 

        return books;

    }
}