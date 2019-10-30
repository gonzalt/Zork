using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    [CommandClass]
    public static class QuitCommand
    {
        [Command("WUIT", new string[] {  "QUIT", "Q", "GOODBYE", "BYE" })]
        public static void Quit(Game game, CommandContext commandContext)
        {
            if (game.ConfirmAction("Are you aure you want to quit? "))
            {
                game.Quit();
            }
        }
    }
}
