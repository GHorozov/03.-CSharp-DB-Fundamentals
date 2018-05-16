namespace Stations.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class Ticket
    {
        public int Id { get; set; } //– integer, Primary Key
        public decimal Price { get; set; } //– decimal value of the ticket(required, non-negative)
        public string SeatingPlace { get; set; } //– text with max length of 8 which combines seating class abbreviation plus a positive integer(required)

        public int TripId { get; set; } //– integer(required)
        public Trip Trip { get; set; } //– the trip for which the ticket is for (required)

        public int? CustomerCardId { get; set; } //– integer(optional)
        public CustomerCard CustomerCard { get; set; } //– reference to the ticket’s buyer
    }
}