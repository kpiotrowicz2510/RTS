using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Web.Script.Serialization;
using RTS.Concrete;

namespace RTS.Abstract
{
    public class ObjectContainer
    {
        public Dictionary<string,GameObject> Objects = new Dictionary<string, GameObject>();
        public GameObject SelectedGameObject { get; set; }
        
        private int objectCount=0;

        
        public void AddObject(string name, GameObject obj, Player Owner)
        {
            obj.OwnerID = Owner.PlayerID;
            obj.Owner = Owner;
            obj.name = name;
            Owner.properties["Objects"]++;
            Objects[name] = obj;
        }

        public GameObject GetGameObject(string name)
        {
            return Objects[name];
        }

        public GameObject CheckBullet(GameObject obj1)
        {
            foreach (var obj in Objects.ToList())
            {
                if (obj.Value.GetType() == obj1.GetType()) continue;
                Rectangle area = new Rectangle((int) obj.Value.Coords.X,
                    (int) obj.Value.Coords.Y,
                    obj.Value.size.X,
                    obj.Value.size.Y);
                if (area.Contains(obj1.Coords.X, obj1.Coords.Y)&&obj.Value.Owner!=obj1.Owner)
                {
                    return obj.Value;
                }
                
            }
            return null;
        }

        public GameObject CheckCollision(GameObject obj1, Type obj2=null)
        {
            foreach (var obj in Objects.ToList())
            {
                if(obj.Value==obj1) continue;
                if (obj.Value.GetType() != obj2 && obj2 != null) continue;
                Rectangle area = new Rectangle((int)obj.Value.Coords.X - obj.Value.properties["SightLine"], (int)obj.Value.Coords.Y - obj.Value.properties["SightLine"], obj.Value.size.X + obj.Value.properties["SightLine"], obj.Value.size.Y + obj.Value.properties["SightLine"]);
                if (area.Contains(obj1.Coords.X, obj1.Coords.Y))
                {
                    //    if (Vector2.Distance(obj.Value.Coords, obj1.Coords) < obj1.properties["SightLine"])
                    //{
                    if (obj1.PlatformCollision != false) continue;
                    obj1.PlatformCollision = true;
                    return obj.Value;
                }
                else
                {
                    obj1.PlatformCollision = false;
                }
            }
            return null;
        }

        public GameObject SelectGameObjectAtAreaToAttack(Rectangle area, Player player)
        {
            foreach (var obj in Objects.Where(o=>o.Value.Owner!=player).ToList())
            {
                if (area.Contains(new Rectangle((int) obj.Value.Coords.X, (int)obj.Value.Coords.Y, obj.Value.size.X,
                        obj.Value.size.Y)))
                {
                    return obj.Value;
                }
            }
            return null;
        }

        public GameObject SelectGameObjectAtArea(Rectangle area, Player player)
        {
            foreach (var obj in Objects.Where(o => o.Value.Owner == player).ToList())
            {
                if (area.Contains(new Rectangle((int)obj.Value.Coords.X, (int)obj.Value.Coords.Y, obj.Value.size.X,
                        obj.Value.size.Y)))
                {
                    return obj.Value;
                }
            }
            return null;
        }

        //public override string ToString()
        //{
        //    //JavaScriptSerializer jsonx  =new JavaScriptSerializer();
        //    //string json = "";
            
        //    //    json = new JavaScriptSerializer().Serialize(this);
        //    //    Debug.WriteLine(json);
            
        //    //dynamic d = jsonx?.Deserialize<G>(json);
        //    //return "";
        //}

        public GameObject SelectGameObjectAtPoint(int x, int y, Player owner, bool ret=false, int sightLine=40)
        {
            foreach (var obj in Objects.ToList())
            {
                Rectangle area = new Rectangle((int) obj.Value.Coords.X-sightLine, (int) obj.Value.Coords.Y-sightLine, obj.Value.size.X+sightLine,obj.Value.size.Y+sightLine);
                if (area.Contains(x, y))
                {
                    if (obj.Value.Owner == owner)
                    {
                        SelectedGameObject = obj.Value;
                        obj.Value.isSelected = true;
                    }
                    if (ret) return obj.Value;
                }
                else
                {
                    if (obj.Value.Owner == owner)
                    {
                        obj.Value.isSelected = false;
                    }
                }
            }
            return null;
        }

        public GameObject CreateNewObject(Type type, Vector2 pos, Player owner)
        {
            GameObject obj = Activator.CreateInstance(type) as GameObject;
            obj.Coords = pos;
            obj.Owner = owner;
            obj.targetCoords = pos;
            obj.name = type.BaseType + "" + objectCount++;
            Objects.Add(obj.name,obj);
            owner.properties["Objects"]++;
            return obj;
        }

        public void DeleteObject(GameObject obj)
        {
            Objects.Remove(obj.name);
            obj = null;
            GC.Collect();
        }
        public IEnumerable<GameObject> ReturnGameObjectsOfType(Type type)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (var obj in Objects.ToList())
            {
                if (obj.Value.GetType() == type)
                {
                    list.Add(obj.Value);
                }
            }
            return list;
        }

        public void UpdateAll()
        {
            foreach (var obj in Objects.ToList())
            {
                obj.Value.Update();
            }
        }

        public void DrawAll()
        {
            foreach (var obj in Objects.ToList())
            {
                obj.Value.Draw();
            }
        }
    }
}
