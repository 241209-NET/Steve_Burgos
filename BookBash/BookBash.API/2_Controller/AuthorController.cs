/*
using Microsoft.AspNetCore.Mvc;
using BookBash.API.Model;
using BookBash.API.Service;

namespace BookBash.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{   
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public IActionResult GetAllAuthors()
    {
        var authorList = _authorService.GetAllAuthors();        
        return Ok(authorList);
    }

    [HttpPost]
    public IActionResult CreateNewAuthor(Author author)
    { 
        return Ok(_authorService.CreateNewAuthor(author));
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorById(Guid id)
    {
        return Ok(_authorService.GetAuthorByID(id));
    }

    [HttpDelete]
    public IActionResult DeleteAuthor(Guid id)
    {
        return Ok(_authorService.DeleteAuthorByID(id));
    }


}
*/