namespace PetClinic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Passport
    {
        [Key]
        public string SerialNumber { get; set; }

        [Required]
        public Animal Animal { get; set; }

        [Required]
        public string OwnerPhoneNumber { get; set; }

        [MinLength(3)]
        [MaxLength(30)]
        [Required]
        public string OwnerName  { get; set; }

        [Required]
        public DateTime RegistrationDate  { get; set; }

    }
}