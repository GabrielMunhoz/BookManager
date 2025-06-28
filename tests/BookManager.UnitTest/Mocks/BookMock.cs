using Bogus;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Books;

namespace BookManager.UnitTest.Mocks;
internal static class BookMock
{
    public static Book GetMock()
    {
        return new Faker<Book>()
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
            .RuleFor(b => b.Autor, f => f.Person.FullName)
            .RuleFor(b => b.ISBN, f => f.Random.Replace("###-##########"))
            .RuleFor(b => b.ReleaseYear, f => f.Date.Past(30).Year)
            .RuleFor(b => b.Value, f => f.Finance.Amount(10, 200))
            .Generate();
    }

    public static List<Book> GetMockList(int count = 3)
    {
        return new Faker<Book>()
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
            .RuleFor(b => b.Autor, f => f.Person.FullName)
            .RuleFor(b => b.ISBN, f => f.Random.Replace("###-##########"))
            .RuleFor(b => b.ReleaseYear, f => f.Date.Past(30).Year)
            .RuleFor(b => b.Value, f => f.Finance.Amount(10, 200))
            .Generate(count);
    }

    public static BookCreate BuildBookCreate() => new Faker<BookCreate>()
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
            .RuleFor(b => b.Autor, f => f.Person.FullName)
            .RuleFor(b => b.ISBN, f => f.Random.Replace("###-##########"))
            .RuleFor(b => b.ReleaseYear, f => f.Date.Past(30).Year)
            .RuleFor(b => b.Value, f => f.Finance.Amount(10, 200))
            .Generate();
    public static BookUpdate BuildBookUpdate() => new Faker<BookUpdate>()
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
            .RuleFor(b => b.Autor, f => f.Person.FullName)
            .RuleFor(b => b.ISBN, f => f.Random.Replace("###-##########"))
            .RuleFor(b => b.ReleaseYear, f => f.Date.Past(30).Year)
            .RuleFor(b => b.Value, f => f.Finance.Amount(10, 200))
            .Generate();
    
}
