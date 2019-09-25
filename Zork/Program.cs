using System;
using System.Collections.Generic;

namespace Zork
{
    class Program
    {
        
        public static Room PlayerRoom
        {
            get
            {
                return Rooms[Position.VerticalPos, Position.LateralPos];
            }
        }

        private static ( int VerticalPos, int LateralPos ) Position = ( 1, 1 );


        private static readonly Room[,] Rooms =
        {
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };

        private static void InitializeRoomDescriptions()
        {
            Rooms[0, 0].Description = "You are on a rock-strewn trail.";                                                                                //Rocky Trail
            Rooms[0, 1].Description = "You are facing the south side of a white house. There is no door here, and all windows are barred.";             //South of House
            Rooms[0, 2].Description = "You are at the top of the Great anyon in its south wall.";                                                       //Canyon View

            Rooms[1, 0].Description = "This is a forest, with trees in all directions around you.";                                                     //Forest
            Rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door.";                                        //West of House
            Rooms[1, 2].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";     //Behind House

            Rooms[2, 0].Description = "This is a dimly lit forest, with large trees allaround. To the east, there appears to be sunlight.";             //Dense Woods
            Rooms[2, 1].Description = "You are facing the north side of a  white house. There is no door here, and all the windows are barred.";        //North of House
            Rooms[2, 2].Description = "You are in a clearing, with a forest surrounding you on the west and south.";                                    //Clearing
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            InitializeRoomDescriptions();
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
                        Console.WriteLine(PlayerRoom.Description);
                        outputString = "";
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
