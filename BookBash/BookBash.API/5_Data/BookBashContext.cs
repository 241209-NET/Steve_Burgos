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
                .HasKey(b => b.ISBN);

            modelBuilder.Entity<AuthorBook>()
                .HasKey(ab => new { ab.AuthorID, ab.BookISBN });

            modelBuilder.Entity<BookBookList>()
                .HasKey(bb => new { bb.BookISBN, bb.BookListID });

            modelBuilder.Entity<AuthorBook>()
                .HasOne<Author>() 
                .WithMany()  
                .HasForeignKey(ab => ab.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<AuthorBook>()
                .HasOne<Book>() 
                .WithMany()  
                .HasForeignKey(ab => ab.BookISBN)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<BookBookList>()
                .HasOne<Book>() 
                .WithMany()  
                .HasForeignKey(bb => bb.BookISBN)
                .OnDelete(DeleteBehavior.Restrict);  

            modelBuilder.Entity<BookBookList>()
                .HasOne<BookList>() 
                .WithMany()  
                .HasForeignKey(bb => bb.BookListID)
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
