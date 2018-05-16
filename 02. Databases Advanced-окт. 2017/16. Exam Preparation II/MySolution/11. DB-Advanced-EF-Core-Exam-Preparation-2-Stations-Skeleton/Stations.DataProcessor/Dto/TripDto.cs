namespace Stations.DataProcessor.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TripDto
    {
        //"Train": "KB20012",
        //"OriginStation": "Sofia",
        //"DestinationStation": "Sofia Sever",
        //"DepartureTime": "27/12/2016 12:00",
        //"ArrivalTime": "27/12/2016 12:30",
        //"Status": "OnTime",

        public string Train { get; set; }
        public string OriginStation { get; set; }
        public string DestinationStation { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Status { get; set; }
        public string TimeDifference { get; set; }
    }
}