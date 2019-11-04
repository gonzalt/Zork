using System;
using System.Collections.Generic;
using System.Text;

namespace Zork.Common
{
    public class PlayerStats
    {
        public string Name { get; set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public List<Item> Inventory { get; set; }

    }
}
