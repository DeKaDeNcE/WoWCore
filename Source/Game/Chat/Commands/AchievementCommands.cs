// Copyright (c) CypherCore <https://github.com/CypherCore> All rights reserved.
// Copyright (c) DeKaDeNcE <https://github.com/DeKaDeNcE/WoWCore> All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE. See LICENSE file in the project root for full license information.

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedType.Local

using Framework.Constants;
using Game.Entities;
using Game.DataStorage;

namespace Game.Chat.Commands
{
    [CommandGroup("achievement")]
    class AchievementCommand
    {
        [Command("add", CypherStrings.CommandAchievementAddHelp, RBACPermissions.CommandAchievementAdd)]
        static bool HandleAchievementAddCommand(CommandHandler handler, AchievementRecord achievementEntry)
        {
            Player target = handler.GetSelectedPlayer();
            if (!target)
            {
                handler.SendSysMessage(CypherStrings.NoCharSelected);
                return false;
            }

            target.CompletedAchievement(achievementEntry);

            return true;
        }
    }
}