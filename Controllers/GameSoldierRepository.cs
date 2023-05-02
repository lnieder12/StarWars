using Microsoft.EntityFrameworkCore;
using StarWars.Model;

namespace StarWars.Controllers;

public class GameSoldierRepository : Repository<GameSoldier>
{
    public GameSoldierRepository(StarWarsDbContext ctx) : base(ctx)
    {
    }

    public GameSoldier Get(int gameId, int soldierId)
    {
        return ctx.Set<GameSoldier>()
            .FirstOrDefault(gs => gs.GameId == gameId && gs.SoldierId == soldierId);
    }

    public GameSoldier GetInclude(int gameId, int soldierId)
    {
        return ctx.Set<GameSoldier>().Include(gs => gs.Game)
            .Include(gs => gs.Soldier)
            .FirstOrDefault(gs => gs.GameId == gameId && gs.SoldierId == soldierId);
    }

}