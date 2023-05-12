using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StarWars.Model;
using StarWars.Service;

namespace StarWars.Controllers;
    
[Route("[controller]")]
[ApiController]
public class GameController : GenericController<Game>
{

    private readonly ServiceGame _svGame;

    public GameController(StarWarsDbContext context) : base(context)
    {
        _svGame = new ServiceGame(context);
    }



    [HttpPost("{rebels:int}/{empires:int}")]
    public ActionResult<Game> CreateGame(int rebels, int empires, int nbRound)
    {
        var game = _svGame.CreateGame(rebels, empires, nbRound);
        if (game == null)
        {
            return BadRequest();
        }

        return game;
    }

    [HttpPost("selectedSoldiers")]
    public ActionResult<Game> CreateGameSelectedSoldiers(RebelsEmpires soldiers, int nbRound)
    {
        var game = _svGame.CreateSelectedGame(soldiers, nbRound);
        if (game == null)
        {
            return BadRequest();
        }
        return game;
    }


    [HttpPost("{id}/" + nameof(Soldier) + "/{pId}")]
    public ActionResult<Soldier> AddSoldier(int id, int pId)
    {
        var soldier = _svGame.AddSoldier(id, pId);
        if (soldier == null)
        {
            return BadRequest();
        }

        return soldier;
    }

    [HttpPatch("{id}/" + nameof(Soldier) + "/{soldierId}")]
    public ActionResult<Soldier> PatchSoldier(int id, int soldierId, JsonPatchDocument<Soldier> patch)
    {
        var rebel = _svGame.PatchSoldier(id, soldierId, patch);
        if (rebel == null)
        {
            return BadRequest();
        }

        return rebel;
    }

    [HttpGet("{id}/round")]
    public ActionResult<List<Round>> GetRounds(int id)
    {
        var rounds = _svGame.GetRounds(id);
        if (rounds == null)
        {
            return BadRequest();
        }

        return rounds;
    }

    [HttpGet("{id:int}/soldier")]
    public ActionResult<List<Soldier>> GetSoldiers(int id)
    {
        var soldiers = _svGame.GetSoldiers(id);
        if (soldiers == null)
        {
            return BadRequest();
        }

        return soldiers;
    }

    [HttpGet("{id:int}/rebel")]
    public ActionResult<List<Rebel>> GetRebels(int id)
    {
        var rebels = _svGame.GetRebels(id);
        if (rebels == null)
        {
            return BadRequest();
        }

        return rebels;
    }


    [HttpGet("{id:int}/empire")]
    public ActionResult<List<Empire>> GetEmpires(int id)
    {
        var empires = _svGame.GetEmpires(id);
        if (empires == null)
        {
            return BadRequest();
        }

        return empires;
    }

    [HttpGet("{id:int}/fight")]
    public ActionResult<Round> Fight(int id)
    {
        var round = _svGame.Fight(id);
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
        return _svGame.GetSoldierScoresPage(id, queries);
    }

    [HttpGet("{id:int}/round/page")]
    public ActionResult<List<Round>> GetRoundsPage(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _svGame.GetRoundsPage(id, queries);
    }

    [HttpGet("{id:int}/score/page/count")]
    public ActionResult<int> GetScoreCount(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _svGame.GetScoresFilteredCount(id, queries);
    }

    [HttpGet("{id}/round/page/count")]
    public ActionResult<int> GetRoundsCount(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _svGame.GetRoundsFilteredCount(id, queries);
    }


    [HttpGet("{id:int}/round/nb")]
    public ActionResult<int> GetNbRounds(int id)
    {
        return _svGame.NbRounds(id);
    }

    [HttpGet("{id:int}/multipleFight")]
    public ActionResult<List<Round>> MultipleFight(int id, int nb)
    {
        return _svGame.MultipleFights(id, nb);
    }

    [HttpGet("{id:int}/rebel/Valid")]
    public ActionResult<int> GetNbValidRebels(int id)
    {
        return _svGame.GetNbValidSoldier<Rebel>(id);
    }

    [HttpGet("{id:int}/empire/valid")]
    public ActionResult<int> GetNbValidEmpires(int id)
    {
        return _svGame.GetNbValidSoldier<Empire>(id);
    }

    [HttpGet("{id:int}/enoughSoldiers")]
    public ActionResult<bool> EnoughSoldiers(int id)
    {
        return _svGame.EnoughSoldiers(id);
    }

    [HttpGet("{id:int}/winnerTeam")]
    public ActionResult<string> GetWinnerTeam(int id)
    {
        return _svGame.WinnerTeam(id);
    }


}