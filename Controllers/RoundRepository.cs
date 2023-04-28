using Microsoft.EntityFrameworkCore;
using StarWars.Model;

namespace StarWars.Controllers;

public class RoundRepository : Repository<Round>
{
    public RoundRepository(StarWarsDbContext ctx) : base(ctx)
    {
    }

    public Round GetInclude(int id)
    {
        return ctx.Set<Round>().Include(r => r.Attacker)
            .Include(r => r.Defender)
            .FirstOrDefault(r => r.Id == id);
    }

    public List<Round> GetAll()
    {
        return ctx.Rounds.Include(r => r.Attacker).Include(r => r.Defender).ToList();
    }

}