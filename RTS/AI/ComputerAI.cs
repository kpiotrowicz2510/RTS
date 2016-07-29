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
            TowerUp();
            Explore();
        }

        public void Explore()
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

            int i = 0;
            foreach (var fighter0 in fighters)
            {
                var fighter = fighter0 as Fighter;
         
                if (Vector2.Distance(fighter.Coords, hq.First().Coords) > 500&&fighter.CurrentJob!=Job.ATTACK)
                {
                    fighter.targetCoords = hq.First().Coords;
                    fighter.CurrentJob = Job.WALK;
                    continue;
                }
                if (fighter.CurrentJob == Job.DONE)
                {
                    Random r = new Random(14124);
                    int direction = r.Next(-2, 2);
                    fighter.CurrentJob = Job.WALK;
                    fighter.targetCoords = new Vector2(direction*500, direction*500);
                }

            }
        }
        public void TowerUp()
        {
            IEnumerable<GameObject> objects = from obj in IManager.Instance.Container.Objects
                                              where obj.Value.Owner == controlledPlayer
                                              select obj.Value;

            IEnumerable<GameObject> workers = from obj in objects
                                              where obj.GetType().Name == "Worker"
                                              select obj;
            IEnumerable<GameObject> towers = from obj in IManager.Instance.Container.Objects
                                            where obj.Value.GetType().Name == "Tower"
                                            select obj.Value;
            IEnumerable<GameObject> hq = from obj in objects
                                         where obj.GetType().Name == "Headquarters"
                                         select obj;

            if (controlledPlayer.properties["Gold"] >= 250)
            {
                var hqa = hq.First() as Headquarters;
                if (workers.Where(o => o.CurrentJob == Job.DONE).Any())
                {
                    if (towers.Count() < 3)
                    {
                        foreach (var worker in workers.Where(o => o.CurrentJob == Job.DONE))
                        {
                            if (worker.CurrentJob == Job.DONE)
                            {
                                worker.move(hq.First().Coords + new Vector2(-50 + towers.Count()*50, -50 + towers.Count() * 50), 300);
                                worker.AddBuild(new Tower());
                            }
                        }
                    }
                }
                else
                {
                    hqa.AddBuild(new Worker());
                    return;
                }
            }

           
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
            if (workers.Count() < 5)
            {
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
                    if (fighters.Count() < 5)
                    {
                        hqa.AddBuild(new Fighter());
                    }
                }
            
            int i = 0;
            foreach (var fighter0 in fighters)
            {
                var fighter = fighter0 as Fighter;
                if (fighters.Count() < 3)
                {
                    fighter.CurrentJob = Job.WALK;
                    fighter.actionControl.AddGoPoint(
                        (hq.First().Coords + new Vector2(-200 + (i)*100, (i++)*100)).ToPoint());
                    fighter.actionControl.AddGoPoint(hq.First().Coords.ToPoint());
                }
            }
        }
    }
}
