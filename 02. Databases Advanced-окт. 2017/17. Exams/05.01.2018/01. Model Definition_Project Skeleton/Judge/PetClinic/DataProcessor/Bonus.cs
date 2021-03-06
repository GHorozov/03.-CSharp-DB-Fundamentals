﻿namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;
    using System.Linq;
    using System.Text;

    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            var vet = context.Vets.Where(v => v.PhoneNumber == phoneNumber).FirstOrDefault();

            if(vet == null)
            {
                return $"Vet with phone number {phoneNumber} not found!";
            }

            var oldProffesion = vet.Profession;
            vet.Profession = newProfession;
            context.SaveChanges();

            return $"{vet.Name}'s profession updated from {oldProffesion} to {newProfession}.";
        }
    }
}
