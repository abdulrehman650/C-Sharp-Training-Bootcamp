using BookLibrary.Data;
using BookLibrary.DTOs;
using BookLibrary.Mappers;

namespace BookLibrary.Services;

public class BookService : IBookService
{
    public Task<IEnumerable<BookResponseDTO>> GetAllAsync(
        string? author,
        int page,
        int pageSize)
    {
        var query = InMemoryStore.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(b =>
                b.Author != null &&
                b.Author.Name.Contains(author));
        }

        var skip = (page - 1) * pageSize;

        var books = query
            .Skip(skip)
            .Take(pageSize)
            .ToList();

        var result = books
            .Select(BookMapper.ToResponse);

        return Task.FromResult(result);
    }

    public Task<BookResponseDTO?> GetByIdAsync(int id)
    {
        var book = InMemoryStore.Books
            .FirstOrDefault(b => b.Id == id);

        if (book == null)
            return Task.FromResult<BookResponseDTO?>(null);

        return Task.FromResult<BookResponseDTO?>(BookMapper.ToResponse(book));
    }

    public Task<BookResponseDTO> CreateAsync(BookCreateDTO dto)
    {
        var book = BookMapper.ToEntity(dto);

        book.Id = InMemoryStore.Books.Count == 0
            ? 1
            : InMemoryStore.Books.Max(b => b.Id) + 1;

        var author = InMemoryStore.Authors.FirstOrDefault(a => a.Id == book.AuthorId);
        if (author is not null)
        {
            book.Author = author;
            author.Books.Add(book);
        }

        InMemoryStore.Books.Add(book);

        return Task.FromResult(BookMapper.ToResponse(book));
    }

    public Task<bool> UpdateAsync(int id, BookUpdateDTO dto)
    {
        var book = InMemoryStore.Books
            .FirstOrDefault(b => b.Id == id);

        if (book == null)
            return Task.FromResult(false);

        BookMapper.ApplyUpdate(dto, book);

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var book = InMemoryStore.Books
            .FirstOrDefault(b => b.Id == id);

        if (book == null)
            return Task.FromResult(false);

        InMemoryStore.Books.Remove(book);

        return Task.FromResult(true);
    }
}
