using Microsoft.EntityFrameworkCore;
using StarWars.Model;
using StarWars.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(o =>
{
    o.AddPolicy(MyAllowSpecificOrigins, policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200", "http://localhost:4202")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddStarWarsServices();


builder.Services.AddControllers()
    .AddNewtonsoftJson(
        options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
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