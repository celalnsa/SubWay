using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubWay
{
    class Station
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Name { get; private set; }
        public List<int> Lines { get;set; }
        public Station(int x, int y, string name, int [] lines)
        {
            Lines=new List<int>();
            X = x;
            Y = y;
            Name = name;
            foreach (int line in lines)
            {
                Lines.Add(line);
            }
        }
        public string PositionToString()
        {
            string str="";
            str = str + "(" + X.ToString() + "," + Y.ToString() + ")";
            return str;
        }
        public string LinesToString()
        {
            string s="";
            foreach (int line in Lines) s +=(line.ToString()+",");
            if(s.Length>0)s = s.Remove(s.Length-1);
            return s;
        }
    }
}
