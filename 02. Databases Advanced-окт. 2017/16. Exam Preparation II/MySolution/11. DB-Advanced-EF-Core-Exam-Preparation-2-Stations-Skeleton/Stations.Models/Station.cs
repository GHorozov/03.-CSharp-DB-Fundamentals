namespace Stations.Models
{
    using System.Collections.Generic;

    public class Station
    {
        public int Id { get; set; } //– integer, Primary Key
        public string Name { get; set; } //– text with max length 50 (required, unique)
        public string Town { get; set; } //– text with max length 50 (required)

        public ICollection<Trip> TripsTo { get; set; } = new List<Trip>(); //– Collection of type Trip
        public ICollection<Trip> TripsFrom { get; set; } = new List<Trip>(); // – Collection of type Trip

    }
}
