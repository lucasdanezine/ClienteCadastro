using ClienteCadastro.Application.Interfaces;
using ClienteCadastro.Infrastructure.Data;
using ClienteCadastro.Infrastructure.Repositories;
using ClienteCadastro.Infrastructure.Services;
using ClienteCadastro.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Refit;
using ClienteCadastro.Domain.Interfaces;
using ClienteCadastro.Infrastructure.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Configurações de banco de dados PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Refit - Cliente HTTP para ViaCEP
builder.Services
    .AddRefitClient<IViaCepApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br"));

// Injeção de dependência
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IViaCepService, ViaCepService>();
builder.Services.AddScoped<IValidator<ClienteCadastro.Application.DTOs.CreateClienteDto>, CreateClienteValidator>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.AddHostedService<ClienteCriadoConsumer>();
// AutoMapper (caso queira usar)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Controllers e validações com FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateClienteValidator>();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware e pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
