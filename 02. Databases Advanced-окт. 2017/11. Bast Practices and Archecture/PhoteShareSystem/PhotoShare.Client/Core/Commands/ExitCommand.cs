namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ExitCommand
    {
        public static string Execute()
        {
            var message = "Bye-bye!";
            Console.WriteLine(message);
            Environment.Exit(0);

            return message;
        }
    }
}
