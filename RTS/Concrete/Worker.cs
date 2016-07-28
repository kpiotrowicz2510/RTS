using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RTS.Abstract;

namespace RTS.Concrete
{
    class Worker: GameObject
    {
        public int MaxWeight = 10;
        public int CurrentWeight = 0;

        public Worker()
        {
            properties["CurrentWeight"] = 0;
            properties["MaxWeight"] = 10;
            properties["BuildCost"] = 50;
            texture = Color.AliceBlue;
            CurrentJob = Job.DONE;
            speed = 150;
        }

        public override void Update()
        {
            if (properties["CurrentWeight"] != 0)
            {
                CurrentJob = Job.MINE;
            }
            else
            {
                if (CurrentJob != Job.WALK)
                {
                    CurrentJob = Job.DONE;
                }
            }
            base.Update();
        }
    }
}
