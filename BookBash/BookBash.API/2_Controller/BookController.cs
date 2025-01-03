using Microsoft.AspNetCore.Mvc;
using BookBash.API.Model;
using BookBash.API.Service;

namespace BookBash.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{   
    private readonly IBookService _bookService;
    private readonly IBookListService _bookListService;

    public BookController(IBookService bookService, IBookListService bookListService)
    {
        _bookService = bookService;
        _bookListService = bookListService;

    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var bookList = _bookService.GetAllBooks();        
        return Ok(bookList);
    }

    [HttpPost]
    public IActionResult CreateNewBook(Book book)
    { 
        return Ok(_bookService.CreateNewBook(book));
    }

    [HttpGet("{isbn}")]
    public IActionResult GetBookByISBN(string isbn)
    {
        return Ok(_bookService.GetBookByISBN(isbn));
    }

    [HttpDelete]
    public IActionResult DeleteBook(string isbn)
    {
        return Ok(_bookService.DeleteBookByISBN(isbn));
    }
    
    [HttpGet("booklist/{bookListID}")]

    public IActionResult GetBooksFromBookList(Guid bookListID)
    {
        
        return Ok(_bookService.GetBookFromBookList(bookListID));
    }

}