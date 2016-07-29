using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

            string type = ob["ObjType"].ToString();
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
            obj.Coords = new Vector2(Convert.ToInt32(ob["Coords"]["X"]), Convert.ToInt32(ob["Coords"]["Y"])) + new Vector2(200,200);
            obj.Owner = IManager.Instance.Manager.Players.GetCurrentPlayer(ob["Owner"]["PlayerName"]);
            dynamic x = ob["properties"];
            Dictionary<string, int> prop = new Dictionary<string, int>();
            foreach (var v in x)
            {
                string key = v.Key; 
                int value = v.Value;
                prop.Add(key,value);
            }
            obj.properties = prop;
            //obj.name = ob["name"];

            return obj;
        }
    }
}
