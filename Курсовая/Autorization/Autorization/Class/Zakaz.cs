using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorization.Class
{
    class Zakaz
    {
        private int count;
        private double stoimost;
        private string name;
        public Zakaz(string name, double stoimost, int count)
        {
            this.name = name;
            this.stoimost = stoimost;
            this.count = count;
        }
        public override string ToString()
        {
            return name + " " + stoimost + " Pуб." + " x" + count;
        }
    }
}
