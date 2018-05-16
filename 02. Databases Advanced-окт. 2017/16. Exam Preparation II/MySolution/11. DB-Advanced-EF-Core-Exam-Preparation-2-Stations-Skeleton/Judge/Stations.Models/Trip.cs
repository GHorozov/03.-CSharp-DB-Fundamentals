namespace Stations.Models
{
    using Stations.Models.Enum;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Trip
    {
        public int Id { get; set; } //– integer, Primary Key

        public int OriginStationId { get; set; } // – integer(required)
        public Station OriginStation { get; set; } //– station from which the trip begins(required)

        public int DestinationStationId { get; set; }  //– integer(required)
        public Station DestinationStation { get; set; } //–  station where the trip ends(required)

        public DateTime DepartureTime { get; set; } //– date and time of departure from origin station(required)
        public DateTime ArrivalTime { get; set; } //– date and time of arrival at destination station, must be after departure time(required)

        public int TrainId { get; set; } //– integer(required)
        public Train Train { get; set; } //– train used for that particular trip(required)

        public TripStatus Status { get; set; } //– TripStatus enumeration with possible values: "OnTime", "Delayed" and "Early" (default: "OnTime")
        public TimeSpan? TimeDifference { get; set; } //– time(span) representing how late or early a given train was(optional)

    }
}