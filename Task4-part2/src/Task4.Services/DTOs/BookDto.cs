namespace Task4.Core.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}
