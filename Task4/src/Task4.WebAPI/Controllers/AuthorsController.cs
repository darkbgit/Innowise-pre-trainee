using Microsoft.AspNetCore.Mvc;
using Task4.Core.DTOs;
using Task4.Core.Interfaces.Services;

namespace Task4.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController(IAuthorService authorsService) : ControllerBase
{
    private readonly IAuthorService _authorsService = authorsService;

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetById(int id)
    {
        var author = await _authorsService.GetAuthorByIdAsync(id);

        return Ok(author);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AuthorDto>>> Get()
    {
        var result = await _authorsService.GetAllAuthorsAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorForCreateDto author)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authorsService.AddAsync(author);

        return CreatedAtAction(nameof(GetById), new { id = result }, null);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, AuthorForUpdateDto author)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _authorsService.UpdateAsync(id, author);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _authorsService.DeleteAsync(id);

        return NoContent();
    }
}
