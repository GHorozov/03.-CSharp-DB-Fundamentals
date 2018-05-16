namespace Stations.Models
{
    using Stations.Models.Enum;
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class Train
    {
        public int Id { get; set; } //– integer, Primary Key
        public string TrainNumber { get; set; } //– text with max length 10 (required, unique) 
        public TrainType? Type { get; set; } //– TrainType enumeration with possible values: "HighSpeed", "LongDistance" or "Freight" (optional)

        public ICollection<TrainSeat> TrainSeats { get; set; } = new List<TrainSeat>(); // – Collection of type TrainSeat
        public ICollection<Trip> Trips { get; set; } = new List<Trip>(); //– Collection of type Trip

    }
}