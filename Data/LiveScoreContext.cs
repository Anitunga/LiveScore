using LiveScore.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveScore.Data
{
    public class LiveScoreContext : DbContext
    {
        public LiveScoreContext(DbContextOptions options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Quarter> Quarters { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Substitution> Substitutions { get; set; }
        public DbSet<Foul> Fouls { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Team -> Player relationship
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            // Match -> Team relationships
            modelBuilder.Entity<Match>()
                .HasOne(m => m.TeamA)
                .WithMany()
                .HasForeignKey(m => m.TeamAId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.TeamB)
                .WithMany()
                .HasForeignKey(m => m.TeamBId)
                .OnDelete(DeleteBehavior.NoAction);

            // Match -> User relationships
            modelBuilder.Entity<Match>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quarter -> Events relationship
            modelBuilder.Entity<Quarter>()
                .HasMany(q => q.Events)
                .WithOne(e => e.Quarter)
                .HasForeignKey(e => e.QuarterId)
                .OnDelete(DeleteBehavior.NoAction);

            // Substitution -> Players relationship
            modelBuilder.Entity<Substitution>()
                .HasOne(s => s.PlayerIn)
                .WithMany()
                .HasForeignKey(s => s.PlayerInId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Substitution>()
                .HasOne(s => s.PlayerOut)
                .WithMany()
                .HasForeignKey(s => s.PlayerOutId)
                .OnDelete(DeleteBehavior.NoAction);

            // Foul -> Player relationship
            modelBuilder.Entity<Foul>()
                .HasOne(f => f.Player)
                .WithMany()
                .HasForeignKey(f => f.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
