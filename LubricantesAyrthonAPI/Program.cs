using LubricantesAyrthonAPI.Configuration;
using Microsoft.EntityFrameworkCore;
using System;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


// Configurar la cadena de conexión a PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios personalizados
builder.Services.AddScoped<LubricantesAyrthonAPI.Services.Interfaces.ICustomerService, LubricantesAyrthonAPI.Services.Implementations.CustomerService>();
builder.Services.AddScoped<LubricantesAyrthonAPI.Services.Interfaces.ISellerService, LubricantesAyrthonAPI.Services.Implementations.SellerService>();
builder.Services.AddScoped<LubricantesAyrthonAPI.Services.Interfaces.ISaleService, LubricantesAyrthonAPI.Services.Implementations.SaleService>();
builder.Services.AddScoped<LubricantesAyrthonAPI.Services.Interfaces.IProductService, LubricantesAyrthonAPI.Services.Implementations.ProductService>();

// Registrar repositorios personalizados
builder.Services.AddScoped<LubricantesAyrthonAPI.Repositories.Interfaces.IBaseRepository<LubricantesAyrthonAPI.Models.Customer>, LubricantesAyrthonAPI.Repositories.Implementations.CustomerRepository>();
builder.Services.AddScoped<LubricantesAyrthonAPI.Repositories.Interfaces.IBaseRepository<LubricantesAyrthonAPI.Models.Seller>, LubricantesAyrthonAPI.Repositories.Implementations.SellerRepository>();
builder.Services.AddScoped<LubricantesAyrthonAPI.Repositories.Interfaces.IBaseRepository<LubricantesAyrthonAPI.Models.Sale>, LubricantesAyrthonAPI.Repositories.Implementations.SaleRepository>();
builder.Services.AddScoped<LubricantesAyrthonAPI.Repositories.Interfaces.IBaseRepository<LubricantesAyrthonAPI.Models.Product>, LubricantesAyrthonAPI.Repositories.Implementations.ProductRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
