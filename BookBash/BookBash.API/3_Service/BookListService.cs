using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Service;

public class BookListService : IBookListService
{

    private readonly IBookListRepository _bookListRepository;

    public BookListService(IBookListRepository bookListRepository) => _bookListRepository = bookListRepository;

    public BookList CreateNewBookList(BookList bookList)
    {
        return _bookListRepository.CreateNewBookList(bookList);
    }

    public BookList? DeleteBookListByID(Guid id)
    {
        var bookList = GetBookListByID(id);

        if(bookList is not null) _bookListRepository.DeleteBookListByID(id);

        return bookList;
    }

    public IEnumerable<BookList> GetAllBookLists()
    {
        return _bookListRepository.GetAllBookLists();
    }

    public BookList? GetBookListByID(Guid id)
    {
        return _bookListRepository.GetBookListByID(id);
    }

    public IEnumerable<BookList> GetBookListByName(string name)
    {
        throw new NotImplementedException();
    }
}