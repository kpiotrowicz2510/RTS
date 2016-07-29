using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS.Concrete
{
    public class Player
    {
        public string PlayerName { get; set; }
        public int PlayerID { get; set; }
        public Dictionary<string,int> properties = new Dictionary<string, int>();
        public Player(string name)
        {
            PlayerName = name;
            properties["score"] = 0;
            properties["Fuel"] = 100;
            properties["Gold"] = 500;
            properties["Uranium"] = 100;
            properties["Objects"] = 0;
        }
    }
}
