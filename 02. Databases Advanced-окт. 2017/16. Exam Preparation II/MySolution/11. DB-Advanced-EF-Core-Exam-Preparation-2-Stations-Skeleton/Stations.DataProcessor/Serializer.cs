using System;
using Stations.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;
using Stations.Models.Enum;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using Stations.DataProcessor.Dto.Export;

namespace Stations.DataProcessor
{
	public class Serializer
	{
        public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
        {
            var inputdate = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var delayedTrains = context.Trains
                .Where(t => t.Trips.Any(tr => tr.DepartureTime <= inputdate && tr.Status == Models.Enum.TripStatus.Delayed))
                .Select(t => new
                {
                    t.TrainNumber,
                    DelayedTimes = t.Trips.Where(tr => tr.Status == TripStatus.Delayed && tr.ArrivalTime <= inputdate).Count(),
                    MaxDelayedTime = t.Trips
                        .Where(tr => tr.Status == TripStatus.Delayed)
                        .OrderByDescending(tr => tr.TimeDifference)
                        .Select(tr => tr.TimeDifference)
                        .First()
                })
                .OrderByDescending(o => o.DelayedTimes)
                .ThenByDescending(o => o.MaxDelayedTime)
                .ThenBy(o => o.TrainNumber)
                .ToArray();


            var json = JsonConvert.SerializeObject(delayedTrains, Newtonsoft.Json.Formatting.Indented);

            return json;
		}

		public static string ExportCardsTicket(StationsDbContext context, string cardType)
		{
            var cards = context.Cards
                 .Where(c => (CardType)Enum.Parse(typeof(CardType), cardType) == c.Type && c.BoughtTickets.Count > 0)
                 .Select(c => new
                 {
                     Name = c.Name,
                     Type = cardType,
                     Tickets = c.BoughtTickets
                         .Select(t => new
                         {
                             OriginStation = t.Trip.OriginStation.Name,
                             DestinationStation = t.Trip.DestinationStation.Name,
                             DepartureTime = t.Trip.DepartureTime.ToString("dd/MM/yyyy HH:mm")
                         })
                 })
                 .OrderBy(c => c.Name)
                 .ToArray();

            var xDoc = new XDocument();

            xDoc.Add(new XElement("Cards"));

            foreach (var c in cards)
            {
                var card = new XElement("Card", new XAttribute("name", c.Name), new XAttribute("type", c.Type));
                var tickets = new XElement("Tickets");

                foreach (var t in c.Tickets)
                {
                    var ticket = new XElement("Ticket");

                    ticket.Add(
                        new XElement("OriginStation", t.OriginStation),
                        new XElement("DestinationStation", t.DestinationStation),
                        new XElement("DepartureTime", t.DepartureTime)
                        );

                    tickets.Add(ticket);
                }

                card.Add(tickets);
                xDoc.Element("Cards").Add(card);
            }

            return xDoc.ToString();


            //var type = Enum.Parse<CardType>(cardType);

            //var cards = context.Cards
            //    .Where(c => c.Type == type && c.BoughtTickets.Any())
            //    .Select(c => new CardDto
            //    {
            //        Name = c.Name,
            //        Type = c.Type.ToString(),
            //        Tickets = c.BoughtTickets.Select(t => new TicketDto
            //        {
            //            OriginStation = t.Trip.OriginStation.Name,
            //            DestinationStation = t.Trip.DestinationStation.Name,
            //            DepartureTime = t.Trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
            //        }).ToArray()
            //    })
            //    .OrderBy(c => c.Name)
            //    .ToArray();

            //var sb = new StringBuilder();

            //var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            //serializer.Serialize(new StringWriter(sb), cards, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            //var result = sb.ToString();
            //return result;

        }
    }
}