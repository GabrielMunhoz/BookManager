using AutoMapper;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Books;

namespace BookManager.Business.Mapper;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Book, BookResponseList>();
    }
}
