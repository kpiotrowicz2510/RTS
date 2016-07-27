using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RTS.Abstract;

namespace RTS.Concrete
{
    class Headquarters:GameObject
    {
        public List<GameObject> HQLoad = new List<GameObject>();
        private bool Created = false;
        public Headquarters()
        {
            properties["MaxLoad"] = 1000;
            properties["SightLine"] = 50;
            properties["CurrentLoad"] = 0;
            size = new Point(50,30);
        }

        public void AddLoad(GameObject obj)
        {
            HQLoad.Add(obj);
        }

        public override void Update()
        {
            if (isSelected == true)
            {
                if (Created != false) return;
                Container.CreateNewObject(typeof(Worker), Coords + new Vector2(50, -50), Owner);
                Created = true;
            }
            else
            {
                Created = false;
            }
        }
    }
}
