namespace BookBash.API.Model
{
    public class Author 
    {
        public required Guid ID { get; set; }
        public required string Name { get; set; }

        // Navigation property for the many-to-many relationship with Book
        public ICollection<AuthorBook>? AuthorBooks { get; set; }
    }
}
