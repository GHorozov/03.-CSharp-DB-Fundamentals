namespace TeamBuilder.App.Core.Commands.Interfaces
{   
    internal interface ICommand
    {
        string Execute(IUserSession userSession, params string[] data);
    }
}