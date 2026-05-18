using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootNBunny
{
    internal class Core
    {
        public static ShootNBunnyEntities1 Context = new ShootNBunnyEntities1();
        public static void Update()
        {
            Context = new ShootNBunnyEntities1();
        }
    }
}
