using System.ComponentModel.DataAnnotations;

namespace Task4.Core.DTOs;

public class AuthorForUpdateDto
{
    [StringLength(100, MinimumLength = 2)]
    public string? Name { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? DateOfBirth { get; set; }
}
