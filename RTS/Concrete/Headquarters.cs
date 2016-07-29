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
    public class ObjectBuild
    {
        public GameObject gameObject;
        public int buildState = 0;
        public int maxBuild = 100;
    }
    public class Headquarters:GameObject
    {
        private bool Created = false;
        public Headquarters()
        {
            properties["MaxLoad"] = 100000;
            properties["SightLine"] = 50;
            properties["CurrentLoad"] = 0;
            size = new Point(50,30);
        }

        

        public override void Draw()
        {
            var rect2 = IManager.Instance.rect;
            rect2.SetData(new[] { Color.Blue });
            
            if (currentBuild != null)
            {
                IManager.Instance.SpriteBatch.Draw(rect2,
                    new Rectangle(new Point((int) (Coords.X - 5), (int) (Coords.Y - 20)),
                        new Point(currentBuild.buildState/2, 5)), Color.Blue);
            }
            base.Draw();
        }

        public override void Update()
        {
            if (currentBuild == null&&BuildList.Count>0)
            {
                currentBuild = BuildList[0];
                if (properties["CurrentLoad"] > 0)
                {
                    properties["CurrentLoad"] -= BuildList[0].gameObject.properties["BuildCost"];
                }
                BuildList.Remove(currentBuild);
                currentBuild.buildState = 0;
                return;
            }
            if (currentBuild != null)
            {
                currentBuild.buildState += 1;
                if (currentBuild.buildState == currentBuild.maxBuild)
                {
                    Type type = currentBuild.gameObject.GetType();
                    IManager.Instance.Container.CreateNewObject(type, Coords + new Vector2(50, -50), Owner);
                    currentBuild = null;
                }
            }
            base.Update();
        }
    }
}
