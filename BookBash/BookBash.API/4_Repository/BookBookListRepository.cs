using BookBash.API.Data;
using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Repository;

public class BookBookListRepository : IBookBookListRepository
{

    private readonly BookBashContext _bookBookListContext;

    public BookBookListRepository(BookBashContext bookBookListContext) => _bookBookListContext = bookBookListContext;
    public BookBookList CreateNewBookBookList(BookBookList bookBookList)
    {
         if (_bookBookListContext.BookBookLists == null)
        {
            throw new InvalidOperationException("BookBookLists DbSet is null.");
        }
        _bookBookListContext.BookBookLists.Add(bookBookList);
        _bookBookListContext.SaveChanges();
        return bookBookList;
    }

    public BookBookList DeleteBookBookListByID(Guid id, string isbn)
    {
         BookBookList? bookBookList = GetBookBookListByID(id, isbn);

        if (bookBookList == null)
        {
            throw new Exception($"BookBookList with ID {id} not found.");
        }
        if (_bookBookListContext.BookBookLists == null)
        {
            throw new InvalidOperationException("BookBookList DbSet is null.");
        }

        _bookBookListContext.BookBookLists.Remove(bookBookList);

        _bookBookListContext.SaveChanges();

        return bookBookList;

    }

    public IEnumerable<BookBookList> GetAllBookBookLists()
    {
         if (_bookBookListContext.BookBookLists == null)
        {
            throw new InvalidOperationException("BookBookList DbSet is null.");
        }

        return _bookBookListContext.BookBookLists.ToList();
    }

    public BookBookList GetBookBookListByID(Guid id, string isbn)
    {
         if (_bookBookListContext.BookBookLists == null)
        {
            // Handle the case where Authors is null
            throw new InvalidOperationException("BookBookList DbSet is null.");
        }

        BookBookList? bookBookList = _bookBookListContext.BookBookLists
        .FirstOrDefault(b => b.BookListID == id && b.BookISBN == isbn);

        if(bookBookList == null)
        {
            // Handle the case where Authors is null
            throw new InvalidOperationException("BookBookList not found.");
        }
        return bookBookList;
        
    }
}
