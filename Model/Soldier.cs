using System.ComponentModel.DataAnnotations;

namespace StarWars.Model;

public class Soldier
{
    public int Id { get; set; }
    [Range(1000, 2000)]
    public int MaxHealth { get; set; }

    [Range(100, 500)]
    public int Attack { get; set; }

    public string SoldierType { get; set; }
}