using Microsoft.EntityFrameworkCore;
using BookBash.API.Data;
using BookBash.API.Repository;
using BookBash.API.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookBashContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("BOOKBASH")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependency Inject Proper Services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorBookService, AuthorBookService>();
builder.Services.AddScoped<IBookListService, BookListService>();
builder.Services.AddScoped<IBookBookListService, BookBookListService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorBookRepository, AuthorBookRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookListRepository, BookListRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookBookListRepository, BookBookListRepository>();


//Add Controllers
builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

