namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;
    using System.Linq;
    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;
    using System.Xml.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using PetClinic.DataProcessor.Dto.Export;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals
                    .Include(p => p.Passport)
                    .Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                    .Select(o => new
                    {
                        OwnerName = o.Passport.OwnerName,
                        AnimalName = o.Name,
                        Age = o.Age,
                        SerialNumber = o.Passport.SerialNumber,
                        RegisteredOn = o.Passport.RegistrationDate.ToString("dd-MM-yyyy")
                    })
                    .OrderBy(o => o.Age)
                    .ThenBy(o => o.SerialNumber)
                    .ToArray();

            var json = JsonConvert.SerializeObject(animals, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {

            var procedures = context.Procedures
                .Include(x => x.Animal)
                .ThenInclude(x => x.Passport)
                .Include(x => x.ProcedureAnimalAids)
                .ThenInclude(x => x.AnimalAid)
                .OrderBy(x => x.DateTime)
                .ThenBy(x => x.Animal.Passport.SerialNumber)
                .Select(x => new ProcDto()
                {
                    Passport = x.Animal.PassportSerialNumber,
                    OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                    DateTime = x.DateTime.ToString("dd-MM-yyyy"),
                    AnimalAids = x.ProcedureAnimalAids.Select(pa => new AniAidDto()
                    {
                        Name = pa.AnimalAid.Name,
                        Price = pa.AnimalAid.Price
                    }).ToArray(),
                    TotalPrice = x.ProcedureAnimalAids.Select(pa => pa.AnimalAid.Price).Sum()
                })
                .ToList();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<ProcDto>), new XmlRootAttribute("Procedures"));
            serializer.Serialize(new StringWriter(sb), procedures,
                new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            return sb.ToString();

            //var xDoc = new XDocument();
            //xDoc.Add(new XElement("Procedures"));

            //foreach (var p in procedures)
            //{
            //    var procedureElements = new XElement("Procedure");
            //    procedureElements.Add(new XElement("Passport", p.Passport));
            //    procedureElements.Add(new XElement("OwnerNumber", p.OwnerNumber));
            //    procedureElements.Add(new XElement("DateTime"), p.DateTime);


            //    var animalAidElements = new XElement("AnimalAids");

            //    foreach (var aa in p.AnimalAids)
            //    {
            //        var animalAid = new XElement("AnimalAid");

            //        animalAid.Add(new XElement("Name", aa.Name));
            //        animalAid.Add(new XElement("Price", aa.Price));

            //        animalAidElements.Add(animalAid);
            //    }

            //    procedureElements.Add(animalAidElements);
            //    procedureElements.Add(new XElement("TotalPrice", p.TotalPrice));
            //    xDoc.Element("Procedures").Add(procedureElements);
            //}

            //return xDoc.ToString();
        }
    }
}
