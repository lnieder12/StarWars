using Microsoft.AspNetCore.Mvc;
using StarWars.Model;

namespace StarWars.Controllers;

public class GameSoldierController : GenericController<GameSoldier>
{


    public GameSoldierController(StarWarsDbContext context) : base(context)
    {
    }
}