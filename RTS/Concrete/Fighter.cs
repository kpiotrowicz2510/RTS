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
        private int shootFrequency = 70;
        private int counterShoot = 0;
        private bool canShoot = true;
        public Fighter()
        {
            properties["Damage"] = 20;
            properties["Armor"] = 20;
            properties["SightLine"] = 160;
            texture = Color.Blue;
            speed = 300;
        }

        public override void Update()
        {
            var obj = Container.SelectGameObjectAtPoint((int) Coords.X, (int) Coords.Y, null, true, properties["SightLine"]);
            if (obj.Owner != Owner)
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
                    if (counterShoot == shootFrequency)
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
            bullet.targetCoords = obj.Coords+new Vector2(5,5);
        }
    }
}
