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

        public void Decoder(string data)
        {
            if (data == null)
            {
                return;
            }
            try
            {
                GameObject obj = null;
                dynamic obx = jsonx.DeserializeObject(data);
                foreach (
                    var VARIABLE in
                        IManager.Instance.Container.Objects.Where(
                            o => o.Value.Owner == IManager.Instance.Manager.Players.GetCurrentPlayer("Computer"))
                            .ToList())
                {
                    GameObject x;
                    IManager.Instance.Container.Objects.TryRemove(VARIABLE.Key, out x);
                }

                foreach (var ob0 in obx)
                {
                    dynamic ob = ob0.Value;
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
                    obj.Coords = new Vector2(Convert.ToInt32(ob["Coords"]["X"]), Convert.ToInt32(ob["Coords"]["Y"]));
                    //obj.Owner = IManager.Instance.Manager.Players.GetCurrentPlayer(ob["Owner"]["PlayerName"]);
                    obj.Owner = IManager.Instance.Manager.Players.GetCurrentPlayer("Computer");
                    dynamic x = ob["properties"];
                    Dictionary<string, int> prop = new Dictionary<string, int>();
                    foreach (var v in x)
                    {
                        string key = v.Key;
                        int value = v.Value;
                        prop.Add(key, value);
                    }
                    obj.properties = prop;
                    if (obj != null && ob["Owner"]["PlayerName"] != IManager.Instance.Manager.Players.GetCurrentPlayer().PlayerName)
                    {
                        IManager.Instance.Container.AddObject("BOT" + IManager.Instance.Container.Objects.Count(), obj,
                            IManager.Instance.Manager.Players.GetCurrentPlayer("Computer"));
                    }
                }

                //obj.name = ob["name"];

            }catch(Exception e) { }
        }
    }
}
