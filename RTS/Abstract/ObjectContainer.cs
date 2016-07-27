using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RTS.Concrete;

namespace RTS.Abstract
{
    public class ObjectContainer
    {
        private Dictionary<string,GameObject> Objects = new Dictionary<string, GameObject>();
        public GameObject SelectedGameObject { get; set; }
        public void AddObject(string name, GameObject obj, Player Owner)
        {
            obj.OwnerID = Owner.PlayerID;
            obj.Owner = Owner;
            Owner.properties["Objects"]++;
            Objects[name] = obj;
        }

        public GameObject GetGameObject(string name)
        {
            return Objects[name];
        }
        public GameObject CheckCollision(GameObject obj1, Type obj2)
        {
            foreach (var obj in Objects)
            {
                if (obj.Value.GetType() == obj2)
                {
                    if (Vector2.Distance(obj.Value.Coords, obj1.Coords) < obj1.properties["SightLine"])
                    {
                        if (obj1.PlatformCollision != false) continue;
                        obj1.PlatformCollision = true;
                        return obj.Value;
                    }
                    else
                    {
                        obj1.PlatformCollision = false;
                    }
                }
            }
            return null;
        }

        public void SelectGameObjectAtPoint(int x, int y)
        {
            foreach (var obj in Objects)
            {
                Rectangle area = new Rectangle((int) obj.Value.Coords.X, (int) obj.Value.Coords.Y, 20,20);
                if (area.Contains(x, y))
                {
                    SelectedGameObject = obj.Value;
                    obj.Value.isSelected = true;
                }
                else
                {
                    obj.Value.isSelected = false;
                }
            }
        }

        public IEnumerable<GameObject> ReturnGameObjectsOfType(Type type)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (var obj in Objects)
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
            foreach (var obj in Objects)
            {
                obj.Value.Update();
            }
        }

        public void DrawAll(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, SpriteFont spriteFont)
        {
            foreach (var obj in Objects)
            {
                obj.Value.Draw(spriteBatch,graphicsDevice,spriteFont);
            }
        }
    }
}
