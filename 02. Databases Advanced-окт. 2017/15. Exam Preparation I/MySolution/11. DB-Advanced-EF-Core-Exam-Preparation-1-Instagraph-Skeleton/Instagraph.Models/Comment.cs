namespace Instagraph.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}