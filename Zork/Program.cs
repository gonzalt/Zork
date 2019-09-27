﻿using System;
using System.Collections.Generic;
using System.IO;

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

        private static readonly Dictionary<string, Room> RoomMap;

        static Program()
        {
            RoomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                RoomMap[room.Name] = room;
            }
        }

        private static readonly Room[,] Rooms =
        {
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };

        private enum Fields
        {
            Name = 0,
            Description
        }

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }

        private static void InitializeRoomDescriptions(string roomsFilename)
        {
            const string fieldDelimiter = "##";
            const int expectedFieldCount = 2;

            string[] lines = File.ReadAllLines(roomsFilename);
            foreach (string line in lines)
            {
                string[] fields = line.Split(fieldDelimiter);
                if (fields.Length != expectedFieldCount)
                {
                    throw new InvalidDataException("Invalid record.");
                }

                string name = fields[(int)Fields.Name];
                string description = fields[(int)Fields.Description];

                    RoomMap[name].Description = description;
            }
          
            
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            const string defaultRoomsFilename = "Rooms.txt";
            string roomsFileName = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);
            InitializeRoomDescriptions(roomsFileName);
            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;

            while (command != Commands.QUIT)
            {
                Console.WriteLine(PlayerRoom);

                if (previousRoom != PlayerRoom)
                {
                    Console.WriteLine(PlayerRoom.Description);
                    previousRoom = PlayerRoom;
                }
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
