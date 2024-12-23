namespace BookBash.API.Model
{
    public class BookList
    {
        public required Guid ID { get; set; }
        public required string Name { get; set; }

        // Navigation property for the many-to-many relationship with Book
        public ICollection<BookBookList>? BookBookLists { get; set; }
    }
}
