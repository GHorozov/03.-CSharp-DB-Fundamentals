namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Commands;
    using System;
    using System.Linq;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var command = commandParameters[0].ToLower();

            string result = null;

            switch (command)
            {
                case "registeruser":
                    result = RegisterUserCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "addtown":
                    result = AddTownCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "modifyuser":
                    result =  ModifyUserCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "deleteuser":
                    result = DeleteUser.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "addtag":
                    result = AddTagCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "createalbum":
                    result = CreateAlbumCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "addtagto":
                    result = AddTagToCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "addfriend":
                    result = AddFriendCommand.Execute(commandParameters.Skip(1).ToArray());
                        break;

                case "acceptfriend":
                    result = AcceptFriendCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "listfriends":
                    result = PrintFriendsListCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "sharealbum":
                    result = ShareAlbumCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "uploadpicture":
                    result = UploadPictureCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "login":
                    result = LoginCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "logout":
                    result = LogoutCommand.Execute(commandParameters.Skip(1).ToArray());
                    break;

                case "exit":
                    result = ExitCommand.Execute();
                    break;

                default:
                    throw new InvalidOperationException($"command {command} not valid!");
            }

            return result;
        }
    }
}
