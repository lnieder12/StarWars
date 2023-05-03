using Microsoft.EntityFrameworkCore;
using StarWars.Model;

namespace StarWars.Controllers;

public class GameRepository : Repository<Game>
{
    public GameRepository(StarWarsDbContext ctx) : base(ctx)
    {
    }
    public Game GetIncludeRounds(int id)
    {
        return ctx.Set<Game>().Include(g => g.Rounds)
            .Include(g => g.Rounds).ThenInclude(r => r.Defender)
            .Include(g => g.Rounds).ThenInclude(r => r.Attacker)
            .FirstOrDefault(g => g.Id == id);
    }

    public Game GetIncludeSoldiers(int id)
    {
        return ctx.Set<Game>().Include(g => g.Soldiers).ThenInclude(gs => gs.Soldier).FirstOrDefault(g => g.Id == id);
    }
    public new List<Game> GetAll()
    {
        return ctx.Set<Game>().Include(g => g.Soldiers).ToList();
    }

}