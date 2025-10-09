using System.ComponentModel.DataAnnotations;

namespace Task4.Core.DTOs;

public class BookForUpdateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public int PublishedYear { get; set; }
    [Required]
    public int AuthorId { get; set; }
}
