using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubWay
{
    class Line
    {
        public int Type{get;private set;}
        public int Extral { get; private set; }
        public string ColorName { get; set; }
        public List<int> Stations{ get;set; }
        public List<int>  ChangeStations{get ;set;}
        public Line(int type,int extral,string color,int [] stations,int [] changestations)
        {
            Type = type;
            Extral = extral;
            ColorName = color;
            Stations=new List<int>();
            foreach(int station in stations)
            {
                Stations.Add(station);
            }
            ChangeStations=new List<int>();
            foreach(int station in changestations)
            {
                ChangeStations.Add(station);
            }
        }
        public string StationsToString()
        {
            string s = "";
            foreach (int station in Stations) s += (station.ToString() + ",");
            if (s.Length > 0) s = s.Remove(s.Length - 1);
            return s;
        }
        public string ChangeStationsToString()
        {
            string s = "";
            foreach (int station in ChangeStations) s += (station.ToString() + ",");
            if (s.Length > 0) s = s.Remove(s.Length - 1);
            return s;
        }
        public int Distance(int a,int b)
        {
            int statime = 0, xtation = 0, aed = 0, bed = 0,distance = 0;
            if(a==b)return distance;
            switch (Type)
            {
                case 1:
                    foreach (int station in Stations)
                    {
                        if (aed == 1 && bed == 1) break;
                        if (aed ==1|| bed == 1) distance++;
                        if (station == a) aed =1;
                        if (station == b) bed =1;
                    } break;
                case 2:
                    foreach (int station in Stations)
                    {
                        if (aed == 1 && bed == 1) break;
                        if (aed == 1 || bed == 1)
                        {
                            distance++;
                            if (xtation == 1) statime++;
                        }
                        if (station == a) aed=1;
                        if (station == b) bed=1;
                        if (station == Extral) xtation ++;
                    } if(xtation==2)distance-=statime;break;
                case 3:
                    foreach (int station in Stations) 
                    {
                        if (aed == 1 && bed == 1) break;
                        if (aed == 1 || bed == 1) distance++;
                        if (station == a) aed = 1;
                        if (station == b) bed = 1;
                    }if(Stations.Count-distance<distance )distance=Stations.Count- distance;break;
                default: break;
            }
            return distance;
        }
        public string PassedStation(int a, int b)
        {
            string stringPass="";
            List<int> tempList = new List<int>();
            int aed=0,bed=0,exed=0,extra=Extral,count=0,step=1;
            switch (Type)
            {
                case 1:
                    for(int i=0; i<Stations.Count;i++)
                    {
                        if (aed == 1 && bed == 1) break;
                        if (Stations[i] == a) { aed = 1; continue; }
                        if (Stations[i] == b) { bed = 1; continue; }
                        if (aed == 1 && bed == 0) tempList.Add(Stations[i]);
                        if (bed == 1 && aed == 0) tempList.Insert(0, Stations[i]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < Stations.Count; i++)
                    {
                        if (aed == 1 && bed == 1) break;
                        if (Stations[i] == a) 
                        { 
                            if(Stations[i] == extra) exed++;
                            if (exed == 2) tempList.Clear();
                            aed = 1; continue; 
                        }
                        if (Stations[i] == b) 
                        {
                            if (Stations[i] == extra) exed++;
                            if (exed == 2) { tempList.Clear(); exed--; }
                            bed = 1; continue; 
                        }
                        if (aed + bed == 1 &&  Stations[i] == extra) exed++;
                        if (exed == 1) count++;
                        if (exed == 2)
                        {
                            if (aed == 1)
                            {
                                for (int j = 0; j < count; j++)
                                {
                                    if (tempList.Count>0)tempList.RemoveAt(tempList.Count - 1);
                                }
                            }
                            if (bed == 1)
                            {
                                for (int j = 0; j < count; j++)
                                {
                                    tempList.RemoveAt(0);
                                }
                            }
                            exed--;
                        }
                        if (aed == 1 && bed == 0) tempList.Add(Stations[i]);
                        if (bed == 1 && aed == 0) tempList.Insert(0, Stations[i]);
                    }
                    break;
                case 3:
                    for (int i = 0;aed+bed<2;)
                    {
                        if (i < 0) i = Stations.Count - 1;
                        if (i >= Stations.Count) i = 0;
                        if (Stations[i] == a) 
                        {
                            if (aed+bed==0&&Distance(Stations[i+1], b) > Distance(a, b)) step = -1;
                            i = i + step;
                            aed = 1; continue; 
                        }
                        if (Stations[i] == b) 
                        {
                            if (aed + bed == 0 && Distance(Stations[i + 1], a) > Distance(b, a)) step = -1;
                            i = i + step;
                            bed = 1; continue;
                        }
                        if (aed == 1 && bed == 0) tempList.Add(Stations[i]);
                        if (bed == 1 && aed == 0) tempList.Insert(0, Stations[i]);
                        i = i + step;
                    }
                    break;
                default: break;
            }
            for (int i = 0; i < tempList.Count; i++)
            {
                stringPass += tempList[i].ToString() + ",";
            }
            stringPass = stringPass.TrimEnd(',');
            return stringPass;
        }
        public int LastChangeStation(int a)
        {
            int lastchange=0;
            foreach (int station in Stations)
            {
                if (ChangeStations.Contains(station)) lastchange = station;
                if (station == a) break;
            }
            if (Type == 3 && lastchange == 0) lastchange = ChangeStations[ChangeStations.Count - 1];
            return lastchange;
        }
        public int NextChangeStation(int a)
        {
            int nextchange = 0,aed=0,exed=0;
            foreach (int station in Stations)
            {
                if (station == Extral) exed++;
                if (station == a) aed=1;
                if (aed == 1 && ChangeStations.Contains(station)) { nextchange = station; break; }
            }
            if (exed == 2 && nextchange == Extral) nextchange = 0;
            if (Type == 3 && nextchange == 0) nextchange = ChangeStations[0];
            return nextchange;
        }
    }
}
