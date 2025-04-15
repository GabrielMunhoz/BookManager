using AutoMapper;
using BookManager.Domain.Entity;
using BookManager.Domain.Model.Loans;

namespace BookManager.Business.Mapper;
public class LoanProfile : Profile
{
    public LoanProfile()
    {
        CreateMap<Loan, LoanRequest>();
        CreateMap<LoanRequest, Loan>()
            .ForMember(l => l.Books, opt => 
                opt.MapFrom(s => 
                    s.Books.Select(b => new Book() { Id = b})))
            .ForMember(l => l.User, opt => 
                opt.MapFrom(
                        s => new Users() { Id = s.UserId }));
        CreateMap<Loan, LoanResponseList>();
    }
}
