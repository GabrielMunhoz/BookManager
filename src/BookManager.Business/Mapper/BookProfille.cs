using AutoMapper;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Books;

namespace BookManager.Business.Mapper;
public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookResponseList>().ReverseMap();
        CreateMap<Book, BookList>().ReverseMap();
        CreateMap<Book, BookDetail>().ReverseMap();
        CreateMap<BookCreate, Book>().ReverseMap();
        CreateMap<BookUpdate, Book>().ReverseMap();
    }
}
