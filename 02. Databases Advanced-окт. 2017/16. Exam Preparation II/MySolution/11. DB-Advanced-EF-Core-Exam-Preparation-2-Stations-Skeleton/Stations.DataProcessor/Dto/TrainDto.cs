namespace Stations.DataProcessor.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class TrainDto
    {
        public string TrainNumber { get; set; }
        public string Type { get; set; }
        public SeatDto[] Seats { get; set; }
    }
}