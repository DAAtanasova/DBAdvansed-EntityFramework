using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Model.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.TeamId);

            builder.Property(n => n.Name)
                .IsRequired();

            builder.HasOne(pc => pc.PrimaryColor)
                .WithMany(t => t.PrimaryKitTeams)
                .HasForeignKey(pc => pc.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(sc => sc.SecondaryColor)
                .WithMany(t => t.SecondaryKitTeams)
                .HasForeignKey(sc => sc.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TownId);
        }
    }
}
