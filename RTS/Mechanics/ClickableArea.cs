using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RTS.Mechanics
{
    public class ClickableArea
    {
        public Rectangle area;
        public Action function;

        public ClickableArea()
        {
            
        }
    }

    public class ClickableAreas
    {
        List<ClickableArea> list = new List<ClickableArea>();

        public ClickableAreas()
        {
            
        }

        public void AddArea(ClickableArea area)
        {
            list.Add(area);
        }

        public void CheckAreas(Point mousePoint)
        {
            foreach (var obj in list)
            {
                if (obj.area.Contains(mousePoint))
                {
                    obj.function?.Invoke();
                }
            }
        }
    }
}
