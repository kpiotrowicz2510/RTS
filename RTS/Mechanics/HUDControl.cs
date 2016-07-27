using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RTS.Concrete;

namespace RTS.Mechanics
{
    class HUDControl
    {
        public Player currentPlayer;
        public Vector2 posPoint;
        public Point size;
        public SpriteBatch spriteBatch;
        public SpriteFont spriteFont;
        public HUDControl()
        {
            
        }

        public void DrawHUD()
        {
            string info = "Player:" + currentPlayer.PlayerName;
            foreach (var prop in currentPlayer.properties)
            {
                info += "   " + prop.Key + ":" + prop.Value;
            }
            spriteBatch.DrawString(spriteFont, info,posPoint,Color.White);
        }
    }
}
