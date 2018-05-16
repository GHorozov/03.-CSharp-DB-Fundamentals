namespace PetClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    
    public class Vet
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required]
        public string Profession  { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string PhoneNumber  { get; set; }

        public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
    }
}