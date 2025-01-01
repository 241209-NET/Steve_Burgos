using Microsoft.AspNetCore.Mvc;
using BookBash.API.Model;
using BookBash.API.Service;

namespace BookBash.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class BookListController : ControllerBase
{   
    private readonly IBookListService _bookListService;
    private readonly IBookService _bookService;

    private readonly IBookBookListService _bookBookListService;

    public BookListController(IBookListService bookListService, IBookService bookService, IBookBookListService bookBookListService)
    {
        _bookListService = bookListService;
        _bookService = bookService;
        _bookBookListService = bookBookListService;
    }

    [HttpGet]
    public IActionResult GetAllBookLists()
    {
        var bookListList = _bookListService.GetAllBookLists();        
        return Ok(bookListList);
    }

    [HttpPost]
    public IActionResult CreateNewBookList(BookList bookList)
    { 
        return Ok(_bookListService.CreateNewBookList(bookList));
    }

    [HttpGet("{id}")]
    public IActionResult GetBookListByID(Guid id)
    {
        return Ok(_bookListService.GetBookListByID(id));
    }

    [HttpDelete]
    public IActionResult DeleteBookList(Guid id)
    {
        return Ok(_bookListService.DeleteBookListByID(id));
    }

    [HttpPost("{id}/book")]
    public IActionResult AddBookToList([FromBody] string isbn, Guid id)
    {
        BookBookList bookBookList = new BookBookList 
        {
            BookISBN = isbn,
            BookListID = id
        };
        return Ok(_bookBookListService.CreateNewBookBookList(bookBookList));
    }

     [HttpDelete("{id}/book")]
    public IActionResult DeleteBookFromList([FromBody] string isbn, Guid id)
    {

        return Ok(_bookBookListService.DeleteBookBookListByID(id, isbn));
    }
    
}
