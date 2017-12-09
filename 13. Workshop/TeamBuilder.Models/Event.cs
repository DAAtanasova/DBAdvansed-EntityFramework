using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<TeamEvent> EventTeams { get; set; } = new List<TeamEvent>();
    }
}
