﻿using BookManager.Domain.Entity;
using BookManager.Domain.Interface.Repositories;
using BookManager.Infra.Data;
using BookManager.Infra.Respository.Base;

namespace BookManager.Infra.Respository;

public class BookRepository(BookManagerDbContext context) : BaseRepository<Book>(context), IBookRepository
{

}
