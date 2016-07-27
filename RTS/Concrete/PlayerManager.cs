using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RTS.Concrete
{
   public class PlayerManager
    {
       Dictionary<string,Player> players = new Dictionary<string, Player>();
       private int NextPlayerID = 0;
       private string CurrentPlayerID { get; set; }
       public PlayerManager()
       {
           
       }

       public Player GetCurrentPlayer(string id=null)
       {
           return id!=null ? players[id] : players[CurrentPlayerID];
       }

       public void AddNewPlayer(string name)
       {
           CurrentPlayerID = name;
           players[name] = new Player(name)
           {
               PlayerID = NextPlayerID++
           };
       }
    }
}
