using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RTS.Concrete;

namespace RTS.Abstract
{
    public enum Job { DONE, START, BUILD, DESTROY, ATTACK, WALK, MINE}
    public abstract class GameObject
    {
        public GameObject target;

        public int id;
        public string name;
        public Color texture;
        protected int speed;
        
        public bool PlatformCollision { get; set; }
        public int OwnerID { get; set; }
        public Player Owner { get; set; }
        public Dictionary<string,int> properties = new Dictionary<string, int>();
        public Job CurrentJob { get; set; }
        public Vector2 Coords { get; set; }
        public Vector2 targetCoords { get; set; }
        public Point size;
        public ActionControl actionControl = new ActionControl();
        public bool isSelected { get; set; }
        public GameObject()
        {
            properties["Health"] = 100;
            properties["Damage"] = 0;
            properties["Armor"] = 10;
            properties["SightLine"] = 20;
            properties["BuildCost"] = 100;
            properties["LiveTime"] = -100;
            properties["Destroyable"] = 1;
            size = new Point(32,32);
            isSelected = false;
        }
        public virtual void move(Vector2 coords, int speed)
        {
            this.targetCoords = coords;
            this.speed = speed;
        }

        public virtual void Update()
        {
            if (Vector2.Distance(this.targetCoords, this.Coords) > 2)
            {
                float xMove = targetCoords.X - Coords.X;
                float yMove = targetCoords.Y - Coords.Y;

                Coords = new Vector2(xMove*speed/10000 + Coords.X, yMove*speed/10000 + Coords.Y);
            }
            else
            {
                Point targetPoint = actionControl.InvokeAction();
                if (targetPoint != Point.Zero)
                {
                    this.targetCoords = new Vector2(targetPoint.X, targetPoint.Y);
                }
            }
            if (properties["Health"] < 0)
            {
                IManager.Instance.Container.DeleteObject(this);
            }
            if (properties["LiveTime"]!=-100&&properties["LiveTime"]-- < 0)
            {
                IManager.Instance.Container.DeleteObject(this);
            }
        }

        public virtual void Draw()
        {
            var rect = IManager.Instance.rect;
            rect.SetData(new[] { texture });
            var rect2 = IManager.Instance.rect;
            rect2.SetData(new[] { Color.Green });
            var rect3 = IManager.Instance.rect;
            rect3.SetData(new[] { Color.Red });
            
            if (properties["Destroyable"] == 1)
            {
                IManager.Instance.SpriteBatch.Draw(rect2,
                    new Rectangle(new Point((int) (Coords.X - 5), (int) (Coords.Y - 10)),
                        new Point(properties["Health"]/5, 5)), Color.Green);
            }
            if (0 == 1)
            {
                rect.SetData(new[] { new Color(Color.Yellow, 10) });
                Rectangle area = new Rectangle((int) Coords.X - properties["SightLine"],
                    (int) Coords.Y - properties["SightLine"], size.X + properties["SightLine"]*2,
                    size.Y + properties["SightLine"]*2);
                IManager.Instance.SpriteBatch.Draw(rect, area, Color.Green);
            }
            if (isSelected)
            {
                IManager.Instance.SpriteBatch.Draw(rect, new Rectangle(new Point((int)(Coords.X-2), (int)(Coords.Y-2)), new Point(size.X+4, size.Y+4)), Color.Green);
            }
            if (Owner!= IManager.Instance.Manager.Players.GetCurrentPlayer())
            {
                IManager.Instance.SpriteBatch.Draw(rect3, new Rectangle(new Point((int)(Coords.X - 2), (int)(Coords.Y - 2)), new Point(size.X + 4, size.Y + 4)), Color.Red);
            }
            string name = GetType().Name;
            string job = CurrentJob.ToString();
            if (CurrentJob != Job.DONE&&CurrentJob!=Job.WALK)
            {
                IManager.Instance.SpriteBatch.Draw(IManager.Instance.Manager.Textures[this.GetType().Name+"_"+job], new Rectangle(new Point((int)Coords.X, (int)Coords.Y), size), Color.CornflowerBlue);
                return;
            }
            IManager.Instance.SpriteBatch.Draw(IManager.Instance.Manager.Textures[this.GetType().Name], new Rectangle(new Point((int) Coords.X,(int) Coords.Y),size), Color.CornflowerBlue);
        }
    }
}
