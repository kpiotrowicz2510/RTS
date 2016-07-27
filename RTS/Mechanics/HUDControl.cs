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
        public GraphicsDevice graphicsDevice;
        public GraphicsDeviceManager graphics;
        public Camera2D camera;
        public GameManager manager;
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
            var rect = new Texture2D(graphicsDevice, 1, 1);
            rect.SetData(new[] { Color.Black });
            Vector2 hudPosition = camera.Position + new Vector2(0, graphics.PreferredBackBufferHeight * 0.8f);
            spriteBatch.Draw(rect, camera.Position+new Vector2(0,0),new Rectangle(0,0,graphics.PreferredBackBufferWidth, 30),Color.Black);
            spriteBatch.DrawString(spriteFont, info, camera.Position + posPoint +new Vector2(10,5),Color.White);
            
            //Draw Hud background
            spriteBatch.Draw(rect, hudPosition, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, (int)(graphics.PreferredBackBufferHeight*0.5)), Color.Black);
            //draw minimap
            rect.SetData(new[] { Color.White });
            spriteBatch.Draw(rect, hudPosition+new Vector2(0,10), new Rectangle(0, 0, 200, 130), Color.White);
            //draw controls
            rect.SetData(new[] { Color.Blue });
            spriteBatch.Draw(rect, hudPosition + new Vector2(graphics.PreferredBackBufferWidth - 200, 10), new Rectangle(0, 0, 50, 50), Color.White);

            ClickableArea area1 = new ClickableArea()
            {
                area = new Rectangle((hudPosition + new Vector2(graphics.PreferredBackBufferWidth - 200, 10)).ToPoint(), new Point(50,50)),
                function = NewWorker
            };
            manager.ClickableAreas.AddArea(area1);

            rect.SetData(new[] { Color.Blue });
            spriteBatch.Draw(rect, hudPosition + new Vector2(graphics.PreferredBackBufferWidth - 130, 10), new Rectangle(0, 0, 50, 50), Color.White);
            ClickableArea area2 = new ClickableArea()
            {
                area = new Rectangle((hudPosition + new Vector2(graphics.PreferredBackBufferWidth - 130, 10)).ToPoint(), new Point(50, 50)),
                function = NewFighter
            };
            manager.ClickableAreas.AddArea(area2);
            //draw objectinfo
        }

        public void NewWorker()
        {
            manager.Container.CreateNewObject(typeof(Worker), manager.Container.SelectedGameObject.Coords + new Vector2(50, -50), manager.Players.GetCurrentPlayer());
        }
        public void NewFighter()
        {
            manager.Container.CreateNewObject(
                typeof(Fighter),
                manager.Container.SelectedGameObject.Coords + new Vector2(50, -50), manager.Players.GetCurrentPlayer());
        }
    }
}
