using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StarWars.Model;

namespace StarWars.Controllers;

public class ServiceRound : Service<Round>
{
    private StarWarsDbContext context;

    private RoundRepository roundRepo;

    private ServiceGameSoldier gsSrv;

    public ServiceRound(StarWarsDbContext context) : base(context)
    {
        this.context = context;
        this.roundRepo = new RoundRepository(context);
        gsSrv = new ServiceGameSoldier(context);
    }

    public override List<Round> GetAll()
    {
        return roundRepo.GetAll();
    }

    public Round AddRound(int idAtt, int idDef)
    {
        var svSoldier = new Service<Soldier>(context);

        var soldAtt = svSoldier.Get(idAtt);

        var soldDef = svSoldier.Get(idDef);

        if(soldAtt == null || soldDef == null || soldAtt.GetType() == soldDef.GetType())
        {
            return null;
        }

        var round = new Round
        {
            Attacker = soldAtt,
            Defender = soldDef,
        };

        

        Add(round);

        context.SaveChanges();

        return round;
    }

    public Round GetInclude(int id)
    {
        return roundRepo.GetInclude(id);
    }

}
