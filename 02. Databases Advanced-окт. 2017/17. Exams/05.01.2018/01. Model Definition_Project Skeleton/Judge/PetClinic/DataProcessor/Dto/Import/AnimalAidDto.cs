namespace PetClinic.DataProcessor.Dto.Import
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Serialization;

    [XmlType("AnimalAid")]
    public class AnimalAidDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}