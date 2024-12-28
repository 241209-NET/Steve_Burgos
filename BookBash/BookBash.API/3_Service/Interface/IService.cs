using BookBash.API.Model;

namespace BookBash.API.Service;

public interface IAuthorService
{
    Author CreateNewAuthor(Author author);
    IEnumerable<Author> GetAllAuthors();
    Author? GetAuthorByID(Guid id);
    IEnumerable<Author> GetAuthorByName(string name);
    Author? DeleteAuthorByID(Guid id);
}

public interface IBookService
{
    Book CreateNewBook(Book book);
    IEnumerable<Book> GetAllBooks();
    Book? GetBookByISBN(string isbn);
    IEnumerable<Book> GetBookByTitle(string title);
    Book? DeleteBookByISBN(string isbn);
    IEnumerable<Book> GetBookFromBookList(Guid bookListID);
}

public interface IBookListService
{
    BookList CreateNewBookList(BookList list);
    IEnumerable<BookList> GetAllBookLists();
    BookList? GetBookListByID(Guid id);
    IEnumerable<BookList> GetBookListByName(string name);
    BookList? DeleteBookListByID(Guid id);
}

public interface IAuthorBookService
{
    //CRUD
    AuthorBook CreateNewAuthorBook(AuthorBook authorBook); 
    IEnumerable<AuthorBook> GetAllAuthorBooks(); 
    AuthorBook GetAuthorBookByID(Guid id); 
    AuthorBook? DeleteAuthorBookByID(Guid id);    
}
public interface IBookBookListService
{
    //CRUD
    BookBookList CreateNewBookBookList(BookBookList bookBookList); 
    IEnumerable<BookBookList> GetAllBookBookLists(); 
    BookBookList GetBookBookListByID(Guid id); 
    BookBookList? DeleteBookBookListByID(Guid id);    
}
