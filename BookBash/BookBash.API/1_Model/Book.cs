namespace BookBash.API.Model
{
    public class Book 
    {
        public required string ISBN { get; set; }
        public required string Title { get; set; }

        // Navigation property for the many-to-many relationship with Author
        public ICollection<AuthorBook>? AuthorBooks { get; set; }

        // Navigation property for the many-to-many relationship with BookList
        public ICollection<BookBookList>? BookBookLists { get; set; }
    }
}
