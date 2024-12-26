using BookBash.API.Model;
using BookBash.API.Repository;

namespace BookBash.API.Service;

public class BookBookListService : IBookBookListService
{

    private readonly IBookBookListRepository _bookBookListRepository;

    public BookBookListService(IBookBookListRepository bookBookListRepository) => _bookBookListRepository = bookBookListRepository;

    public BookBookList CreateNewBookBookList(BookBookList bookBookList)
    {
        return _bookBookListRepository.CreateNewBookBookList(bookBookList);
    }

    public BookBookList? DeleteBookBookListByID(Guid id)
    {
        var bookBookList = GetBookBookListByID(id);

        if(bookBookList is not null) _bookBookListRepository.DeleteBookBookListByID(id);

        return bookBookList;
    }

    public IEnumerable<BookBookList> GetAllBookBookLists()
    {
        return _bookBookListRepository.GetAllBookBookLists();
    }

    public BookBookList GetBookBookListByID(Guid id)
    {
        return _bookBookListRepository.GetBookBookListByID(id);
    }

}