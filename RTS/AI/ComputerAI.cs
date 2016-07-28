using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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
            Patrol();
        }

        public void SearchForResources()
        {
            IEnumerable<GameObject> objects = from obj in IManager.Instance.Container.Objects
                where obj.Value.Owner == controlledPlayer
                select obj.Value;

            IEnumerable<GameObject> workers = from obj in objects
                where obj.GetType().Name == "Worker"
                select obj;
            IEnumerable<GameObject> mines = from obj in IManager.Instance.Container.Objects
                            where obj.Value.GetType().Name == "GoldMine"
                select obj.Value;
            IEnumerable<GameObject> hq = from obj in objects
                where obj.GetType().Name == "Headquarters"
                select obj;
            
                if (controlledPlayer.properties["Gold"] >= 50)
                {
                    var hqa = hq.First() as Headquarters;
                    if (workers.Count() < 5)
                    {
                        hqa.AddBuild(new Worker());
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

        public void Patrol()
        {
            IEnumerable<GameObject> objects = from obj in IManager.Instance.Container.Objects
                where obj.Value.Owner == controlledPlayer
                select obj.Value;

            IEnumerable<GameObject> fighters = from obj in objects
                where obj.GetType().Name == "Fighter"
                select obj;
            IEnumerable<GameObject> mines = from obj in objects
                                            where obj.GetType().Name == "GoldMine"
                                            select obj;
            IEnumerable<GameObject> hq = from obj in objects
                                         where obj.GetType().Name == "Headquarters"
                                         select obj;
            
                if (controlledPlayer.properties["Gold"] >= 150)
                {
                    var hqa = hq.First() as Headquarters;
                    if (fighters.Count() < 3)
                    {
                        hqa.AddBuild(new Fighter());
                    }
                }
            
            int i = 0;
            foreach (var fighter0 in fighters)
            {
                var fighter = fighter0 as Fighter;
                fighter.CurrentJob = Job.WALK;
                fighter.actionControl.AddGoPoint((hq.First().Coords + new Vector2(-200+(i)*100,(i++)*100)).ToPoint());
                fighter.actionControl.AddGoPoint(hq.First().Coords.ToPoint());
            }
        }
    }
}
