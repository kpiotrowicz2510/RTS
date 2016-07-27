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
            Owner.properties["Objects"]++;
            Objects[name] = obj;
        }

        public GameObject GetGameObject(string name)
        {
            return Objects[name];
        }
        public GameObject CheckCollision(string objName)
        {
            foreach (var obj in Objects)
            {

                if (Vector2.Distance(obj.Value.Coords, Objects[objName].Coords) < 10)
                {
                    if (objName == obj.Key || Objects[objName].PlatformCollision != false) continue;
                    Objects[objName].PlatformCollision = true;
                    return obj.Value;
                }
                else
                {
                    Objects[objName].PlatformCollision = false;
                }
            }
            return null;
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
