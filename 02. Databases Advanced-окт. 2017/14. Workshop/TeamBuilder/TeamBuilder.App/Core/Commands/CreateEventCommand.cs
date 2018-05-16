namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using TeamBuilder.App.Core.Commands.Interfaces;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class CreateEventCommand : ICommand
    {
        private const string DateFormat = "dd/MM/yyyy HH:mm";

        //<name> <description> <startDate> <endDate>
        public string Execute(IUserSession userSession, params string[] data)
        {
            var eventName = data[0];
            var description = data[1];
            var stringStartDateAndHour = $"{data[2]} {data[3]}";
            var stringEndDateAndHour = $"{data[4]} {data[5]}";

            DateTime parsedStartDate;
            var isStartDateValid = DateTime.TryParseExact(stringStartDateAndHour, DateFormat, CultureInfo.InvariantCulture,DateTimeStyles.None, out parsedStartDate);

            DateTime parseEndDate;
            var isEndDateValid = DateTime.TryParseExact(stringEndDateAndHour, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parseEndDate);

            var context = new TeamBuilderContext();

            if (!isStartDateValid || !isEndDateValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if(parsedStartDate > parseEndDate)
            {
                throw new ArgumentException("Start date should be before end date.");
            }

            User user = userSession.User;

            if (user == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            var creator = userSession.User;

            var newEvent = new Event()
            {
                CreatorId = creator.UserId,
                Name = eventName,
                Description = description,
                StartDate = parsedStartDate,
                EndDate = parseEndDate
            };
          
            context.Events.Add(newEvent);
            context.SaveChanges();

            return $"Event {eventName} was created successfully!";
        }
    }
}