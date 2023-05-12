using StarWars.Model;
using Microsoft.AspNetCore.Mvc;
using StarWars.Service;

namespace StarWars.Controllers;


[Route("[controller]")]
[ApiController]
public class RoundController : GenericController<Round>
{

    private ServiceRound svRound;

    public RoundController(StarWarsDbContext context) : base(context)
    {
        svRound = new ServiceRound(context);
    }

    public override ActionResult<List<Round>> GetAll()
    {
        return svRound.GetAll();
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
        var round = svRound.GetInclude(id);
        if (round == null)
        {
            return BadRequest();
        }
        return round;
    }

    [HttpGet("page")]
    public ActionResult<List<Round>> GetPage(int lastId, int pageSize)
    {
        var rounds = svRound.GetPage(lastId, pageSize);
        if (rounds == null)
        {
            return BadRequest();
        }

        return rounds;
    }

}
