using StarWars.Model;
using Microsoft.AspNetCore.Mvc;
using StarWars.Service;

namespace StarWars.Controllers;


[Route("[controller]")]
[ApiController]
public class RoundController : GenericController<Round>
{

    private readonly ServiceRound _svRound;

    public RoundController(StarWarsDbContext context) : base(context)
    {
        _svRound = new ServiceRound(context);
    }

    public override ActionResult<List<Round>> GetAll()
    {
        return _svRound.GetAll();
    }

    // [HttpPost("{att:int}/{def:int}")]
    // public ActionResult<Round> AddRound(int att, int def)
    // {
    //     var round = svRound.AddRound(att, def);
    //     if(round == null)
    //     {
    //         return BadRequest();
    //     }
    //     return round;
// }

    public override ActionResult<Round> Get(int id)
    {
        var round = _svRound.GetInclude(id);
        if (round == null)
        {
            return BadRequest();
        }
        return round;
    }

}
