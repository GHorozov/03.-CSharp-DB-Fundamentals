using System;
using Stations.Data;
using Newtonsoft.Json;
using Stations.DataProcessor.Dto;
using Stations.Models;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Stations.Models.Enum;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Stations.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportStations(StationsDbContext context, string jsonString)
        {
            var deserializeStations = JsonConvert.DeserializeObject<StationDto[]>(jsonString);

            var stations = new List<Station>();
            var sb = new StringBuilder();
            foreach (var dto in deserializeStations)
            {
                if (dto.Town == null)
                {
                    dto.Town = dto.Name;
                }

                if (dto.Name == null || dto.Name.Length > 50 || dto.Town.Length > 50)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var stationExist = context.Stations.Any(s => s.Name == dto.Name);

                if (stationExist)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var newStation = new Station()
                {
                    Name = dto.Name,
                    Town = dto.Town
                };

                context.Stations.Add(newStation);
                context.SaveChanges();

                sb.AppendLine(String.Format(SuccessMessage, dto.Name));
            }

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportClasses(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializeSeatingClasses = JsonConvert.DeserializeObject<SeatingClassesDto[]>(jsonString);

            foreach (var dto in deserializeSeatingClasses)
            {
                if (dto.Name == null || dto.Abbreviation == null || dto.Name.Length > 30 || dto.Abbreviation.Length < 2 || dto.Abbreviation.Length > 2)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var isNameExists = context.SeatingClasses.Any(sc => sc.Name == dto.Name);
                var isAbrExists = context.SeatingClasses.Any(sc => sc.Abbreviation == dto.Abbreviation);

                if (isNameExists || isAbrExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var newSeatingClasses = new SeatingClass()
                {
                    Name = dto.Name,
                    Abbreviation = dto.Abbreviation
                };

                context.SeatingClasses.Add(newSeatingClasses);
                context.SaveChanges();

                sb.AppendLine(String.Format(SuccessMessage, dto.Name));
            }

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportTrains(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializeTrains = JsonConvert.DeserializeObject<TrainDto[]>(jsonString);

            foreach (var dto in deserializeTrains)
            {
                if (dto.TrainNumber == null || context.Trains.Any(t => t.TrainNumber == dto.TrainNumber) || dto.TrainNumber.Length > 10)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var type = dto.Type == null ? TrainType.HighSpeed : (TrainType)Enum.Parse(typeof(TrainType), dto.Type);

                var trainSeats = new List<TrainSeat>();
                if (dto.Seats != null)
                {
                    if (!dto.Seats.All(s => s.Abbreviation != null && s.Name != null && s.Quantity != null && context.SeatingClasses.Any(c => c.Name == s.Name && c.Abbreviation == s.Abbreviation) && s.Quantity > 0))
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }

                    foreach (var s in dto.Seats)
                    {
                        var classe = context.SeatingClasses.SingleOrDefault(c => c.Name == s.Name && c.Abbreviation == s.Abbreviation);

                        trainSeats.Add(new TrainSeat { Quantity = (int)s.Quantity, SeatingClass = classe });
                    }
                }

                var newTrain = new Train
                {
                    TrainNumber = dto.TrainNumber,
                    Type = type,
                    TrainSeats = trainSeats
                };

                context.Trains.Add(newTrain);
                context.SaveChanges();

                sb.AppendLine(String.Format(SuccessMessage, dto.TrainNumber));
            }

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializeTrips = JsonConvert.DeserializeObject<TripDto[]>(jsonString);

            foreach (var dto in deserializeTrips)
            {
                if (dto.Status == null)
                {
                    dto.Status = "OnTime";
                }

                var status = Enum.Parse<TripStatus>(dto.Status);

                if (dto.DestinationStation == null || dto.OriginStation == null || dto.DepartureTime == null || dto.ArrivalTime == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!context.Trains.Any(t => t.TrainNumber == dto.Train) || !context.Stations.Any(s => s.Name == dto.OriginStation || s.Name == dto.DestinationStation))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var train = context.Trains.FirstOrDefault(t => t.TrainNumber == dto.Train);

                if (train == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var originStation = context.Stations.FirstOrDefault(os => os.Name == dto.OriginStation);
                var destinationStation = context.Stations.FirstOrDefault(ds => ds.Name == dto.DestinationStation);

                if (originStation == null || destinationStation == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var departureTime = DateTime.ParseExact(dto.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var arrivalTime = DateTime.ParseExact(dto.ArrivalTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                if (departureTime > arrivalTime)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                TimeSpan? timeDifference = null;
                if (dto.TimeDifference != null)
                {
                    timeDifference = TimeSpan.ParseExact(dto.TimeDifference, "hh\\:mm", CultureInfo.InvariantCulture);
                }

                var newTrip = new Trip()
                {
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destinationStation,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    Status = status,
                    TimeDifference = timeDifference
                };

                context.Trips.Add(newTrip);
                context.SaveChanges();

                sb.AppendLine($"Trip from {dto.OriginStation} to {dto.DestinationStation} imported.");
            }

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            foreach (var e in elements)
            {
                var name = e.Element("Name")?.Value;
                var ageString = e.Element("Age")?.Value;
                var type = e.Element("CardType")?.Value;

                if (type == null)
                {
                    type = "Normal";
                }

                var age = int.Parse(ageString);
                if (name == null || name.Length > 128 || ageString == null || age > 120 || age < 0)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var cardType = Enum.Parse<CardType>(type);

                var newCard = new CustomerCard()
                {
                    Name = name,
                    Age = age,
                    Type = cardType
                };

                context.Cards.Add(newCard);
                context.SaveChanges();

                sb.AppendLine(String.Format(SuccessMessage, name));
            }

            var result = sb.ToString().Trim();
            return result;
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            var result = new List<string>();
            var validTickets = new List<Ticket>();

            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            foreach (var ticket in elements)
            {
                var priceAttr = ticket.Attribute("price")?.Value;
                var seatAttr = ticket.Attribute("seat")?.Value;

                if (priceAttr == null || seatAttr == null || seatAttr.Length > 8)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                var tripEl = ticket.Element("Trip");

                var origin = tripEl.Element("OriginStation")?.Value;
                var destination = tripEl.Element("DestinationStation")?.Value;
                var timeStr = tripEl.Element("DepartureTime")?.Value;

                if (origin == null || destination == null || timeStr == null)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                var time = DateTime.ParseExact(timeStr, "dd/MM/yyyy HH:mm", null);

                var originStation = context.Stations
                    .SingleOrDefault(s => s.Name == origin);

                var destinationStation = context.Stations
                    .SingleOrDefault(s => s.Name == destination);

                if (originStation == null || destinationStation == null)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                var trip = context.Trips
                    .Include(t => t.Train)
                    .SingleOrDefault(t => t.OriginStation == originStation && t.DestinationStation == destinationStation && t.DepartureTime == time);

                if (trip == null)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                var train = trip.Train;

                var abbreviation = seatAttr.Substring(0, 2);
                int number;
                var isNumber = int.TryParse(seatAttr.Substring(2), out number);

                if (!isNumber || number <= 0)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                var classe = context.SeatingClasses
                    .SingleOrDefault(sc => sc.Abbreviation == abbreviation);

                if (classe == null)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                var trainSeat = context.TrainSeats
                    .SingleOrDefault(ts => ts.Train == train && ts.SeatingClass == classe);

                if (trainSeat == null)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                if (trainSeat.Quantity < number)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                CustomerCard card = null;
                var cardEl = ticket.Element("Card");
                if (cardEl != null)
                {
                    var name = cardEl.Attribute("Name")?.Value;

                    card = context.Cards
                        .SingleOrDefault(c => c.Name == name);

                    if (card == null)
                    {
                        result.Add(FailureMessage);
                        continue;
                    }
                }

                decimal price = decimal.Parse(priceAttr);
                if (price < 0)
                {
                    result.Add(FailureMessage);
                    continue;
                }

                validTickets.Add(new Ticket { CustomerCard = card, Price = price, SeatingPlace = seatAttr, Trip = trip });
                result.Add($"Ticket from {origin} to {destination} departing at {time:dd/MM/yyyy HH:mm} imported.");
            }

            context.Tickets.AddRange(validTickets);
            context.SaveChanges();
            return String.Join(Environment.NewLine, result);




            //var sb = new StringBuilder();

            //var xDoc = XDocument.Parse(xmlString);
            //var elements = xDoc.Root.Elements();

            //foreach (var e in elements)
            //{
            //    var ticketPriceString = e.Attribute("price")?.Value;
            //    var ticketSeat = e.Attribute("seat")?.Value;

            //    //price
            //    var ticketPrice = decimal.Parse(ticketPriceString);
            //    if (ticketPriceString == null || ticketPrice < 0)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    //seat
            //    if (ticketSeat == null || ticketSeat.Length > 8)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    //trips
            //    var tripEl = e.Element("Trip");

            //    var originStation = tripEl.Element("OriginStation")?.Value;
            //    var destinationStation = tripEl.Element("DestinationStation")?.Value;
            //    var departureTime = tripEl.Element("DepartureTime")?.Value;

            //    if (originStation == null || destinationStation == null || departureTime == null)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    var depTime = DateTime.ParseExact(departureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            //    var origin = context.Stations.FirstOrDefault(s => s.Name == originStation);
            //    var destination = context.Stations.FirstOrDefault(s => s.Name == destinationStation);

            //    if (origin == null || destination == null)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    var trip = context.Trips
            //        .Include(t => t.Train)
            //        .FirstOrDefault(t => t.OriginStation == origin && t.DestinationStation == destination && t.DepartureTime == depTime);

            //    if (trip == null)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    var train = trip.Train;



            //     //seat
            //    var abbreviation = ticketSeat.Substring(0, 2);
            //    int number;
            //    var isNumber = int.TryParse(ticketSeat.Substring(2), out number);

            //    if (!isNumber || number < 0)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    var classe = context.SeatingClasses
            //       .SingleOrDefault(sc => sc.Abbreviation == abbreviation);
            //    if (classe == null)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    var trainSeats = context.TrainSeats.FirstOrDefault(ts => ts.Train == train && ts.SeatingClass == classe);
            //    if (trainSeats == null)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    if (number > trainSeats.Quantity)
            //    {
            //        sb.AppendLine(FailureMessage);
            //        continue;
            //    }

            //    //card
            //    CustomerCard card = null;
            //    var cardElement = e.Element("Card");

            //    if (cardElement != null)
            //    {
            //        var name = cardElement.Attribute("Name")?.Value;

            //        card = context.Cards.FirstOrDefault(c => c.Name == name);

            //        if (card == null)
            //        {
            //            sb.AppendLine(FailureMessage);
            //            continue;
            //        }
            //    }

            //    //new ticket
            //    var newTicket = new Ticket()
            //    {
            //        CustomerCard = card,
            //        Price = ticketPrice,
            //        SeatingPlace = ticketSeat,
            //        Trip = trip
            //    };

            //    context.Tickets.Add(newTicket);
            //    context.SaveChanges();

            //    sb.AppendLine($"Ticket from {originStation} to {destinationStation} departing at {depTime} imported.");
            //}

            //var result = sb.ToString().Trim();
            //return result;
        }
    }
}