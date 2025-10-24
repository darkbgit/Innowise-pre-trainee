namespace Task4.Core.DTOs;

public class AuthorWithBooksNumberDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int BooksNumber { get; set; }
}
