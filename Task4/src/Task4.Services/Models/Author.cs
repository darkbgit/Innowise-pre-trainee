namespace Task4.Core.Models;

public class Author : IBaseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}
