using StarWars.Model;
using Microsoft.AspNetCore.Mvc;

namespace StarWars.Controllers;

[Route("[controller]")]
[ApiController]
public class RebelController : GenericController<Rebel>
{
    public RebelController(StarWarsDbContext context) : base(context)
    {
    }
}