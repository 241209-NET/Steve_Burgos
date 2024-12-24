using BookBash.API.Model;

namespace BookBash.API.Repository;

public interface IAuthorRepository
{
    //CRUD
    Author CreateNewAuthor(Author author); 
    IEnumerable<Author> GetAllAuthors(); 
    Author? GetAuthorByID(Guid id); 
    IEnumerable<Author> GetAuthorByName(string name);
    void DeleteAuthorByID(Guid id);    
}
public interface IAuthorBookRepository
{
    //CRUD
    AuthorBook CreateNewAuthorBook(AuthorBook authorBook); 
    IEnumerable<AuthorBook> GetAllAuthorBooks(); 
    AuthorBook GetAuthorBookByID(Guid id); 
    void DeleteAuthorBookByID(Guid id);    
}
public interface IBookBookListRepository
{
    //CRUD
    BookBookList CreateNewBookBookList(BookBookList bookBookList); 
    IEnumerable<BookBookList> GetAllBookBookLists(); 
    BookBookList GetBookBookListByID(Guid id); 
    void DeleteBookBookListByID(Guid id);    
}
public interface IBookRepository
{
    //CRUD
    Book CreateNewBook(Book book); 
    IEnumerable<Book> GetAllBooks(); 
    Book GetBookByISBN(string isbn); 
    IEnumerable<Book> GetBookByTitle(string title);
    void DeleteBookByISBN(string isbn);    
}
public interface IBookListRepository
{
    //CRUD
    BookList CreateNewBookList(BookList bookList); 
    IEnumerable<BookList> GetAllBookLists(); 
    BookList? GetBookListByID(Guid id); 
    IEnumerable<Author> GetBookListByName(string name);
    void DeleteBookListByID(Guid id);    
}
