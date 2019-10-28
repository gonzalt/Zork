using System;
using System.Collections.Generic;
using System.Linq;

namespace Zork
{
    public  class CommandContext
    {

        public string CommandString { get; }

        public Command Command { get; }

        public CommandContext(string commandString, Command command)
        {
            CommandString = commandString;
            Command = command;
        }



    }
}
