using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamBuilder.Models
{
    public class User
    {
        public User()
        {
            this.IsDeleted = false;
        }
        public int UserId { get; set; }

        [MinLength(3)]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int? Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Team> CreatedTeams { get; set; } = new List<Team>();

        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();

        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
    }
}
