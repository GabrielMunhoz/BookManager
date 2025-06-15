using System.Text.Json.Serialization;

namespace BookManager.Domain.Model.Books;
public  class BookUpdate
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public int ReleaseYear { get; set; }
    public decimal Value { get; set; }
    [JsonIgnore]
    public DateTime? UpdateDate { get; set; } = DateTime.Now;
}
