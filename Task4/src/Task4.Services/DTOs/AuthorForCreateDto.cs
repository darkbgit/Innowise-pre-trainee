using System.ComponentModel.DataAnnotations;

namespace Task4.Core.DTOs;

public class AuthorForCreateDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
}
