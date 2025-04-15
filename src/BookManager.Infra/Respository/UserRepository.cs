using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Infra.Data;
using BookManager.Infra.Respository.Base;
using Microsoft.Extensions.Logging;

namespace BookManager.Infra.Respository;
public class UserRepository(BookManagerDbContext context, ILogger<BaseRepository<Users>> logger) : 
    BaseRepository<Users>(context, logger), IUserRepository
{
}
