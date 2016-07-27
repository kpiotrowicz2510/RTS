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
    }
}
