namespace StarWars.Model;

public class Game
{
    public int Id { get; set; }

    //[DeleteBehavior(DeleteBehavior.Cascade)]
    public List<Soldier> Soldiers { get; set; } = new List<Soldier>();

    //[DeleteBehavior(DeleteBehavior.Cascade)]
    public List<Empire> Empires => Soldiers.Where(x => x.GetType() == typeof(Empire)).Cast<Empire>().ToList();

    //[DeleteBehavior(DeleteBehavior.Cascade)]
    public List<Rebel> Rebels => Soldiers.Where(x => x.GetType() == typeof(Rebel)).Cast<Rebel>().ToList();

    //[DeleteBehavior(DeleteBehavior.Cascade)]
    public List<Round> Rounds { get; set; } = new List<Round>();

    public int MaxRound { get; set; }

}