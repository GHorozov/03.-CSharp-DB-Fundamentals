namespace FastFood.DataProcessor.Dto
{
    using FastFood.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    
    public class EmployeeDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Range(15, 80)]
        public int Age { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Position { get; set; }
    }
}