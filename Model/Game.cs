namespace StarWars.Model;

public class Game
{
    public int Id { get; set; }

    //[DeleteBehavior(DeleteBehavior.Cascade)]
    public List<GameSoldier> Soldiers { get; set; } = new List<GameSoldier>();

    public List<Round> Rounds { get; set; } = new List<Round>();

    public int MaxRound { get; set; }

}