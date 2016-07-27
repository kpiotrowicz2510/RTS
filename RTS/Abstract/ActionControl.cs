using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RTS.Abstract
{
    public class ActionControl
    {
        protected List<Point> GoList = new List<Point>();
        protected List<Point> DoneList= new List<Point>();
        private int index = 0;
        public ActionControl()
        {
            
        }
        public void AddGoPoint(Point point)
        {
            if (DoneList.IndexOf(point)<0)
            {
                DoneList.Add(point);
            }

        }

        public Point InvokeAction()
        {
            if (DoneList.Count > 0)
            {
                if (index == DoneList.Count) index = 0;
                Point returnPoint = DoneList[index++];
                return returnPoint;
            }

            return Point.Zero;
        }

        public void CancelActions()
        {
            index = 0;
            DoneList.Clear();
        }
    }
}
