using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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
        public List<ObjectBuild> HQBuildList = new List<ObjectBuild>();
        private ObjectBuild currentBuild;
        private bool Created = false;
        public Headquarters()
        {
            properties["MaxLoad"] = 1000;
            properties["SightLine"] = 50;
            properties["CurrentLoad"] = 0;
            size = new Point(50,30);
        }

        public void AddBuild(GameObject obj)
        {
            ObjectBuild build = new ObjectBuild()
            {
                gameObject=obj
            };
            if (Owner.properties["Gold"] >= obj.properties["BuildCost"])
            {
                Owner.properties["Gold"] -= obj.properties["BuildCost"];
                HQBuildList.Add(build);
            }
            else
            {
                //Error
            }
        }

        public override void Update()
        {
            if (currentBuild == null&&HQBuildList.Count>0)
            {
                currentBuild = HQBuildList[0];
                HQBuildList.Remove(currentBuild);
                currentBuild.buildState = 0;
                return;
            }
            if (currentBuild != null)
            {
                currentBuild.buildState += 1;
                if (currentBuild.buildState == currentBuild.maxBuild)
                {
                    Type type = currentBuild.gameObject.GetType();
                    Container.CreateNewObject(type, Container.SelectedGameObject.Coords + new Vector2(50, -50), Owner);
                    currentBuild = null;
                }
            }
        }
    }
}
