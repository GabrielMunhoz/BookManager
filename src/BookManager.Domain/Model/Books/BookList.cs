namespace BookManager.Domain.Model.Books;
public  class BookList
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public int ReleaseYear { get; set; }
    public decimal Value { get; set; }
    public int Stock { get; set; }

}
