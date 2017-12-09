using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Acronym { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

        public ICollection<UserTeam> UsersTeam { get; set; } = new List<UserTeam>();

        public ICollection<TeamEvent> TeamEvents { get; set; } = new List<TeamEvent>();
        
    }
}
