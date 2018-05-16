namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Key]
        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<EventTeam> EventTeams { get; set; } = new List<EventTeam>();

    }
}