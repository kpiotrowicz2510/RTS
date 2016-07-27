using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS.Abstract
{
    public class ObjectContainer
    {
        private Dictionary<string,GameObject> Objects = new Dictionary<string, GameObject>();
        public GameObject SelectedGameObject { get; set; }
        public void AddObject(string name, GameObject obj, int OwnerID)
        {
            obj.OwnerID = OwnerID;
            Objects[name] = obj;
        }

        public GameObject GetGameObject(string name)
        {
            return Objects[name];
        }
        public bool CheckCollision(string objName)
        {
            foreach (var obj in Objects)
            {

                if (Vector2.Distance(obj.Value.Coords, Objects[objName].Coords) < 10)
                {
                    if (objName == obj.Key || Objects[objName].PlatformCollision != false) continue;
                    Objects[objName].PlatformCollision = true;
                    return true;
                }
                else
                {
                    Objects[objName].PlatformCollision = false;
                }
            }
            return false;
        }

        public void UpdateAll()
        {
            foreach (var obj in Objects)
            {
                obj.Value.Update();
            }
        }

        public void DrawAll(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            foreach (var obj in Objects)
            {
                obj.Value.Draw(spriteBatch,graphicsDevice);
            }
        }
    }
}
