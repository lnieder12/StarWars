using Microsoft.AspNetCore.Mvc;
using StarWars.Model;
using StarWars.Service;

namespace StarWars.Controllers;

[Route("[controller]")]
[ApiController]
public class GameSoldierController : GenericController<GameSoldier>
{
    public GameSoldierController(StarWarsDbContext context, IService<GameSoldier> service) : base(context, service)
    {
    }
}