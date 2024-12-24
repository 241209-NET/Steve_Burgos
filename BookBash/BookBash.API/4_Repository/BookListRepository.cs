using BookBash.API.Data;
using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Repository;

public class BookListRepository : IBookListRepository
{

    private readonly BookBashContext _bookListContext;

    public BookListRepository(BookBashContext bookListContext) => _bookListContext = bookListContext;
    public BookList CreateNewBookList(BookList bookList)
    {
         if (_bookListContext.BookLists == null)
        {
            throw new InvalidOperationException("BookLists DbSet is null.");
        }
        _bookListContext.BookLists.Add(bookList);
        _bookListContext.SaveChanges();
        return bookList;
    }

    public void DeleteBookListByID(Guid id)
    {
         BookList? bookList = GetBookListByID(id);

        if (bookList == null)
        {
            throw new Exception($"BookList with ID {id} not found.");
        }
        if (_bookListContext.BookLists == null)
        {
            throw new InvalidOperationException("BookList DbSet is null.");
        }

        _bookListContext.BookLists.Remove(bookList);

        _bookListContext.SaveChanges();

    }

    public IEnumerable<BookList> GetAllBookLists()
    {
         if (_bookListContext.BookLists == null)
        {
            throw new InvalidOperationException("BookList DbSet is null.");
        }

        return _bookListContext.BookLists.ToList();
    }

    public BookList GetBookListByID(Guid id)
    {
         if (_bookListContext.BookLists == null)
        {
            // Handle the case where Authors is null
            throw new InvalidOperationException("BookList DbSet is null.");
        }

        BookList? bookList = _bookListContext.BookLists.Find(id);

        if(bookList == null)
        {
            // Handle the case where Authors is null
            throw new InvalidOperationException("BookList not found.");
        }
        return bookList;
        
    }

    public IEnumerable<Author> GetBookListByName(string name)
    {
        throw new NotImplementedException();
    }
}
