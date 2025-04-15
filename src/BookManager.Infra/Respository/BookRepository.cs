using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Infra.Data;
using BookManager.Infra.Respository.Base;
using Microsoft.Extensions.Logging;

namespace BookManager.Infra.Respository;

public class BookRepository(BookManagerDbContext context, ILogger<BaseRepository<Book>> logger) : 
    BaseRepository<Book>(context, logger), IBookRepository
{

}
