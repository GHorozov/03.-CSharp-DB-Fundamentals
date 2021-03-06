﻿namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class Invitation
    {
        public Invitation()
        {
            this.IsActive = true;
        }

        public int InvitationId { get; set; }

        public int InvitedUserId { get; set; }
        public User InviteUser { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public bool IsActive { get; set; }
    }
}