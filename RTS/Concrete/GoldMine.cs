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
        public GoldMine(int goldResource)
        {
            this.properties["CurrentGoldResource"] = goldResource;
            this.properties["GoldMineSpeed"] = 10;
            properties["Destroyable"] = 0;
            size = new Point(50,50);
            texture = Color.Gold;
        }

        public override void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, SpriteFont font, Player currentPlayer)
        {
            var rect2 = new Texture2D(graphicsDevice, 1, 1);
            rect2.SetData(new[] { Color.Yellow });
            //int i = 0;
            //foreach (var prop in properties)
            //{
            //    spriteBatch.DrawString(font, prop.Key+":"+prop.Value, new Vector2((int)Coords.X, (int)Coords.Y - 100+i), Color.Black);
            //    i += 10;
            //}
            
                spriteBatch.Draw(rect2,
                    new Rectangle(new Point((int)(Coords.X - 5), (int)(Coords.Y - 20)),
                        new Point(properties["CurrentGoldResource"] / 20, 5)), Color.Yellow);
            
            base.Draw(spriteBatch, graphicsDevice, font, currentPlayer);
        }

        public int TakeGold()
        {
            this.properties["CurrentGoldResource"] -= this.properties["GoldMineSpeed"];
            return this.properties["GoldMineSpeed"];
        }
    }
}
