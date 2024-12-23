using Microsoft.EntityFrameworkCore;
using BookBash.API.Model;

namespace BookBash.API.Data
{
    public partial class BookBashContext : DbContext
    {
        public BookBashContext() { }
        public BookBashContext(DbContextOptions<BookBashContext> options) : base(options) { }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookList> BookLists { get; set; } = null!;
        public virtual DbSet<AuthorBook> AuthorBooks { get; set; } = null!;
        public virtual DbSet<BookBookList> BookBookLists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasKey(b => new {b.ISBN});

            // Composite key for AuthorBook (AuthorID, BookISBN)
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorID, ab.BookISBN });

            // Composite key for BookBookList (BookISBN, BookListID)
            modelBuilder.Entity<BookBookList>()
                .HasKey(bb => new { bb.BookISBN, bb.BookListID });

            // Configure many-to-many relationship between Author and Book via AuthorBook
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorID);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookISBN);

            // Configure many-to-many relationship between Book and BookList via BookBookList
            modelBuilder.Entity<BookBookList>()
                .HasOne(bb => bb.Book)
                .WithMany(b => b.BookBookLists)
                .HasForeignKey(bb => bb.BookISBN);

            modelBuilder.Entity<BookBookList>()
                .HasOne(bb => bb.BookList)
                .WithMany(bl => bl.BookBookLists)
                .HasForeignKey(bb => bb.BookListID);
        }
    }
}
