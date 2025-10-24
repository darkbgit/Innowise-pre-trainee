namespace Task4.Domain.Entities;

public class Author : IBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public ICollection<Book> Books { get; set; } = [];
}
