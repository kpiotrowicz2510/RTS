using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RTS.Abstract;
using RTS.Concrete;

namespace RTS.Mechanics
{
    class HUDControl
    {
        public Player currentPlayer;
        public Vector2 posPoint;
        public Point size;
        public GraphicsDeviceManager graphics;
        public Camera2D camera;
        public GameManager manager;
        public Vector2 hudPosition;
        private bool redraw = true;
        private Texture2D rect;
        
        public void init()
        {
            hudPosition = camera.Position + new Vector2(0, graphics.PreferredBackBufferHeight * 0.8f);
            rect = IManager.Instance.rect;
        }

        public void RedrawHud()
        {
            redraw = true;
        }
        public void DrawHUD()
        {
            string info = "Player:" + currentPlayer.PlayerName;
            foreach (var prop in currentPlayer.properties)
            {
                info += "   " + prop.Key + ":" + prop.Value;
            }
            
            rect.SetData(new[] { Color.Black });
            hudPosition = camera.Position + new Vector2(0, graphics.PreferredBackBufferHeight * 0.8f);
            IManager.Instance.SpriteBatch.Draw(rect, camera.Position+new Vector2(0,0),new Rectangle(0,0,graphics.PreferredBackBufferWidth, 30),Color.Black);
            IManager.Instance.SpriteBatch.DrawString(IManager.Instance.SpriteFont, info, camera.Position + posPoint +new Vector2(10,5),Color.White);

            //Draw Hud background
            IManager.Instance.SpriteBatch.Draw(rect, hudPosition, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, (int)(graphics.PreferredBackBufferHeight*0.5)), Color.Black);
            //draw minimap
            rect.SetData(new[] { Color.White });
            IManager.Instance.SpriteBatch.Draw(rect, hudPosition+new Vector2(0,10), new Rectangle(0, 0, 200, 130), Color.White);
            //draw controls
            Texture2D t1, t2;
            Action t1Action, t2Action;
            t1Action = NewWorker;
            t2Action = NewFighter;
            t1 = rect;
            t2 = rect;
            if (IManager.Instance.Container.SelectedGameObject?.GetType() == typeof(Headquarters))
            {
                t1 = manager.Textures["Worker"];
                t2 = manager.Textures["Fighter"];
                t1Action = NewWorker;
                t2Action = NewFighter;
            }
            if (IManager.Instance.Container.SelectedGameObject?.GetType() == typeof(Worker))
            {
                t1 = manager.Textures["Tower"];
                t1Action = NewTower;
                redraw = true;
                //t2 = manager.Textures["Fighter"];
            }

            rect.SetData(new[] { Color.Blue });
            IManager.Instance.SpriteBatch.Draw(t1, hudPosition + new Vector2(graphics.PreferredBackBufferWidth - 200, 10), new Rectangle(0, 0, 50, 50), Color.White);


            rect.SetData(new[] { Color.Blue });
            IManager.Instance.SpriteBatch.Draw(t2, hudPosition + new Vector2(graphics.PreferredBackBufferWidth - 130, 10), new Rectangle(0, 0, 50, 50), Color.White);

            //draw objectinfo
            int i = 0;
            if (IManager.Instance.Container.SelectedGameObject == null) return;
            IManager.Instance.SpriteBatch.DrawString(IManager.Instance.SpriteFont, "" + IManager.Instance.Container.SelectedGameObject.GetType().Name,
                       hudPosition + new Vector2(graphics.PreferredBackBufferWidth / 2 - 170, 5) + new Vector2(0, i),
                       Color.White);
            t1 = manager.Textures[IManager.Instance.Container.SelectedGameObject.GetType().Name];
            IManager.Instance.SpriteBatch.Draw(t1, hudPosition + new Vector2(graphics.PreferredBackBufferWidth / 2 - 175, 5) + new Vector2(0, 20), new Rectangle(0, 0, 64, 64), Color.White);
            foreach (var prop in IManager.Instance.Container.SelectedGameObject.properties)
            {
                IManager.Instance.SpriteBatch.DrawString(IManager.Instance.SpriteFont, prop.Key + ":" + prop.Value,
                    hudPosition + new Vector2(graphics.PreferredBackBufferWidth / 2, 5) + new Vector2(0, i),
                    Color.White);
                i += 15;
            }

            if (redraw == false)
            {
                return;
            }
            redraw = false;

            manager.ClickableAreas.RemoveAreas();
            ClickableArea area1 = new ClickableArea()
            {
                area = new Rectangle((hudPosition + new Vector2(graphics.PreferredBackBufferWidth - 200, 10)).ToPoint(), new Point(50, 50)),
                function = t1Action
            };
            manager.ClickableAreas.AddArea(area1);

            ClickableArea area2 = new ClickableArea()
            {
                area = new Rectangle((hudPosition + new Vector2(graphics.PreferredBackBufferWidth - 130, 10)).ToPoint(), new Point(50, 50)),
                function = t2Action
            };
            manager.ClickableAreas.AddArea(area2);

            
            
        }

        private void NewTower()
        {
            if (IManager.Instance.Container.SelectedGameObject.GetType() == typeof(Worker))
                IManager.Instance.Container.SelectedGameObject.AddBuild(new Tower());
        }

        public void NewWorker()
        {
            if(IManager.Instance.Container.SelectedGameObject.GetType()==typeof(Headquarters)) manager.Headquarters.AddBuild(new Worker());
        }
        public void NewFighter()
        {
            if (IManager.Instance.Container.SelectedGameObject.GetType() == typeof(Headquarters)) manager.Headquarters.AddBuild(new Fighter());
        }
    }
}
