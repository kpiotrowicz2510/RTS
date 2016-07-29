using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.Xna.Framework;
using RTS.Abstract;
using RTS.Concrete;

namespace RTS.Multiplayer
{
    class Uncoder
    {
        JavaScriptSerializer jsonx = new JavaScriptSerializer();
        public Uncoder()
        {
            
        }

        public GameObject Decoder(string data)
        {
            if (data == null)
            {
                return null;
            }
            GameObject obj=null;
            dynamic ob = jsonx.DeserializeObject(data);

            string type = ob["ObjType"];
            switch (type)
            {
                case "Headquarters":
                    obj = new Headquarters();
                    break;
                case "Worker":
                    obj = new Worker();
                    break;
                case "Fighter":
                    obj = new Fighter();
                    break;
                case "Tower":
                    obj = new Tower();
                    break;
                case "GoldMine":
                    obj = new GoldMine();
                    break;
            }
            obj.name = ob["name"];
            obj.Coords = new Vector2(Convert.ToInt32(ob["Coords"]["X"]), Convert.ToInt32(ob["Coords"]["Y"]));
            obj.Owner = IManager.Instance.Manager.Players.GetCurrentPlayer(ob["Owner"]["PlayerName"]);
            //obj.name = ob["name"];

            return obj;
        }
    }
}
