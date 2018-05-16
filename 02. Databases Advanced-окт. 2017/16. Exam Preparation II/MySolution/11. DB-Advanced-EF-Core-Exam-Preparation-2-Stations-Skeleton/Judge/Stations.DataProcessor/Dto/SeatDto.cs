namespace Stations.DataProcessor.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class SeatDto
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int? Quantity { get; set; }
    }
}