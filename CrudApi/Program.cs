using Microsoft.EntityFrameworkCore;
using CrudApi.Properties.Context;
using CrudApi.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("http://localhost:5001")  // Cambia esto si tu frontend está en otro puerto
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql"))
);

builder.Services.AddScoped<PerfilServices>();
builder.Services.AddScoped<EmpleadoServices>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
