using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipavMerge
{
    class Parent
    {
        private string parent { get; set; }
        private List<Child> child { get; set; }

        public Parent(string parent, List<Child> child)
        {
            this.parent = parent;
            child = this.child;
        }
    }
}
