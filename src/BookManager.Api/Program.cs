using BookManager.Domain.Extensions;
using BookManager.Infra.Data;
using BookManager.IoC.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AutoMapperConfig();

builder.Services.AddDbContext<BookManagerDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration
                .GetSection("ConnectionStrings")[ConstantsConf.BookConnection])
                .EnableSensitiveDataLogging());

SerilogExtension.LogsConfig(builder);

builder.Services.AddHealthChecksConfig(builder.Configuration);

builder.Services.ConfigServices();
builder.Services.ConfigRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHealthChecksConfig();

app.UseAuthorization();

app.MapControllers();

app.Run();
