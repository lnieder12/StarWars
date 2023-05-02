using Microsoft.EntityFrameworkCore;

namespace StarWars.Model
{
    public class StarWarsDbContext : DbContext
    {
        public DbSet<Soldier> Soldiers { get; set;}

        public DbSet<Round> Rounds { get; set;}

        public DbSet<Game> Games { get; set;}

        public DbSet<GameSoldier> GameSoldiers { get; set;}


        public StarWarsDbContext(DbContextOptions<StarWarsDbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Soldier>()
                .HasDiscriminator<string>(s => s.SoldierType);

            modelBuilder.Entity<Rebel>()
                .HasDiscriminator<string>(r => r.SoldierType)
                .HasValue("Rebel");

            modelBuilder.Entity<Empire>()
                .HasDiscriminator<string>(e => e.SoldierType)
                .HasValue("Empire");

            modelBuilder.Entity<GameSoldier>()
                .HasKey(gs => new { gs.GameId, gs.SoldierId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
