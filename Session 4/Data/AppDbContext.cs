using Microsoft.EntityFrameworkCore;
using BookLibrary.Entities;

namespace BookLibrary.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }      // Books table
    public DbSet<Author> Authors { get; set; }  // Authors table
    public DbSet<Tag> Tags { get; set; }        // Tags table
    public DbSet<BookTag> BookTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BookTag>()
            .HasKey(bt => new { bt.BookId, bt.TagId });

        modelBuilder.Entity<BookTag>()
            .HasOne<Book>()
            .WithMany(b => b.BookTags)
            .HasForeignKey(bt => bt.BookId);

        modelBuilder.Entity<BookTag>()
            .HasOne<Tag>()
            .WithMany(t => t.BookTags)
            .HasForeignKey(bt => bt.TagId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);


        // 3 authors
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "George Orwell" },
            new Author { Id = 2, Name = "Jane Austen" },
            new Author { Id = 3, Name = "J.R.R. Tolkien" }
        );

        // 4 tags
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Classic" },
            new Tag { Id = 2, Name = "Fantasy" },
            new Tag { Id = 3, Name = "Political" },
            new Tag { Id = 4, Name = "Romance" }
        );

        // 8 books 
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "1984",                       Year = 1949, PageCount = 328, AuthorId = 1 },
            new Book { Id = 2, Title = "Animal Farm",                Year = 1945, PageCount = 112, AuthorId = 1 },
            new Book { Id = 3, Title = "Homage to Catalonia",        Year = 1938, PageCount = 232, AuthorId = 1 },
            new Book { Id = 4, Title = "Pride and Prejudice",        Year = 1813, PageCount = 432, AuthorId = 2 },
            new Book { Id = 5, Title = "Emma",                       Year = 1815, PageCount = 474, AuthorId = 2 },
            new Book { Id = 6, Title = "The Hobbit",                 Year = 1937, PageCount = 310, AuthorId = 3 },
            new Book { Id = 7, Title = "The Fellowship of the Ring", Year = 1954, PageCount = 423, AuthorId = 3 },
            new Book { Id = 8, Title = "The Two Towers",             Year = 1954, PageCount = 352, AuthorId = 3 }
        );

        // 6 BookTag links 
        modelBuilder.Entity<BookTag>().HasData(
            new BookTag { BookId = 1, TagId = 1 }, // 1984 -> Classic
            new BookTag { BookId = 1, TagId = 3 }, // 1984 -> Political
            new BookTag { BookId = 2, TagId = 3 }, // Animal Farm -> Political
            new BookTag { BookId = 4, TagId = 4 }, // Pride and Prejudice -> Romance
            new BookTag { BookId = 6, TagId = 2 }, // The Hobbit -> Fantasy
            new BookTag { BookId = 7, TagId = 2 }  // Fellowship -> Fantasy
        );
    }
}
