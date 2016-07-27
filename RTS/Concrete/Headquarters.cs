using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTS.Abstract;

namespace RTS.Concrete
{
    class Headquarters:GameObject
    {
        public List<GameObject> HQLoad = new List<GameObject>();
        public Headquarters()
        {
            properties["MaxLoad"] = 100;
            properties["CurrentLoad"] = 0;
        }

        public void AddLoad(GameObject obj)
        {
            HQLoad.Add(obj);
        }
    }
}
