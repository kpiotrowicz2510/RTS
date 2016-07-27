using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RTS.Abstract;

namespace RTS.Concrete
{
    class GoldMine: GameObject
    {
        public GoldMine(int goldResource)
        {
            this.properties["CurrentGoldResource"] = goldResource;
            this.properties["GoldMineSpeed"] = 10;
            size = new Point(50,50);
            texture = Color.Gold;
        }

        public int TakeGold()
        {
            this.properties["CurrentGoldResource"] -= this.properties["GoldMineSpeed"];
            return this.properties["GoldMineSpeed"];
        }
    }
}
