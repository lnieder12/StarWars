using StarWars.Model;
using Microsoft.AspNetCore.Mvc;
using StarWars.Service;

namespace StarWars.Controllers;


[Route("[controller]")]
[ApiController]
public class RoundController : GenericController<Round>
{

    private readonly IRoundService _sv;


    public RoundController(StarWarsDbContext context, IService<Round> service, IRoundService sv) : base(context, service)
    {
        _sv = sv;
    }

    public override ActionResult<List<Round>> GetAll()
    {
        return _sv.GetAll();
    }

    public override ActionResult<Round> Get(int id)
    {
        var round = _sv.GetInclude(id);
        if (round == null)
        {
            return BadRequest();
        }
        return round;
    }

}
