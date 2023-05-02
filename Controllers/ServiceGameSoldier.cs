using StarWars.Model;

namespace StarWars.Controllers;

public class ServiceGameSoldier : Service<GameSoldier>
{

    private GameSoldierRepository gsRepo;

    public ServiceGameSoldier(StarWarsDbContext context) : base(context)
    {
        this.gsRepo = new GameSoldierRepository(context);
    }

    public void SetHealth(int hp, int gameId, int soldierId)
    {
    }

    public int SoldierHealth(int gameId, int soldierId)
    {
        return Get(gameId, soldierId).Health;
    }

    public GameSoldier Get(int gameId, int soldierId)
    {
        return this.gsRepo.Get(gameId, soldierId);
    }


}