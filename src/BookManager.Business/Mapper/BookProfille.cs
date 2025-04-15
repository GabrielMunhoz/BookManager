using AutoMapper;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.User;

namespace BookManager.Business.Mapper;
public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Users, UserResponseList>();
    }
}
