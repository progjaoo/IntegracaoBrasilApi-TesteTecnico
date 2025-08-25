using IntegracaoWebApi.Application.Services;
using IntegracaoWebApi.Core.Interfaces;
using IntegracaoWebApi.Infrastructure.Data;
using IntegracaoWebApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IntegracaoWebApi.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "João Marcos Valente",
            Email = "joaomvprog@gmail.com"
        }
    });

    var xmlFile = "IntegracaoWebApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IBancoRepository, BancoRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IBancoService, BancoService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();

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
