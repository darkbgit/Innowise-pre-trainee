namespace Task4.Core.DTOs;

public class AuthorForUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}
