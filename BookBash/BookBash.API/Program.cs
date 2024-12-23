using Microsoft.EntityFrameworkCore;
using BookBash.API.Data;
using BookBash.API.Repository;
using BookBash.API.Service;
using Bookbash.API.Service;
using PetTracker.API.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookBashContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("BOOKBASH")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependency Inject Proper Services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();


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

