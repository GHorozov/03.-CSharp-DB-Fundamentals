namespace PetClinic.DataProcessor.Dto.Import
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class PassportDto
    {
        public string SerialNumber { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string RegistrationDate { get; set; }
    }
}