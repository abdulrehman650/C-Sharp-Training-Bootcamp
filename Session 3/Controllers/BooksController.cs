using Microsoft.AspNetCore.Mvc;
using BookLibrary.DTOs;
using BookLibrary.Services;

namespace BookLibrary.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(
        [FromQuery] string? author,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var books = await _bookService.GetAllAsync(author, page, pageSize);
        return Ok(books); 
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] BookCreateDTO dto)
    {
        var created = await _bookService.CreateAsync(dto);

        return Ok(created); 
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] BookUpdateDTO dto)
    {
        var result = await _bookService.UpdateAsync(id, dto);

        if (!result)
            return NotFound(); 

        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _bookService.DeleteAsync(id);

        if (!result)
            return NotFound(); 

        return NoContent(); 
    }
}
