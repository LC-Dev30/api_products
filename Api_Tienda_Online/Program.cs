using Api_Tienda_Online.Services.Security;
using Api_Tienda_Online.Validation;
using Capa_Repositorio.dbContext;
using Capa_Repositorio.RepositorioProductos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(set =>
{
    set.AddDefaultPolicy(opt =>
    {
        opt.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

//config jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    var opciones = option.TokenValidationParameters;

    opciones.ValidateAudience = true;
    opciones.ValidateIssuer = true;
    opciones.ValidateLifetime = true;
    opciones.ValidateIssuerSigningKey = true;
    opciones.ValidIssuer = builder.Configuration["Jwt:Issuer"];
    opciones.ValidAudience = builder.Configuration["Jwt:Audience"];
    opciones.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TiendaOnlineContext>(db =>
{
    db.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});

//instacia de Productos
builder.Services.AddScoped<IToken, CreateTokenClient>();

builder.Services.AddScoped<IProductosRepositorio, RepositorioProducto>();

//instacia para validar objectos de products
builder.Services.AddScoped<IProductValidation,ProductValidation>();

//instacia para validar objectos de client
builder.Services.AddScoped<IValidationClient,ClientValidation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
