namespace FastFood.DataProcessor.Dto.Import
{
    using FastFood.Models;
    using FastFood.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    
    public class OrderDto
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        public Employee Employee { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Type { get; set; }

        public OrderItem OrdersItems { get; set; }


    }
}