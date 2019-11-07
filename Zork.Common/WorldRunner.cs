using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Zork.Common
{
    public class WorldRunner : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
        public List<Room> Rooms { get; set; }

        public List<Item> Items { get; set; }


		public WorldRunner()
		{
			Rooms = new List<Room>();
			Items = new List<Item>();
		}

    }
}
