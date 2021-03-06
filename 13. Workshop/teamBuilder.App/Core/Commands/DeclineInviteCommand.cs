﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;

namespace TeamBuilder.App.Core.Commands
{
    public class DeclineInviteCommand
    {
        //<teamName>
        public static string Execute(string[] data)
        {
            Check.CheckLenght(1, data);

            //login first
            AuthenticationManager.Autorized();
            var currentUser = AuthenticationManager.GetCurrentUser();

            string teamName = data[0];
            bool isTeamExist = CommandHelper.IsTeamExisting(teamName);
            if (!isTeamExist)
            {
                string errorMsg = string.Format(Constants.ErrorMessages.TeamNotFound, teamName);
                throw new ArgumentException(errorMsg);
            }

            bool isInviteExist = CommandHelper.IsInviteExisting(teamName, currentUser);
            if (!isInviteExist)
            {
                string errorMsg = string.Format(Constants.ErrorMessages.InviteNotFound, teamName);
                throw new ArgumentException(errorMsg);
            }

            using (var db = new TeamBuilderContext())
            {
                var currentInvitation = db.Invitations
                    .FirstOrDefault(i => i.Team.Name == teamName && i.InvitedUserId == currentUser.UserId && i.IsActive);
                currentInvitation.IsActive = false;

                db.SaveChanges();
            }
            return $"Invite from {teamName} declined.";
        }
    }
}
