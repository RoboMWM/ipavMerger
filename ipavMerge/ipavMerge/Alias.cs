using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipavMerge
{
    public class Alias
    {
        public Child player { get; set; }
        public List<Child> child { get; set; }

        public Alias(Child player, List<Child> child)
        {
            this.player = player;
            this.child = child;
        }
        public Alias(Child player)
        {
            this.player = player;
        }
    }
}
