namespace BookManager.Domain.Entity;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public int ReleaseYear { get; set; }
}
