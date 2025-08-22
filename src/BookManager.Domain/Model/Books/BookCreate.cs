namespace BookManager.Domain.Model.Books;
public  class BookCreate
{
    public string Title { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public int ReleaseYear { get; set; }
    public decimal Value { get; set; }
}
