using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
               PlayerID = NextPlayerID++,
               startingPosition = new Vector2(20,300) + new Vector2((NextPlayerID-1)*500,0)
           };
       }
    }
}
