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

            // Configure primary key for Book
            modelBuilder.Entity<Book>()
                .HasKey(b => b.ISBN);

            // Configure composite key for AuthorBook (AuthorID, BookISBN)
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorID, ab.BookISBN });

            // Configure composite key for BookBookList (BookISBN, BookListID)
            modelBuilder.Entity<BookBookList>()
                .HasKey(bb => new { bb.BookISBN, bb.BookListID });

            // Configure relationship between AuthorBook and Author via foreign key
            modelBuilder.Entity<AuthorBook>()
                .HasOne<Author>() // No navigation property, just a foreign key relation
                .WithMany()  // No navigation property on Author
                .HasForeignKey(ab => ab.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);  // Adjust delete behavior as needed

            // Configure relationship between AuthorBook and Book via foreign key
            modelBuilder.Entity<AuthorBook>()
                .HasOne<Book>() // No navigation property, just a foreign key relation
                .WithMany()  // No navigation property on Book
                .HasForeignKey(ab => ab.BookISBN)
                .OnDelete(DeleteBehavior.Restrict);  // Adjust delete behavior as needed

            // Configure relationship between BookBookList and Book via foreign key
            modelBuilder.Entity<BookBookList>()
                .HasOne<Book>() // No navigation property, just a foreign key relation
                .WithMany()  // No navigation property on Book
                .HasForeignKey(bb => bb.BookISBN)
                .OnDelete(DeleteBehavior.Restrict);  // Adjust delete behavior as needed

            // Configure relationship between BookBookList and BookList via foreign key
            modelBuilder.Entity<BookBookList>()
                .HasOne<BookList>() // No navigation property, just a foreign key relation
                .WithMany()  // No navigation property on BookList
                .HasForeignKey(bb => bb.BookListID)
                .OnDelete(DeleteBehavior.Restrict);  // Adjust delete behavior as needed
        }
    }
}
