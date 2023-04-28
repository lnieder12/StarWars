using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StarWars.Model;

namespace StarWars.Controllers;

public class ServiceGame : Service<Game>
{
    private readonly StarWarsDbContext _context;

    private GameRepository gameRepo;

    public ServiceGame(StarWarsDbContext context) : base(context)
    {
        this._context = context;
        this.gameRepo = new GameRepository(context);
    }

    public Game CreateGame(int rebels, int empires, int nbRounds)
    {
        var srvRebel = new ServiceRebel(_context);

        var srvEmpire = new ServiceEmpire(_context);

        if (srvEmpire == null || srvRebel == null) throw new ArgumentNullException(nameof(srvEmpire));

        Game game;
        if (nbRounds > 0)
        {
            game = this.Add(new Game()
            {
                MaxRound = nbRounds,
            });
        }
        else
            game = this.Add(new Game());


        for (var i = 1; i <= rebels; i++)
        {
            this.AddSoldier(game.Id, srvRebel.CreateRandom(i).Id);
        }
        for (var y = 1; y <= empires; y++)
        {
            this.AddSoldier(game.Id, srvEmpire.CreateRandom(y).Id);
        }

        return game;
    }

    public Game CreateSelectedGame(List<Rebel> rebels, List<Empire> empires, int nbRounds)
    {
        Game game;
        if (nbRounds > 0)
        {
            game = this.Add(new Game()
            {
                MaxRound = nbRounds,
            });
        }
        else
            game = this.Add(new Game());

        rebels.ForEach(reb => this.AddSoldier(game.Id, reb.Id));
        empires.ForEach(emp => this.AddSoldier(game.Id, emp.Id));

        return game;
    }

    public override List<Game> GetAll()
    {
        return gameRepo.GetAll();
    }

    public Game GetIncludeSoldiers(int id)
    {
        return gameRepo.GetIncludeSoldiers(id);
    }
    public Game GetIncludeRounds(int id)
    {
        return gameRepo.GetIncludeRounds(id);
    }

    public Soldier GetSoldier(int id)
    {
        var srv = new Service<Soldier>(_context);

        return srv.Get(id);
    }

    public Soldier AddSoldier(int id, int soldierId)
    {
        if (SoldierInGame(id, soldierId))
            return null;

       

        var soldier = GetSoldier(soldierId);

        soldier.Health = soldier.MaxHealth;

        var game = GetIncludeSoldiers(id);

        if (game == null)
        {
            return null;
        }

        game.Soldiers.Add(soldier);

        _context.SaveChanges();

        return soldier;

    }

    public Soldier PatchSoldier(int id, int soldierId, JsonPatchDocument<Soldier> patch)
    {
        if (!SoldierInGame(id, soldierId))
            return null;

        var srv = new Service<Soldier>(_context);

        var soldier = srv.Get(soldierId);

        var nSoldier = srv.Patch(soldier.Id, patch);

        return nSoldier;
    }

    public Round AddRound(int id, int roundId)
    {
        var srv = new ServiceRound(_context);

        var round = srv.Get(roundId);

        var game = GetIncludeRounds(id);

        if (game == null)
            return null;

        game.Rounds.Add(round);

        _context.SaveChanges();

        return round;
    }


    public List<Round> GetRounds(int id)
    {
        return GetIncludeRounds(id)?.Rounds;
    }

    public int NbRounds(int id)
    {
        return GetRounds(id).Count;
    }

    public bool SoldierInGame(int id, int soldierId)
    {
        var game = GetIncludeSoldiers(id);
        return game?.Soldiers?.Any(s => s.Id == soldierId) ?? false;
    }

    public List<Soldier> GetSoldiers(int id)
    {
        return GetIncludeSoldiers(id)?.Soldiers;
    }

    public List<Rebel> GetRebels(int id)
    {
        return GetIncludeSoldiers(id)?.Rebels;
    }

    public List<Empire> GetEmpires(int id)
    {
        return GetIncludeSoldiers(id)?.Empires;
    }

    public List<Soldier> FilterValideSoldiers(List<Soldier> soldiers)
    {
        return soldiers.FindAll(s => s.Health > 0);
    }

    public Soldier GetRandomSoldier(int id)
    {
        var soldiers = FilterValideSoldiers(GetSoldiers(id));
        if (soldiers.IsNullOrEmpty())
            return null;
        Random random = new Random();

        return soldiers[random.Next(soldiers.Count())];
    }

    public Rebel GetRandomRebel(int id)
    {
        var rebels = GetValideRebels(id);
        if (rebels.IsNullOrEmpty())
            return null;
        Random random = new Random();
        return (Rebel)rebels[random.Next(rebels.Count())];
    }

    public Empire GetRandomEmpire(int id)
    {
        var empires = GetValideEmpires(id);
        if (empires.IsNullOrEmpty())
            return null;
        Random random = new Random();
        return (Empire)empires[random.Next(empires.Count())];
    }

    public List<Rebel> GetValideRebels(int id)
    {
        return FilterValideSoldiers(new List<Soldier>(GetRebels(id))).Cast<Rebel>().ToList();
    }

    public List<Empire> GetValideEmpires(int id)
    {
        return FilterValideSoldiers(new List<Soldier>(GetEmpires(id))).Cast<Empire>().ToList();
    }

    public int NbValideRebels(int id)
    {
        return GetValideRebels(id).Count;
    }

    public int NbValideEmpires(int id)
    {
        return GetValideEmpires(id).Count;
    }

    public Round Fight(int id)
    {
        var game = Get(id);

        var att = GetRandomSoldier(id);

        if(game == null || att == null || !SoldierInGame(id, att.Id))
        {
            return null;
        }

        var srv = new ServiceRound(_context);

        Soldier defender;

        if (att.GetType() == typeof(Empire))
        {
            defender = GetRandomRebel(id);
        }
        else
        {
            defender = GetRandomEmpire(id);
        }

        if (defender == null)
            return null;
        defender.Health -= att.Attack;
        if(defender.Health < 0)
            defender.Health = 0;

        var round = AddRound(id, srv.AddRound(att.Id, defender.Id).Id);

        return round;

    }

    public List<Round> GetSoldierRoundsAsAttacker(int id, int soldierId)
    {
        if (!SoldierInGame(id, soldierId))
            return null;
        return GetRounds(id).FindAll(r => r.Attacker.Id == soldierId);
    }

    public int SoldierTotalDamages(int id, int soldierId)
    {
        if(!SoldierInGame(id, soldierId))
            return 0;
        var rounds = GetSoldierRoundsAsAttacker(id, soldierId);
        int totalDamages = 0;
        rounds.ForEach(r => totalDamages += r.Damage);
        return totalDamages;
    }

    public int SoldierScore(int id, Soldier soldier)
    {
        var damages = SoldierTotalDamages(id, soldier.Id);
        return (damages + soldier.Health) * 10;
    }

    public SoldierScore GetSoldierScore(int id, Soldier sld)
    {
        var score = new SoldierScore
        {
            Soldier = sld,
            Score = this.SoldierScore(id, sld)
        };
        return score;
    }

    public List<SoldierScore> GetSoldierScores(int id)
    {
        var scores = new List<SoldierScore>();
        GetSoldiers(id).ForEach(soldier => scores.Add(this.GetSoldierScore(id, soldier)));
        return scores;
    }

    public int RebelScore(int id)
    {
        var score = 0;
        GetRebels(id).ForEach(rebel => score += SoldierScore(id, rebel));
        return score;
    }

    public int EmpireScore(int id)
    {
        var score = 0;
        GetEmpires(id).ForEach(empire => score += SoldierScore(id, empire));
        return score;
    }

    public List<Round> MultipleFights(int id, int nb)
    {

        List<Round> rounds = new List<Round>();

        Round round;

        if (nb > 0)
        {
            int i = 0;

            do
            {
                round = Fight(id);
                if(round != null)
                    rounds.Add(round);
                i++;
            }
            while (round != null && i < nb);
        }
        else
        {
            do
            {
                round = Fight(id);
                if (round != null)
                    rounds.Add(round);
            }       
            while (round != null) ;
        }
        return rounds;
    }

    public bool EnoughSoldiers(int id)
    {
        return NbValideRebels(id) > 0 && NbValideEmpires(id) > 0;
    }

    public string WinnerTeam(int id)
    {
        var winner = "";
        if (!EnoughSoldiers(id))
        {
            if (GetValideEmpires(id).Count > 0)
            {
                winner = "Empires won";
            }
            else
                winner = "Rebels won";
        }
        else
        {
            var empScore = EmpireScore(id);
            var rebScore = RebelScore(id);
            if (empScore > rebScore)
                winner = "Empires won with " + empScore + " points";
            else
                winner = "Rebels won with " + rebScore + " points";
        }
        return winner;
    }

}