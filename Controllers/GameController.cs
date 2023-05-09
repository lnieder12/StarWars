using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using StarWars.Model;

namespace StarWars.Controllers;

[Route("[controller]")]
[ApiController]
public class 
    GameController : GenericController<Game>
{

    private readonly ServiceGame _svGame;

    public GameController(StarWarsDbContext context) : base(context)
    {
        _svGame = new ServiceGame(context);
    }



    [HttpPost("{rebels}/{empires}")]
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
    public ActionResult<Game> CreateGameSelectedSoldiers(Rebels_Empires soldiers, int nbRound)
    {
        var game = _svGame.CreateSelectedGame(soldiers.Rebels, soldiers.Empires, nbRound);
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

    [HttpPost("{id}/" + nameof(Round) + "/{roundId}")]
    public ActionResult<Round> AddRound(int id, int roundId)
    {
        var round = _svGame.AddRound(id, roundId);
        if (round == null)
        {
            return BadRequest();
        }

        return round;
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

    [HttpGet("{id}/soldier")]
    public ActionResult<List<Soldier>> GetSoldiers(int id)
    {
        var soldiers = _svGame.GetSoldiers(id);
        if (soldiers == null)
        {
            return BadRequest();
        }

        return soldiers;
    }

    [HttpGet("{id}/rebel")]
    public ActionResult<List<Rebel>> GetRebels(int id)
    {
        var rebels = _svGame.GetRebels(id);
        if (rebels == null)
        {
            return BadRequest();
        }

        return rebels;
    }


    [HttpGet("{id}/empire")]
    public ActionResult<List<Empire>> GetEmpires(int id)
    {
        var empires = _svGame.GetEmpires(id);
        if (empires == null)
        {
            return BadRequest();
        }

        return empires;
    }

    [HttpGet("{id}/soldier/random")]
    public ActionResult<Soldier> GetRandomSoldier(int id)
    {
        var soldier = _svGame.GetRandomSoldier(id);
        if (soldier == null)
        {
            return BadRequest();
        }

        return soldier;
    }

    [HttpGet("{id}/fight")]
    public ActionResult<Round> Fight(int id)
    {
        var round = _svGame.Fight(id);
        if (round == null)
        {
            return null;
        }

        return round;
    }


    [HttpGet("{id}/score")]
    public ActionResult<List<SoldierScore>> GetSoldierScores(int id)
    {
        return _svGame.GetSoldierScores(id);
    }

    [HttpGet("{id}/score/page")]
    public ActionResult<List<SoldierScore>> GetSoldierScorePage(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _svGame.GetSoldierScoresPage(id, queries);
    }

    [HttpGet("{id}/score/count")]
    public ActionResult<int> GetScoreCount(int id)
    {
        var queries = HttpContext.Request.Query.ToDictionary(k => k.Key, v => v.Value);
        return _svGame.GetScoreFilteredCount(id, queries);
    }
    

    [HttpGet("{id}/round/nb")]
    public ActionResult<int> GetNbRounds(int id)
    {
        return _svGame.NbRounds(id);
    }

    [HttpGet("{id}/multipleFight")]
    public ActionResult<List<Round>> MultipleFight(int id, int nb)
    {
        return _svGame.MultipleFights(id, nb);
    }

    [HttpGet("{id}/rebel/valide")]
    public ActionResult<int> GetNbValideRebels(int id)
    {
        return _svGame.NbValideRebels(id);
    }

    [HttpGet("{id}/empire/valide")]
    public ActionResult<int> GetNbValideEmpires(int id)
    {
        return _svGame.NbValideEmpires(id);
    }

    [HttpGet("{id}/enoughSoldiers")]
    public ActionResult<bool> EnoughSoldiers(int id)
    {
        return _svGame.EnoughSoldiers(id);
    }

    [HttpGet("{id}/winnerTeam")]
    public ActionResult<string> GetWinnerTeam(int id)
    {
        return _svGame.WinnerTeam(id);
    }


}