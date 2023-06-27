using Microsoft.AspNetCore.Mvc;
using StarWars.Model;
using StarWars.Service;

namespace StarWars.Controllers;

[Route("[controller]")]
[ApiController]
public class SoldierController : GenericController<Soldier>
{
    public SoldierController(StarWarsDbContext context, IService<Soldier> service) : base(context, service)
    {
    }
}
