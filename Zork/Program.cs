using System;

namespace Zork
{
    class Program
    {
        
        private static string PlayerRoom
        {
            get
            {
                return Rooms[LateralPos];
            }
        }

        private static int LateralPos = (1);
       
        
        private static readonly string[] Rooms =
         {
            "Forest", "West of House", "Behind House", "Clearing", "Canyon View"
        };
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            //int i = 1;

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
                        /*
                        else if (command == Commands.EAST)
                        {
                            i++;
                        }
                        else if (command == Commands.WEST)
                        {
                            i--;
                        }
                        */
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
            
            bool canMove = false;

            switch (command)
            {

                case Commands.NORTH:
                    break;

                case Commands.SOUTH:
                    break;

                case Commands.WEST when LateralPos > 0 :
                    LateralPos--;
                    canMove = true;
                    break;

                case Commands.EAST when LateralPos < Rooms.GetLength(0) - 1 :
                    LateralPos++;
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
    }
}
