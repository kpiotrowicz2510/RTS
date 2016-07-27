using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTS.Abstract;

namespace RTS.Concrete
{
    class GoldMine: GameObject
    {
        public GoldMine(int goldResource)
        {
            this.properties["CurrentGoldResource"] = goldResource;
            this.properties["GoldMineSpeed"] = 10;
        }

        public int TakeGold()
        {
            this.properties["CurrentGoldResource"] -= this.properties["GoldMineSpeed"];
            return this.properties["GoldMineSpeed"];
        }
    }
}
