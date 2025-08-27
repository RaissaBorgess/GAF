using GAF.Api.Data;
using Microsoft.EntityFrameworkCore;
using GAF.Api.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Serviço de Conexão com Banco de Dados
builder.Services.AddDbContext<AppDbContext>( options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Conexao"))
);

//Serviço de Identidade (Identity-usuarios)
builder.Services.AddIdentity<User, IdentityRole>( options =>
{
    //Configuração de Senha
    options.Password.RequireLength = 6;
    options.Password.RequireUniqueChars = 6;

    // Configurações de Bloqueio
    options.Lockout.MaxFailedAcessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    
    // Configurações de Usuário
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Serviço de configuração do Jwt
var jwtSetiings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services>AddAuthentication(options =>
{

})

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(char =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GAF API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
