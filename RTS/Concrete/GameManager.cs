using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RTS.Abstract;
using RTS.Mechanics;

namespace RTS.Concrete
{
    public class GameManager
    {
        public PlayerManager Players;
        public ClickableAreas ClickableAreas;

        public Headquarters Headquarters;
        public Dictionary<string, Texture2D> Textures;
        public GameManager()
        {
            ObjectContainer Container = new ObjectContainer();

            IManager.Instance.Container = Container;
            
            Players = new PlayerManager();
            ClickableAreas = new ClickableAreas();
            this.Initialize();
        }

        private void Initialize()
        {
            Players.AddNewPlayer("Computer");
            Players.AddNewPlayer("kris");

            IManager.Instance.Container.CreateNewObject(typeof(Worker), new Vector2(120, 50), Players.GetCurrentPlayer());
            IManager.Instance.Container.CreateNewObject(typeof(Worker), new Vector2(200, 0), Players.GetCurrentPlayer());

            IManager.Instance.Container.CreateNewObject(typeof(Fighter), new Vector2(600, 350), Players.GetCurrentPlayer("Computer"));
            IManager.Instance.Container.CreateNewObject(typeof(Fighter), new Vector2(650, 350), Players.GetCurrentPlayer("Computer"));
            IManager.Instance.Container.CreateNewObject(typeof(Fighter), new Vector2(550, 350), Players.GetCurrentPlayer("Computer"));
            IManager.Instance.Container.CreateNewObject(typeof(Headquarters), new Vector2(750, 350), Players.GetCurrentPlayer("Computer"));
            IManager.Instance.Container.CreateNewObject(typeof(GoldMine), new Vector2(950, 250), Players.GetCurrentPlayer("Computer"));

            IManager.Instance.Container.CreateNewObject(typeof(Fighter), new Vector2(200,200), Players.GetCurrentPlayer());

            var mine = new GoldMine()
            {
                Coords = new Vector2(440, 100)
            };

            IManager.Instance.Container.AddObject("Mine1", mine, Players.GetCurrentPlayer());
            

            var HQ = new Headquarters()
            {
                texture = Color.Brown,
                Coords = new Vector2(20, 400)
            };
            IManager.Instance.Container.AddObject("HQ", HQ, Players.GetCurrentPlayer());
            Headquarters = HQ;
        }

        public void UpdateOrganisms(GameObject obj=null)
        {
            if (obj == null)
            {
                IManager.Instance.Container.UpdateAll();
            }
            else
            {
                obj.Update();
            }
        }

        public void DrawOrganisms(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice, SpriteFont spriteFont, Player currentPlayer,GameObject obj = null )
        {
            if (obj == null)
            {
                IManager.Instance.Container.DrawAll();
            }
            else
            {
                obj.Draw();
            }
        }
    }
}
