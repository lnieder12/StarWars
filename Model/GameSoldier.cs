namespace StarWars.Model;

public class GameSoldier
{
    public int GameId { get; set; }
    public virtual Game Game{ get; set; }

    public int SoldierId { get; set; }

    public virtual Soldier Soldier { get; set; }

    public int Health { get; set; }
}