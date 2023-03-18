using GeekBurger.Products.Contract.MapperProfiles;
using GeekBurger.Products.Contract.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDbContext>
      (o => o.UseInMemoryDatabase("geekburger-products")) ;

builder.Services
  .AddScoped<IProductsRepository, ProductsRepository>();

builder.Services.AddAutoMapper(c => c.AddProfile(typeof(AutomapperProfile)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();