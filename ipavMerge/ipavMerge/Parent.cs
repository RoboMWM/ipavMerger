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

        public string uuidFind(string uuid)
        {
            String oi = this.child[0].name;
            String str = this.child[0].name;
            for (int i = 0; i < this.child.Count; i++)
            {
                if (!this.child[i].name.Equals(oi))
                {
                    oi = child[i].name;
                    str += (", " + oi);
                }
            }
            return str;
        }
    }
}
