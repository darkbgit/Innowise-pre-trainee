using System.ComponentModel.DataAnnotations;

namespace Task4.Core.DTOs;

public class BookForUpdateDto
{
    [StringLength(1000, MinimumLength = 2)]
    public string? Title { get; set; }
    public int? PublishedYear { get; set; }
    public int? AuthorId { get; set; }
}
