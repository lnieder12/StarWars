namespace StarWars.Model;

public class GameSoldier
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }

    public int SoldierId { get; set; }

    public Soldier Soldier { get; set; }

    public int Health { get; set; }
}