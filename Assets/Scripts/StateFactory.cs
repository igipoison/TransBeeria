using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public static class StateFactory
    {
        public static IState Create(int i)
        {
            if (i == 0)
              return new StateLeft();
            if (i == 1)
               return new StateRight();
            else
               return new StateDown();
        }
    }
}
