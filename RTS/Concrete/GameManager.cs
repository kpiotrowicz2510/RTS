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
    public class GameManager
    {
        public ObjectContainer Container;
        public PlayerManager Players;

        public GameManager()
        {
            Container = new ObjectContainer();  
            Players = new PlayerManager();
            this.Initialize();
        }

        private void Initialize()
        {
            Players.AddNewPlayer("kris");
            var worker = new Worker
            {
                texture = Color.Aqua,
                Coords = new Vector2(0, 10),
                CurrentJob = Job.DONE
            };
            worker.move(new Vector2(120, 50), 50);
            
            Container.AddObject("Worker1",worker,Players.GetCurrentPlayer().PlayerID);
            Players.GetCurrentPlayer().properties["Objects"]++;

            var mine = new GoldMine(1000)
            {
                texture = Color.Beige,
                Coords = new Vector2(440, 100)
            };

            Container.AddObject("Mine1", mine, Players.GetCurrentPlayer().PlayerID);
            Players.GetCurrentPlayer().properties["Objects"]++;

        }

        public void UpdateOrganisms(GameObject obj=null)
        {
            if (obj == null)
            {
                Container.UpdateAll();
            }
            else
            {
                obj.Update();
            }
        }

        public void DrawOrganisms(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice, GameObject obj = null)
        {
            if (obj == null)
            {
                Container.DrawAll(spriteBatch, graphicsDevice);
            }
            else
            {
                obj.Draw(spriteBatch,graphicsDevice);
            }
        }
    }
}
