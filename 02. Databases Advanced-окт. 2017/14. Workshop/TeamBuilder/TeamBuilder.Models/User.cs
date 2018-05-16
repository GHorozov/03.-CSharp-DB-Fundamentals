namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public int UserId { get; set; }
        [MinLength(3)]
        public string Username { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public bool IsDeleted { get; set; }


        public ICollection<UserTeam> CreatedUserTeams { get; set; } = new List<UserTeam>();
        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
        public ICollection<Invitation> ReceivedInvitations { get; set; } = new List<Invitation>();
        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
