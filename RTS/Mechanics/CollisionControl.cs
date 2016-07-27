using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTS.Abstract;
using RTS.Concrete;

namespace RTS.Mechanics
{
    class CollisionControl
    {
        public List<GameObject> list = new List<GameObject>();
        private List<Action> actionList = new List<Action>();
        private Concrete.GameManager manager = null;
        public CollisionControl(Concrete.GameManager man)
        {
            manager = man;
            actionList.Add(MiningWorker);
            actionList.Add(HQWorker);
        }

        public void InvokeActions()
        {
            foreach (var action in actionList)
            {
                action.Invoke();
            }
        }
        private void MiningWorker()
        {
            if (manager.Container.CheckCollision("Worker1") is GoldMine)
            {
                var mine = manager.Container.GetGameObject("Mine1") as GoldMine;
                var worker = manager.Container.GetGameObject("Worker1") as Worker;
                if (worker?.properties["CurrentWeight"] < worker?.MaxWeight)
                {
                    worker.properties["CurrentWeight"] += mine.TakeGold();
                    worker.properties["MaxWeight"] = worker.MaxWeight;
                }
            }
        }

        private void HQWorker()
        {
            if (manager.Container.CheckCollision("Worker1") is Headquarters)
            {
                var hq = manager.Container.GetGameObject("HQ") as Headquarters;
                var worker = manager.Container.GetGameObject("Worker1") as Worker;
                if (worker?.properties["CurrentWeight"] > 0&&hq?.properties["CurrentLoad"] + worker.properties["CurrentWeight"] < hq.properties["MaxLoad"])
                {
                    hq.properties["CurrentLoad"] += worker.properties["CurrentWeight"];
                    worker.properties["CurrentWeight"] = 0;
                    
                }
            }
        }
    }
}
