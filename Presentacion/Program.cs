using CapaDatos.Context;
using CapaDatos.Repositorios.Interfaces;
using CapaDatos.Repositorios.Repositories;
using CapaEntidades.Models;
using CapaNegocio.Interfaces;
using CapaNegocio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("AppConnection"));


builder.Services.AddScoped(typeof(IRepositorio<>),typeof(Repository<>));
builder.Services.AddScoped<ICategoriaService,CategoriaService>();
builder.Services.AddScoped<IGastoService, GastoService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPasswordHashing, HashService>();
builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
