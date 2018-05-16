namespace TeamBuilder.App
{
    using System;
    using TeamBuilder.App.Core;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
}
