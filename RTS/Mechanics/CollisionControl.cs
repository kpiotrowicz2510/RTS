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
            actionList.Add(BulletAttack);
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
            foreach (var obj in manager.Container.ReturnGameObjectsOfType(typeof(Worker)))
            {
                var obj2 = manager.Container.CheckCollision(obj, typeof(GoldMine));
                if (obj2!=null)
                {
                    var mine = obj2 as GoldMine;
                    var worker = obj as Worker;
                    if (worker?.properties["CurrentWeight"] < worker?.MaxWeight)
                    {
                        worker.properties["CurrentWeight"] += mine.TakeGold();
                        worker.properties["MaxWeight"] = worker.MaxWeight;
                    }
                }
            }
        }

        private void HQWorker()
        {
            foreach (var obj in manager.Container.ReturnGameObjectsOfType(typeof(Worker)))
            {
                var obj2 = manager.Container.CheckCollision(obj, typeof(Headquarters));
                if (obj2 != null)
                {
                    var hq = obj2 as Headquarters;
                    var worker = obj as Worker;
                    if (worker?.properties["CurrentWeight"] > 0 && hq?.properties["CurrentLoad"] + worker.properties["CurrentWeight"] <= hq.properties["MaxLoad"])
                    {
                        hq.properties["CurrentLoad"] += worker.properties["CurrentWeight"];
                        hq.Owner.properties["Gold"] += worker.properties["CurrentWeight"];
                        worker.properties["CurrentWeight"] = 0;
                    }
                    if (hq?.properties["CurrentLoad"] == hq?.properties["MaxLoad"])
                    {
                        worker.actionControl.CancelActions();
                    }
                }
            }
        }

        private void BulletAttack()
        {
            foreach (var obj in manager.Container.ReturnGameObjectsOfType(typeof(Bullet)))
            {
                var obj2 = manager.Container.CheckBullet(obj);
                if (obj2 != null)
                {
                    obj2.properties["Health"] -= obj.properties["Damage"] - obj2.properties["Armor"]/10;
                    manager.Container.DeleteObject(obj);
                }
            }
        }
    }
}
