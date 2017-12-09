using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class InviteToTeamCommand
    {
        //<teamName> <username>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(2, data);

            //login first
            AuthenticationManager.Autorized();
            var currentUser = AuthenticationManager.GetCurrentUser();

            string teamName = data[0];
            string username = data[1];

            if(!CommandHelper.IsUserExisting(username) || !CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            using(var db = new TeamBuilderContext())
            {
                var team = db.Teams.SingleOrDefault(t => t.Name == teamName);
                var invitedUser = db.Users.SingleOrDefault(u => u.Username == username);

                if (CommandHelper.IsInviteExisting(teamName, invitedUser))
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
                }

                bool isUserCreatorOfTeam = CommandHelper.IsUserCreatorOfTeam(teamName, currentUser);
                bool isCurrentUserMemberOfTheTeam = CommandHelper.IsMemberOfTeam(teamName, currentUser.Username);
                bool isInvitedUserMemberOfTeam = CommandHelper.IsMemberOfTeam(teamName, invitedUser.Username);

                if((!isUserCreatorOfTeam && !isCurrentUserMemberOfTheTeam) && isInvitedUserMemberOfTeam)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
                }

                if (isUserCreatorOfTeam)
                {
                    var invitation = new Invitation
                    {
                        InvitedUserId = invitedUser.UserId,
                        TeamId = team.TeamId,
                        IsActive = false
                    };

                    var addInvitedUserToTeam = new UserTeam
                    {
                        TeamId = team.TeamId,
                        UserId = invitedUser.UserId
                    };

                    db.Invitations.Add(invitation);
                    db.UsersTeams.Add(addInvitedUserToTeam);
                    db.SaveChanges();
                }

                else
                {
                    var invitation = new Invitation
                    {
                        InvitedUserId = invitedUser.UserId,
                        TeamId = team.TeamId
                    };

                    db.Invitations.Add(invitation);
                    db.SaveChanges();
                }
            }

            return $"Team {teamName} invited {username}!";
        }
    }
}
