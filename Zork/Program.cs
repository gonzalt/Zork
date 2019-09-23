using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        
        private static string PlayerRoom
        {
            get
            {
                return Rooms[Position.VerticalPos, Position.LateralPos];
            }
        }

        private static ( int VerticalPos, int LateralPos ) Position = ( 1, 1 );


        private static readonly string[,] Rooms =
        {
            { "Rocky Trail", "South of House", "Canyon View" },
            { "Forest", "West of House", "Behind House" },
            { "Dense Woods", "North of House", "Clearing" }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(PlayerRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.WEST:
                    case Commands.EAST:
                        if (Move(command) == false)
                        {
                            outputString = "The way is shut! \n";
                        }                        
                        else
                        {
                            outputString = $"You moved {command}.";
                        }
                        break;

                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }

        }

        
        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsADirection(command), "Invalid direction.");

            bool canMove = false;

            switch (command)
            {

                case Commands.NORTH when ( Position.VerticalPos < Rooms.GetLength(0) - 1 ) :
                    Position.VerticalPos++;
                    canMove = true;
                    break;

                case Commands.SOUTH when ( Position.VerticalPos > 0 ) :
                    Position.VerticalPos--;
                    canMove = true;
                    break;

                case Commands.WEST when ( Position.LateralPos > 0 ):
                    Position.LateralPos--;
                    canMove = true;
                    break;

                case Commands.EAST when ( Position.LateralPos < Rooms.GetLength(1) - 1 ) :
                    Position.LateralPos++;
                    canMove = true;
                    break;

                default:
                    canMove = false;
                    break;
            }
            
            return canMove;
           
        }
          
        



        private static Commands ToCommand(string commandString)
        {

            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        }

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static bool IsADirection(Commands command) => Directions.Contains(command);




    }
}
