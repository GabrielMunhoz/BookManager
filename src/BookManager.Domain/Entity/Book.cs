namespace BookManager.Domain.Entity;

public class Book : Base
{
    public string Title { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public DateTime ReleaseYear { get; set; }
}
