namespace Task4.Core.DTOs;

public class BookForCreate
{
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
}
