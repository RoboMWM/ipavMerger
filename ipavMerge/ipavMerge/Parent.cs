using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipavMerge
{
    class Parent
    {
        public string parent { get; set; }
        public List<Child> child { get; set; }

        public Parent(string parent, List<Child> child)
        {
            this.parent = parent;
            this.child = child;
        }
    }
}
