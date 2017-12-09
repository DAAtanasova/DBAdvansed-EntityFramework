using System;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.Data.Configuration;
using TeamBuilder.Models;

namespace TeamBuilder.Data
{
    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext(){ }

        public TeamBuilderContext(DbContextOptions options):
            base(options) { }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<UserTeam> UsersTeams { get; set; }

        public DbSet<TeamEvent> TeamsEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ServerConfig.configurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new TeamConfig());
            builder.ApplyConfiguration(new EventConfig());
            builder.ApplyConfiguration(new InvitationConfig());
            builder.ApplyConfiguration(new UserTeamConfig());
            builder.ApplyConfiguration(new TeamEventConfig());
        }
    }
}
