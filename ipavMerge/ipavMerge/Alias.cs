using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipavMerge
{
    public class Alias
    {
        public List<string> playerUuids { get; set; }

        //public Alias(List<string> player, List<Child> child)
        //{
        //    this.playerUuids = player;
        //    this.child = child;
        //}
        public Alias(List<string> player)
        {
            this.playerUuids = player;
        }
        public int uuidExist(string uuid)
        {
            int index = this.playerUuids.FindIndex(delegate (string str)
            { return str.Equals(uuid); });
            return index;
        }
    }
}
