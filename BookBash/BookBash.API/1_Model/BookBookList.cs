namespace BookBash.API.Model
{
    public class BookBookList
    {
        public required string BookISBN { get; set; } // Foreign key to Book
        public required Guid BookListID { get; set; } // Foreign key to BookList

        // Navigation properties
        public  Book Book { get; set; }
        public  BookList BookList { get; set; }
    }
}
