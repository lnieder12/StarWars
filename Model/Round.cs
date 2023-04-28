using System.Diagnostics.CodeAnalysis;

namespace StarWars.Model;

[Serializable()]
public class Round
{
    public int Id { get; set; }

    [AllowNull]
    public Soldier Attacker { get; set; }

    [AllowNull]
    public Soldier Defender { get; set; }

    public int Damage => Attacker != null ? Attacker.Attack : 0;

    public bool IsDead { get; set; }

    public int HpLeft { get; set; }

}