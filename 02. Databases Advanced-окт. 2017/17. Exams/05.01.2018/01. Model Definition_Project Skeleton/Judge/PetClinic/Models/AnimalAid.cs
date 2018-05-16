namespace PetClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    
    public class AnimalAid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<ProcedureAnimalAid> AnimalAidProcedures  { get; set; } = new List<ProcedureAnimalAid>();
    }
}