using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTS.Abstract;
using RTS.Concrete;

namespace RTS.AI
{
    class ComputerAI
    {
        public Player controlledPlayer;
        public GameManager GameManager;

        public ComputerAI()
        {
            
        }

        public void Update()
        {
            SearchForResources();
        }

        public void SearchForResources()
        {
            IEnumerable<GameObject> objects = from obj in IManager.Instance.Container.Objects
                                  where obj.Value.Owner == controlledPlayer
                select obj.Value;

            IEnumerable<GameObject> workers = from obj in objects
                where obj.GetType().Name == "Worker" 
                select obj;
            IEnumerable<GameObject> mines = from obj in objects
                                              where obj.GetType().Name == "GoldMine"
                                              select obj;
            IEnumerable<GameObject> hq = from obj in objects
                                            where obj.GetType().Name == "Headquarters"
                                            select obj;
            if (workers.Any() == false)
            {
                if (controlledPlayer.properties["Gold"] >= 50)
                {
                    var hqa = hq.First() as Headquarters;
                    if (hqa.HQBuildList.Count < 1)
                    {
                        hqa.AddBuild(new Worker());
                    }
                }
            }
            foreach (var worker in workers)
            {
                if (worker.CurrentJob == Job.DONE)
                {
                   worker.actionControl.AddGoPoint(mines.First().Coords.ToPoint());
                   worker.actionControl.AddGoPoint(hq.First().Coords.ToPoint());
                   worker.CurrentJob = Job.WALK;
                }
            }
        }
    }
}
