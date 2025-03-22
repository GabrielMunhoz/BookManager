using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Infra.Data;
using BookManager.Infra.Respository.Base;
using Microsoft.Extensions.Logging;

namespace BookManager.Infra.Respository;
public class UserBookRepository(BookManagerDbContext context, ILogger<BaseRepository<UserBook>> logger) : 
    BaseRepository<UserBook>(context, logger), IUserBookRepository
{
}
