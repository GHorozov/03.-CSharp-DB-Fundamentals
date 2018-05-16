namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;
    using System.Text;
    using Newtonsoft.Json;
    using PetClinic.DataProcessor.Dto.Import;
    using System.Linq;
    using PetClinic.Models;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using System.IO;

    public class Deserializer
    {

        private const string  errorMsg = "Error: Invalid data.";
        private const string sucessMsg = "Record {0} successfully imported.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializeAnimalAids = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);

            foreach (var dto in deserializeAnimalAids)
            {
                if(dto.Name.Length < 3 || dto.Name.Length > 30 || dto.Price < 0.01m)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var isExists = context.AnimalAids.Any(ae => ae.Name == dto.Name);
                if (isExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var newAnimalAid = new AnimalAid()
                {
                    Name = dto.Name,
                    Price = dto.Price
                };

                context.AnimalAids.Add(newAnimalAid);
                context.SaveChanges();

                sb.AppendLine(String.Format(sucessMsg, dto.Name));
            }

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializeAnimals = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);

            foreach (var dto in deserializeAnimals)
            {
                if(dto.Name == null || dto.Name.Length < 3 || dto.Name.Length > 20 || dto.Type == null || dto.Type.Length < 3 || dto.Type.Length > 20 || dto.Age == null || dto.Age <= 0)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                //serialNumber
                var regexSerialNumber = new Regex(@"^[A-Za-z]{7}\d{3}$");
                var isMatchSerialNumber = regexSerialNumber.IsMatch(dto.Passport.SerialNumber); 


                if (!isMatchSerialNumber)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                

                //ownerName
                if(dto.Passport.OwnerName.Length < 3 || dto.Passport.OwnerName.Length > 30 )
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

     
                var regexPhone = new Regex(@"^\+359\d{9}$");
                var matchOwnerPhone = regexPhone.IsMatch(dto.Passport.OwnerPhoneNumber);

                var regexPhoneOwner = new Regex(@"^0\d{9}$");
                var matchPhoneOwnerTenDigits = regexPhoneOwner.IsMatch(dto.Passport.OwnerPhoneNumber);

                if (!matchOwnerPhone && !matchPhoneOwnerTenDigits)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }


                var regDate = DateTime.ParseExact(dto.Passport.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                var newPassport = new Passport()
                {
                    SerialNumber = dto.Passport.SerialNumber,
                    OwnerName = dto.Passport.OwnerName,
                    OwnerPhoneNumber = dto.Passport.OwnerPhoneNumber,
                    RegistrationDate = regDate
                };

                var isExistSerialNumber = context.Passports.Any(p => p.SerialNumber == newPassport.SerialNumber);
                if (isExistSerialNumber)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var newAnimal = new Animal()
                {
                    Name = dto.Name,
                    Type = dto.Type,
                    Age = dto.Age.Value,
                    Passport = newPassport
                };

                context.Animals.Add(newAnimal);
                context.SaveChanges();

                sb.AppendLine($"Record {dto.Name} Passport №: {dto.Passport.SerialNumber} successfully imported.");
            }

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var XDoc = XDocument.Parse(xmlString);
            var elements = XDoc.Root.Elements();


            var listVets = new List<Vet>();
            foreach (var e in elements)
            {
                var nameString = e.Element("Name")?.Value;
                var profession = e.Element("Profession")?.Value;
                var ageStr = e.Element("Age")?.Value;
                var phoneNumberString = e.Element("PhoneNumber")?.Value;

                var age = int.Parse(ageStr);

                //Check Phone number is valid
                if (phoneNumberString == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var regexPhone = new Regex(@"^\+359\d{9}$");
                var matchOwnerPhone = regexPhone.IsMatch(phoneNumberString);

                var regexPhoneOwner = new Regex(@"^0\d{9}$");
                var matchPhoneOwnerTenDigits = regexPhoneOwner.IsMatch(phoneNumberString);

                if (!matchOwnerPhone && !matchPhoneOwnerTenDigits)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                if (nameString == null || nameString.Length < 3 || nameString.Length > 40  ||
                    profession == null || profession.Length < 3 || profession.Length > 50 ||
                    ageStr == null || age < 22 || age > 65)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }              

                var isVetPhoneExistsInList = listVets.Any(v => v.PhoneNumber == phoneNumberString);
                var isVetPhoneExistInBase = context.Vets.Any(v => v.PhoneNumber == phoneNumberString);
                if (isVetPhoneExistsInList || isVetPhoneExistInBase)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var newVet = new Vet()
                {
                    Name = nameString,
                    Age = age,
                    PhoneNumber = phoneNumberString,
                    Profession = profession              
                };

                listVets.Add(newVet);
                sb.AppendLine($"Record {nameString} successfully imported.");
            }

            context.Vets.AddRange(listVets);
            context.SaveChanges();

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));

            var procedures = (ProcedureDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var validProcedures = new List<Procedure>();
            foreach (var dto in procedures)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }
                if (!context.Animals.Any(x => x.PassportSerialNumber == dto.Animal) || !DateTime.TryParseExact(
                        dto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var dateTime) || !context.Vets.Any(x => x.Name == dto.Vet) ||
                    !dto.AnimalAids.All(x => context.AnimalAids.Any(ai => ai.Name == x.Name)))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }
                var procedure = new Procedure()
                {
                    Animal = context.Animals.SingleOrDefault(x => x.PassportSerialNumber == dto.Animal),
                    Vet = context.Vets.SingleOrDefault(x => x.Name == dto.Vet),
                    DateTime = dateTime
                };

                var aidsToEnter = new List<ProcedureAnimalAid>();
                var toBreak = false;
                foreach (var aniDto in dto.AnimalAids)
                {
                    if (!IsValid(aniDto))
                    {
                        sb.AppendLine("Error: Invalid data.");
                        continue;
                    }
                    if (aidsToEnter.Any(x => x.AnimalAid.Name == aniDto.Name))
                    {
                        sb.AppendLine("Error: Invalid data.");
                        toBreak = true;
                        break;
                    }
                    var aid = new ProcedureAnimalAid()
                    {
                        Procedure = procedure,
                        AnimalAid = context.AnimalAids.SingleOrDefault(x => x.Name == aniDto.Name)
                    };
                    aidsToEnter.Add(aid);
                }
                if (toBreak)
                {
                    toBreak = false;
                    continue;
                }
                procedure.ProcedureAnimalAids = aidsToEnter;
                validProcedures.Add(procedure);
                sb.AppendLine("Record successfully imported.");
            }
            context.Procedures.AddRange(validProcedures);
            context.SaveChanges();
            return sb.ToString();


            //var sb = new StringBuilder();
            //var XDoc = XDocument.Parse(xmlString);
            //var elements = XDoc.Root.Elements();

            //var validProcedures = new List<Procedure>();

            //foreach (var e in elements)
            //{
            //    var vetStr = e.Element("Vet")?.Value;
            //    var animalStr = e.Element("Animal")?.Value;
            //    var dateTimeStr = e.Element("DateTime")?.Value;

            //    if(vetStr == null || animalStr == null || dateTimeStr == null)
            //    {
            //        sb.AppendLine(errorMsg);
            //        continue;
            //    }

            //    var regexSerialNumber = new Regex(@"^[A-Za-z]{7}\d{3}$");
            //    var isMatchSerialNumber = regexSerialNumber.IsMatch(animalStr);


            //    if (vetStr.Length < 3 || vetStr.Length > 40 || !isMatchSerialNumber || !DateTime.TryParseExact(
            //           dateTimeStr, "dd-MM-yyyy", CultureInfo.InvariantCulture,
            //            DateTimeStyles.None, out var dateTime))
            //    {
            //        sb.AppendLine(errorMsg);
            //        continue;
            //    }


            //    var vet = context.Vets.FirstOrDefault(v => v.Name == vetStr);
            //    if(vet == null)
            //    {
            //        sb.AppendLine(errorMsg);
            //        continue;
            //    }

            //    var animal = context.Animals.FirstOrDefault(a => a.PassportSerialNumber == animalStr);
            //    if(animal == null)
            //    {
            //        sb.AppendLine(errorMsg);
            //        continue;
            //    }

            //    var newProc = new Procedure()
            //    {
            //        Animal = animal,
            //        Vet = vet,
            //        DateTime = dateTime
            //    };

            //    var procedureAnimalAidsList = new List<ProcedureAnimalAid>();
            //    var breakAdding = false;

            //    var animAid = e.Elements("AnimalAids");
            //    foreach (var a in animAid)
            //    {
            //        var name = a.Element("AnimalAid").Element("Name")?.Value;

            //        if(name == null || name.Length < 3 || name.Length > 30)
            //        {
            //            sb.AppendLine("Error: Invalid data.");
            //            continue;
            //        }                   

            //        if(!context.AnimalAids.Any(x => x.Name == name))
            //        {
            //            sb.AppendLine("Error: Invalid data.");
            //            continue;
            //        }

            //        if(procedureAnimalAidsList.Any(x => x.AnimalAid.Name == name))
            //        {
            //            sb.AppendLine(errorMsg);
            //            breakAdding = true;
            //            break;
            //        }

            //        var newProcedureAnimalAid = new ProcedureAnimalAid()
            //        {
            //            Procedure = newProc,
            //            AnimalAid = context.AnimalAids.FirstOrDefault(x => x.Name == name)
            //        };

            //        procedureAnimalAidsList.Add(newProcedureAnimalAid);
            //    }

            //    if(breakAdding)
            //    {
            //        breakAdding = false;
            //        continue;
            //    }

            //    newProc.ProcedureAnimalAids = procedureAnimalAidsList;
            //    validProcedures.Add(newProc);

            //    sb.AppendLine($"Record successfully imported.");
            //}

            //context.Procedures.AddRange(validProcedures);
            //context.SaveChanges();

            //var result = sb.ToString().Trim();
            //return result;
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, results, true);
        }
    }
}
