using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StarWars.Model;

namespace StarWars.Controllers;

public class ServiceEmpire : Service<Empire>
{

    public ServiceEmpire(StarWarsDbContext context) : base(context)
    {
    }

    public Empire CreateRandom(int number)
    {

        var empire = new Empire();

        var random = new Random();

        empire.Attack = random.Next(100, 500);
        var rnd = random.Next(1000, 2000);
        empire.MaxHealth = rnd;

        empire.Name = "EMP-" + number;


        return this.Add(empire);
    }

}