using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RTS.Abstract;

namespace RTS.Concrete
{
    public class Fighter: GameObject
    {
        private int shootFrequency = 100;
        private int counterShoot = 0;
        private bool canShoot = true;
        public Fighter()
        {
            properties["Damage"] = 10;
            properties["Armor"] = 20;
            properties["SightLine"] = 60;
            properties["BuildCost"] = 150;
            texture = Color.Blue;
            speed = 300;
        }

        public override void Update()
        {
            Rectangle area = new Rectangle((int)Coords.X - properties["SightLine"], (int)Coords.Y - properties["SightLine"], size.X + properties["SightLine"]*2, size.Y + properties["SightLine"]*2);
            var obj = Container.SelectGameObjectAtArea(area, Owner);
            if (obj!=null&&obj.Owner != Owner)
            {
                if (canShoot)
                {
                    Attack(obj);
                    counterShoot = 0;
                    canShoot = false;
                }
                else
                {
                    counterShoot++;
                    if (counterShoot >= shootFrequency)
                    {
                        counterShoot = 0;
                        canShoot = true;
                    }
                }
            }
            base.Update();
        }

        public void Attack(GameObject obj)
        {
            var bullet = Container.CreateNewObject(typeof(Bullet), Coords, Owner);
            bullet.target = this;
            bullet.targetCoords = obj.Coords+new Vector2(2,2);
        }
    }
}
