using BookLibrary.DTOs;
using BookLibrary.Entities;

namespace BookLibrary.Mappers;


public static class BookMapper
{
    public static Book ToEntity(BookCreateDTO dto)
    {
        return new Book
        {
            Title = dto.Title,
            PageCount = dto.PageCount,
            AuthorId = dto.AuthorId
        };
    }

    public static void ApplyUpdate(BookUpdateDTO dto, Book book)
    {
        book.Title = dto.Title;
        book.PageCount = dto.PageCount;
        book.AuthorId = dto.AuthorId;
    }
    public static BookResponseDTO ToResponse(Book book)
    {
        return new BookResponseDTO
        {
            Id = book.Id,
            Title = book.Title,
            PageCount = book.PageCount,
            AuthorId = book.AuthorId
        };
    }
}
