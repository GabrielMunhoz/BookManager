using AutoMapper;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.User;

namespace BookManager.Business.Mapper;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Users, UserResponseList>();
        CreateMap<Users, UsersList>().ReverseMap();
        CreateMap<Users, UsersDetail>().ReverseMap();
        CreateMap<UsersCreate, Users>().ReverseMap();
        CreateMap<UsersUpdate, Users>().ReverseMap();
    }
}
