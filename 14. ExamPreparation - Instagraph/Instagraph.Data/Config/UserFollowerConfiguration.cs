using System;
using System.Collections.Generic;
using System.Text;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    public class UserFollowerConfiguration : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(p => new { p.UserId, p.FollowerId });

            builder.HasOne(uf => uf.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uf => uf.Follower)
                .WithMany(f => f.UsersFollowing)
                .HasForeignKey(uf => uf.FollowerId);
        }
    }
}
