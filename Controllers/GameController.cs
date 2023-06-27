using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StarWars.Model;
using StarWars.Service;

namespace StarWars.Controllers;
    
[Route("[controller]")]
[ApiController]
public class GameController : GenericController<Game>
{

    private readonly IGameService _sv;


    public GameController(StarWarsDbContext context, IService<Game> service, IGameService sv) : base(context, service)
    {
        _sv = sv;
    }

    [HttpPost("{rebels:int}/{empires:int}")]
    public ActionResult<Game> CreateGame(int rebels, int empires, int nbRound)
    {
        var game = _sv.CreateGame(rebels, empires, nbRound);
        if (game == null)
        {
            return BadRequest();
        }

        return game;
    }

    [HttpPost("selectedSoldiers")]
    public ActionResult<Game> CreateGameSelectedSoldiers(RebelsEmpires soldiers, int nbRound)
    {
        var game = _sv.CreateSelectedGame(soldiers, nbRound);
        if (game == null)
        {
            return BadRequest();
        }
        return game;
    }


    [HttpPost("{id}/" + nameof(Soldier) + "/{pId}")]
    public ActionResult<Soldier> AddSoldier(int id, int pId)
    {
        var soldier = _sv.AddSoldier(id, pId);
        if (soldier == null)
        {
            return BadRequest();
        }

        return soldier;
    }

    [HttpPatch("{id}/" + nameof(Soldier) + "/{soldierId}")]
    public ActionResult<Soldier> PatchSoldier(int id, int soldierId, JsonPatchDocument<Soldier> patch)
    {
        var rebel = _sv.PatchSoldier(id, soldierId, patch);
        if (rebel == null)
        {
            return BadRequest();
        }

        return rebel;
    }

    [HttpGet("{id}/round")]
    public ActionResult<List<Round>> GetRounds(int id)
    {
        var rounds = _sv.GetRounds(id);
        if (rounds == null)
        {
            return BadRequest();
        }

        return rounds;
    }

    [HttpGet("{id:int}/soldier")]
    public ActionResult<List<Soldier>> GetSoldiers(int id)
    {
        var soldiers = _sv.GetSoldiers(id);
        if (soldiers == null)
        {
            return BadRequest();
        }

        return soldiers;
    }

    [HttpGet("{id:int}/rebel")]
    public ActionResult<List<Rebel>> GetRebels(int id)
    {
        var rebels = _sv.GetRebels(id);
        if (rebels == null)
        {
            return BadRequest();
        }

        return rebels;
    }


    [HttpGet("{id:int}/empire")]
    public ActionResult<List<Empire>> GetEmpires(int id)
    {
        var empires = _sv.GetEmpires(id);
        if (empires == null)
        {
            return BadRequest();
        }

        return empires;
    }

    [HttpGet("{id:int}/fight")]
    public ActionResult<Round> Fight(int id)
    {
        var round = _sv.Fight(id);
        if (round == null)
        {
            return null;
        }

        return round;
    }

    [HttpGet("{id:int}/score/page")]
    public ActionResult<List<SoldierScore>> GetSoldierScorePage(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _sv.GetSoldierScoresPage(id, queries);
    }

    [HttpGet("{id:int}/round/page")]
    public ActionResult<List<Round>> GetRoundsPage(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _sv.GetRoundsPage(id, queries);
    }

    [HttpGet("{id:int}/score/page/count")]
    public ActionResult<int> GetScoreCount(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _sv.GetScoresFilteredCount(id, queries);
    }

    [HttpGet("{id}/round/page/count")]
    public ActionResult<int> GetRoundsCount(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _sv.GetRoundsFilteredCount(id, queries);
    }


    [HttpGet("{id:int}/round/nb")]
    public ActionResult<int> GetNbRounds(int id)
    {
        return _sv.NbRounds(id);
    }

    [HttpGet("{id:int}/multipleFight")]
    public ActionResult<List<Round>> MultipleFight(int id, int nb)
    {
        return _sv.MultipleFights(id, nb);
    }

    [HttpGet("{id:int}/rebel/Valid")]
    public ActionResult<int> GetNbValidRebels(int id)
    {
        return _sv.GetNbValidSoldier<Rebel>(id);
    }

    [HttpGet("{id:int}/empire/valid")]
    public ActionResult<int> GetNbValidEmpires(int id)
    {
        return _sv.GetNbValidSoldier<Empire>(id);
    }

    [HttpGet("{id:int}/enoughSoldiers")]
    public ActionResult<bool> EnoughSoldiers(int id)
    {
        return _sv.EnoughSoldiers(id);
    }

    [HttpGet("{id:int}/winnerTeam")]
    public ActionResult<string> GetWinnerTeam(int id)
    {
        return _sv.WinnerTeam(id);
    }


}