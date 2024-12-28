namespace BookBash.API.Model
{
    public class AuthorBook 
    {
        public required Guid AuthorID { get; set; }
        public required string BookISBN { get; set; }

      
    }
}
