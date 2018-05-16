namespace Stations.Models
{
    using Stations.Models.Enum;
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class CustomerCard
    {
       public int Id { get; set; } //– integer, Primary Key
       public string Name { get; set; } //– text with max length 128 (required)
       public int Age { get; set; } //– integer between 0 and 120
       public CardType Type { get; set; } //– CardType enumeration with values: "Pupil", "Student", "Elder", "Debilitated", "Normal" (default: Normal)

        public ICollection<Ticket> BoughtTickets { get; set; } = new List<Ticket>(); //– Collection of type Ticket
    }
}