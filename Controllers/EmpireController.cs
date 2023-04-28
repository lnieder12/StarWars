using StarWars.Model;
using Microsoft.AspNetCore.Mvc;

namespace StarWars.Controllers;

[Route("[controller]")]
[ApiController]
public class EmpireController : GenericController<Empire>
{
    public EmpireController(StarWarsDbContext context) : base(context)
    {
    }
}
