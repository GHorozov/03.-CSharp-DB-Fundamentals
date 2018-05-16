using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [MinLength(3)]
        public string Name { get; set; }

        [MinLength(3)]
        public string Type { get; set; }

        [Required]
        public int Age { get; set; }

        [ForeignKey("SerialNumber")]
        public string PassportSerialNumber  { get; set; }

        [Required]
        public Passport  Passport  { get; set; }

        
    
        public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
    }
}
