namespace TeamBuilder.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using TeamBuilder.Data.EntriesConfiguration;
    using TeamBuilder.Models;

    public class TeamBuilderContext :DbContext
    {
        public TeamBuilderContext()
        {

        }

        public TeamBuilderContext(DbContextOptions options)
            :base(options)
        {

        }

        //DBSets
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<EventTeam> EventTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new InvitationConfiguration());
            modelBuilder.ApplyConfiguration(new UserTeamConfiguration());
            modelBuilder.ApplyConfiguration(new EventTeamConfiguration());
        }

    }
}
