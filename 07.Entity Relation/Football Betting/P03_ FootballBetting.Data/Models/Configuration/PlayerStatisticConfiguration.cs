using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Model.Configuration
{
    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.HasKey(pg => new { pg.PlayerId, pg.GameId });
            builder.HasOne(p => p.Player)
                .WithMany(ps => ps.PlayerGames)
                .HasForeignKey(p => p.PlayerId);
            builder.HasOne(g => g.Game)
                .WithMany(ps => ps.GamePlayers)
                .HasForeignKey(g => g.GameId);
        }
    }
}
