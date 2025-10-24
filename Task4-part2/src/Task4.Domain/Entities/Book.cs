namespace Task4.Domain.Entities;

public class Book : IBaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = default!;
}
