using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StarWars.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(o =>
{
    o.AddPolicy(MyAllowSpecificOrigins, policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContextPool<StarWarsDbContext>(opt =>
{
    const string cs = "Server=localhost;Port=3306;Database=StarWars;Uid=root;Pwd=root";
    opt.UseMySql(cs, ServerVersion.AutoDetect(cs));
});
builder.Services.AddMvc().AddNewtonsoftJson();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
