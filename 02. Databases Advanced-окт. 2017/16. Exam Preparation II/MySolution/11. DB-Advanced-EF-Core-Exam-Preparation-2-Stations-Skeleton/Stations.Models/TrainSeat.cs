namespace Stations.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TrainSeat
    {
        public int Id { get; set; } //– integer, Primary Key

        public int TrainId { get; set; } //– integer(required)
        public Train Train { get; set; } //– train whose seats will be described(required)

        public int SeatingClassId { get; set; } //– integer(required)
        public SeatingClass SeatingClass { get; set; } //– class of the seats(required)

        public int Quantity { get; set; } //– how many seats of given class total for the given train(required, non-negative)
    }
}