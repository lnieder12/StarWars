using StarWars.Model;
using Microsoft.AspNetCore.Mvc;
using StarWars.Service;

namespace StarWars.Controllers;

[Route("[controller]")]
[ApiController]
public class EmpireController : GenericController<Empire>
{
    public EmpireController(StarWarsDbContext context, IService<Empire> service) : base(context, service)
    {
    }
}