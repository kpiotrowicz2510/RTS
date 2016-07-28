using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS.Abstract
{
   public   class ClassContainer
    {
        private Dictionary<Type,object> list;

        public ClassContainer()
        {
         list=new Dictionary<Type, object>();   
        }

        public object ReturnSingleton(Type param)
        {
            try
            {
                return list[param];
            }
            catch (KeyNotFoundException e)
            {
                list[param] = Activator.CreateInstance(param);
                return list[param];
            }
        }
    }
}
