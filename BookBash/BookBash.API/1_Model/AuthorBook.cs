namespace BookBash.API.Model
{
    public class AuthorBook 
    {
        public required Guid AuthorID { get; set; }
        public required string BookISBN { get; set; }

        // Navigation properties
        public  Author Author { get; set; }
        public  Book Book { get; set; }
    }
}
