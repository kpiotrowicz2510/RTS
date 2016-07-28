using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using RTS.Concrete;

namespace RTS.Abstract
{
    public class IManager
    {
        public GameManager Manager;
        public ObjectContainer Container;
        public SpriteFont SpriteFont;
        public SpriteBatch SpriteBatch;
        public GraphicsDevice GraphicsDevice;
        public Texture2D rect;

        private static IManager instance;

        private IManager() { }

        public static IManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IManager();
                }
                return instance;
            }
        }
    }
}
