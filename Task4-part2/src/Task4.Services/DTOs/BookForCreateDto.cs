using System.ComponentModel.DataAnnotations;

namespace Task4.Core.DTOs;

public class BookForCreateDto
{
    [Required]
    [StringLength(1000, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;
    [Required]
    public int PublishedYear { get; set; }
    [Required]
    public int AuthorId { get; set; }
}
