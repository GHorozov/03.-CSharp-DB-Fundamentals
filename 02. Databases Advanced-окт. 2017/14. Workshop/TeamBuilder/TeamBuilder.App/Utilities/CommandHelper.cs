namespace TeamBuilder.App.Utilities
{
    using System;
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            var context = new TeamBuilderContext();

            return context.Teams.Any(t => t.Name == teamName);
        }


        public static bool IsUserExisting(string username)
        {
            var context = new TeamBuilderContext();

            return context.Users.Any(u => u.Username == username);
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            var context = new TeamBuilderContext();

            return context.Invitations.Any(i => i.Team.Name == teamName && i.InvitedUserId == user.UserId && i.IsActive);
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            throw new NotImplementedException();
        }

        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            throw new NotImplementedException();
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            var context = new TeamBuilderContext();

            return context.Teams.Single(t => t.Name == teamName).UserTeams.Any(ut => ut.User.Username == username);
        }

        public static bool IsEventExisting(string eventName)
        {
            var context = new TeamBuilderContext();

            return context.Events.Any(e => e.Name == eventName);
        }

    }
}