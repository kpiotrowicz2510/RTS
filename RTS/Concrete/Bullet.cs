using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RTS.Abstract;

namespace RTS.Concrete
{
    class Bullet : GameObject
    {
        private string ownerString;
        public Bullet()
        {
            speed = 1000;
            properties["Damage"] = 20;
            properties["LiveTime"] = 100;
            size = new Point(3,3);
            properties["SightLine"] = 2;
            texture = Color.Yellow;
        }

        public override void Draw()
        {
            var rect = IManager.Instance.rect;
            rect.SetData(new[] {texture});
            IManager.Instance.SpriteBatch.Draw(rect, new Rectangle(new Point((int) Coords.X, (int) Coords.Y), new Point(2, 2)), texture);
        }
    }
}
