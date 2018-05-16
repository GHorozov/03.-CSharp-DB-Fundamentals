using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
	public class Employee
	{
        public int Id { get; set; }       //– integer, Primary Key

        [MinLength(3)]
        public string Name { get; set; }  //– text with min length 3 and max length 30 (required)

        [Range(15, 80)]
        public int Age { get; set; }      //– integer in the range[15, 80] (required)
        
        public int PositionId { get; set; } // ¬– integer, foreign key
        [Required]
        public Position Position { get; set; } //– the employee’s position(required)

        public ICollection<Order> Orders { get; set; } = new List<Order>(); // – the orders the employee has processed
    }
}