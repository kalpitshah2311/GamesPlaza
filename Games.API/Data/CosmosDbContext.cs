using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace Games.API.Data
{
    public class CosmosDbContext : DbContext
    {
        public CosmosDbContext(DbContextOptions options) : base(options) { }

        public DbSet<GameDetails> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAutoscaleThroughput(1000);
            modelBuilder.HasDefaultContainer("GameDetails");

            // Seed Genres
            var actionGenre = new Genre { Id = Guid.NewGuid(), Name = "Action" };
            var racingGenre = new Genre { Id = Guid.NewGuid(), Name = "Racing" };
            var fightingGenre = new Genre { Id = Guid.NewGuid(), Name = "Fighting" };

            modelBuilder.Entity<Genre>()
                .HasNoDiscriminator()
                .ToContainer("Genres")
                .HasPartitionKey(x => x.Id)
                .HasKey(x => x.Id);

            modelBuilder.Entity<Genre>().HasData(actionGenre, racingGenre, fightingGenre);

            // Seed Games
            modelBuilder.Entity<GameDetails>()
                .HasNoDiscriminator()
                .HasPartitionKey(x => x.GenreId)
                .HasKey(x => x.Id);

            modelBuilder.Entity<GameDetails>().HasData(
                new GameDetails
                {
                    Id = Guid.NewGuid(),
                    Name = "Street Fighter II",
                    GenreId = fightingGenre.Id,
                    Price = 19.99M,
                    ReleaseDate = new DateOnly(1992, 7, 15)
                },
                new GameDetails
                {
                    Id = Guid.NewGuid(),
                    Name = "Counter Strike II",
                    GenreId = actionGenre.Id,
                    Price = 15.99M,
                    ReleaseDate = new DateOnly(2020, 8, 23)
                },
                new GameDetails
                {
                    Id = Guid.NewGuid(),
                    Name = "Hill Climb Racing",
                    GenreId = racingGenre.Id,
                    Price = 30.99M,
                    ReleaseDate = new DateOnly(2024, 3, 12)
                }
            );
        }
    
}
}
