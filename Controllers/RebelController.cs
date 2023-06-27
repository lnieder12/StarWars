using StarWars.Model;
using Microsoft.AspNetCore.Mvc;
using StarWars.Service;

namespace StarWars.Controllers;

[Route("[controller]")]
[ApiController]
public class RebelController : GenericController<Rebel>
{
    public RebelController(StarWarsDbContext context, IService<Rebel> service) : base(context, service)
    {
    }
}