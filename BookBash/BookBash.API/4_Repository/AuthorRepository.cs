using BookBash.API.Data;
using BookBash.API.Model;

namespace BookBash.API.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookBashContext _authorContext;

    public AuthorRepository(BookBashContext authorContext) => _authorContext = authorContext;

    public IEnumerable<Author> GetAllAuthors()
    {
        if (_authorContext.Authors == null)
        {
            throw new InvalidOperationException("Authors DbSet is null.");
        }

        return _authorContext.Authors.ToList();
    }

    public Author CreateNewAuthor(Author author)
    {
        if (_authorContext.Authors == null)
        {
            throw new InvalidOperationException("Authors DbSet is null.");
        }
        _authorContext.Authors.Add(author);
        _authorContext.SaveChanges();
        return author;
    }

    public Author? GetAuthorByID(Guid id)
    {
        if (_authorContext.Authors == null)
        {
            // Handle the case where Authors is null
            throw new InvalidOperationException("Authors DbSet is null.");
        }

        return _authorContext.Authors.Find(id);
        
    }

    public void DeleteAuthorByID(Guid id)
    {

       Author? author = GetAuthorByID(id);

        if (author == null)
        {
            throw new Exception($"Author with ID {id} not found.");
        }
        if (_authorContext.Authors == null)
        {
            throw new InvalidOperationException("Authors DbSet is null.");
        }

        _authorContext.Authors.Remove(author);

        _authorContext.SaveChanges();
    }

    public IEnumerable<Author> GetAuthorByName(string name)
    {
       throw new NotImplementedException();
       
       //var author = _petContext.Pets.Where(p => p.Name.Equals(name)).ToList();
    }
}