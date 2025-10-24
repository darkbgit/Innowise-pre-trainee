using Microsoft.AspNetCore.Mvc;
using Task4.Core.DTOs;
using Task4.Core.Interfaces.Services;

namespace Task4.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
    private readonly IBookService _bookService = bookService;

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        return Ok(book);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookDto>>> Get()
    {
        var result = await _bookService.GetAllBooksAsync();

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IReadOnlyList<AuthorDto>>> SearchAuthors([FromQuery] int? authorId, [FromQuery] int? publishedAfter, [FromQuery] int? publishedBefore)
    {
        var result = await _bookService.SearchBooksAsync(authorId, publishedAfter, publishedBefore);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookForCreateDto book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _bookService.AddAsync(book);

        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, BookForUpdateDto book)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _bookService.UpdateAsync(id, book);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteAsync(id);

        return NoContent();
    }
}
