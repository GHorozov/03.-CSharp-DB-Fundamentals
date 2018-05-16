namespace Stations.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class SeatingClass
    {
        public int Id { get; set; } //– integer, Primary Key
        public string Name { get; set; } //– text with max length 30 (required, unique)

        [MinLength(2)]
        public string Abbreviation { get; set; } //– text with an exact length of 2 (no more, no less), (required, unique)
    }
}