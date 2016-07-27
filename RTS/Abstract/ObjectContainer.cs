﻿using System;
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
        private int objectCount=0;
        public void AddObject(string name, GameObject obj, Player Owner)
        {
            obj.OwnerID = Owner.PlayerID;
            obj.Owner = Owner;
            obj.Container = this;
            obj.name = name;
            Owner.properties["Objects"]++;
            Objects[name] = obj;
        }

        public GameObject GetGameObject(string name)
        {
            return Objects[name];
        }
        public GameObject CheckCollision(GameObject obj1, Type obj2=null)
        {
            foreach (var obj in Objects)
            {
                if(obj.Value==obj1) continue;
                if (obj.Value.GetType() == obj2||obj2==null)
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

        public GameObject SelectGameObjectAtPoint(int x, int y, Player owner, bool ret=false, int sightLine=40)
        {
            foreach (var obj in Objects)
            {
                Rectangle area = new Rectangle((int) obj.Value.Coords.X-sightLine/2, (int) obj.Value.Coords.Y-sightLine/2, sightLine,sightLine);
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
            obj.Container = this;
            obj.targetCoords = pos;
            obj.name = type.BaseType + "" + objectCount++;
            Objects.Add(obj.name,obj);
            return obj;
        }

        public void DeleteObject(GameObject obj)
        {
            Objects.Remove(obj.name);
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
            foreach (var obj in Objects.ToList())
            {
                obj.Value.Update();
            }
        }

        public void DrawAll(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, SpriteFont spriteFont, Player currentPlayer)
        {
            foreach (var obj in Objects)
            {
                obj.Value.Draw(spriteBatch,graphicsDevice,spriteFont,currentPlayer);
            }
        }
    }
}
