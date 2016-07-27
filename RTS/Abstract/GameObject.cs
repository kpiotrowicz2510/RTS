using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS.Abstract
{
    public enum Job { DONE, START, BUILD, DESTROY, ATTACK, WALK, MINE}
    public abstract class GameObject
    {
        public GameObject target;
        public int id;
        public Color texture;
        protected int speed;
        public bool PlatformCollision { get; set; }
        public int OwnerID { get; set; }
        public Dictionary<string,int> properties = new Dictionary<string, int>();
        public Job CurrentJob { get; set; }
        public Vector2 Coords { get; set; }
        public Vector2 targetCoords { get; set; }

        public virtual void move(Vector2 coords, int speed)
        {
            this.targetCoords = coords;
            this.speed = speed;
        }

        public virtual void Update()
        {
            if (Vector2.Distance(this.targetCoords, this.Coords) > 5)
            {
                float xMove = targetCoords.X - Coords.X;
                float yMove = targetCoords.Y - Coords.Y;

                Coords = new Vector2(xMove * speed / 10000 + Coords.X, yMove * speed / 10000 + Coords.Y);

            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            var rect = new Texture2D(graphicsDevice, 1, 1);
            rect.SetData(new[] { texture });
            spriteBatch.Draw(rect, new Rectangle(new Point((int) Coords.X,(int) Coords.Y),new Point(10,10)), texture);
        }
    }
}
