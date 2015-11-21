using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ipavMerge
{
    public class Child
    {
        private string name { get; set; }
        private string uuid { get; set; }
        private string ip { get; set; }
        private string dateTimeStamp { get; set; }
        private string idc { get; set; } //kek
        private string city { get; set; }
        private string state { get; set; } //'merica
        private string country { get; set; }

        //public void AddChild(List<Child> child)
        //{
        //    child.Add(child);
        //}
        public Child(string name, string uuid, string ip, string dateTimeStamp, string idc, string city, string state, string country)
        {
            this.name = name;
            this.uuid = uuid;
            this.dateTimeStamp = dateTimeStamp;
            this.ip = ip;
            this.idc = idc;
            this.city = city;
            this.state = state;
            this.country = country;
        }
    }
}
