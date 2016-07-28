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
    class GoldMine: GameObject
    {
        public GoldMine()
        {
            this.properties["CurrentGoldResource"] = 1000;
            this.properties["GoldMineSpeed"] = 10;
            properties["Destroyable"] = 0;
            size = new Point(50,50);
            texture = Color.Gold;
        }

        public override void Draw()
        {
            var rect2 = new Texture2D(Container.GraphicsDevice, 1, 1);
            rect2.SetData(new[] { Color.Yellow });
            

            Container.SpriteBatch.Draw(rect2,
                    new Rectangle(new Point((int)(Coords.X - 5), (int)(Coords.Y - 20)),
                        new Point(properties["CurrentGoldResource"] / 20, 5)), Color.Yellow);
            
            base.Draw();
        }

        public int TakeGold()
        {
            this.properties["CurrentGoldResource"] -= this.properties["GoldMineSpeed"];
            return this.properties["GoldMineSpeed"];
        }
    }
}
