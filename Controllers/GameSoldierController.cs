using Microsoft.AspNetCore.Mvc;
using StarWars.Model;

namespace StarWars.Controllers;

[Route("[controller]")]
[ApiController]
public class GameSoldierController : GenericController<GameSoldier>
{


    public GameSoldierController(StarWarsDbContext context) : base(context)
    {
    }
}