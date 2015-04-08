using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SubWay
{
    class SubWeb
    {        
        public const int INFINITY = int.MaxValue/2-1;
        public const int StationMax = 1500;
        public const int LineMax =50;
        public int StationCount { get; private set; } 
        public int LineCount { get; private set; }
        public int ChangeStationCount { get;private set; }
        public int[,] GraghLinkTable { get; private set; }
        public int[,] GraghDistanceTable{get;private set;}
        public int[] GraghVertexNum { get; private set; }
        public string[,] GraghShortPath{get;private set;}
        public string StationXml { get; private set; }
        public string LineXml { get; private set; }
        public Station[] StationsArray { get; set; }
        public Line[] LinesArray { get; set; }
        public List<int> FreeStationNum { get; set; }
        public List<int> FreeLineNum { get; set; }
        public SubWeb(string xmlstation, string xmlline)
        {
            int i,staID,linID,x,y,type=0,extra=0;
            int[] tempintarr, tempintarr1;
            string linsta="", linchasta="",stalin="",name="",pos="",color="",tempstr;
            string[] tempstrarr;
            char [] tempchar={'(',')',' '};
            StationsArray = new Station[StationMax];
            FreeStationNum=new List<int>();
            for (i = 1; i < StationMax; i++)
            {
                FreeStationNum.Add(i);
            }
            LinesArray = new Line[LineMax];
            FreeLineNum=new List<int>();
            for(i=1;i<LineMax;i++)
            {
                FreeLineNum.Add(i);
            }
            ChangeStationCount= StationCount = LineCount = 0;
            StationXml = xmlstation;
            LineXml = xmlline;
            XmlDocument XmlStation = new XmlDocument();
            XmlStation.Load(xmlstation);
            XmlNode StationRoot= XmlStation.SelectSingleNode("All_Stations");
            XmlNodeList StationList = StationRoot.ChildNodes;
            foreach (XmlNode StationNode in StationList)
            {
                XmlElement station=(XmlElement)StationNode;
                staID = int.Parse(station.GetAttribute("Station_ID"));
                XmlNodeList nodelist = StationNode.ChildNodes;
                foreach (XmlNode nodenode in nodelist)
                {
                    XmlElement nodele = (XmlElement)nodenode;
                    if (nodele.Name == "Station_Name") name = nodele.InnerText;
                    if (nodele.Name == "Station_Position") pos = nodele.InnerText;
                    if (nodele.Name == "Station_Lines") stalin = nodele.InnerText;
                }
                tempstr = pos.Trim(tempchar);
                tempstrarr = tempstr.Split(',');
                x = int.Parse(tempstrarr[0]);
                y = int.Parse(tempstrarr[1]);
                tempstr = stalin;
                tempstrarr = tempstr.Split(',');
                tempintarr = new int[tempstrarr.Length];
                for (i = 0; i < tempstrarr.Length; i++) tempintarr[i] = int.Parse(tempstrarr[i]);
                StationsArray[staID] = new Station(x, y, name, tempintarr);
                FreeStationNum.Remove(staID);
                if (tempintarr.Length > 1) ChangeStationCount++;
                StationCount++;
            }
            XmlDocument XmlLine = new XmlDocument();
            XmlLine.Load(xmlline);
            XmlNode LineRoot = XmlLine.SelectSingleNode("All_Lines");
            XmlNodeList LineList = LineRoot.ChildNodes;
            foreach (XmlNode LineNode in LineList)
            {
                XmlElement line = (XmlElement)LineNode;
                linID = int.Parse(line.GetAttribute("Line_ID"));
                XmlNodeList nodelist = LineNode.ChildNodes;
                foreach (XmlNode nodenode in nodelist)
                {
                    XmlElement nodele = (XmlElement)nodenode;
                    if (nodele.Name == "Line_Color") color = nodele.InnerText;
                    if (nodele.Name == "Line_Type") type = int.Parse(nodele.InnerText);
                    if (nodele.Name == "Line_Stations") linsta = nodele.InnerText;
                    if (nodele.Name == "Line_ChangeStations") linchasta = nodele.InnerText;
                    if (nodele.Name == "Line_Extral") extra = int.Parse(nodele.InnerText);
                }
                if (linsta != "")
                {
                    tempstr = linsta;
                    tempstrarr = tempstr.Split(',');
                    tempintarr = new int[tempstrarr.Length];
                    for (i = 0; i < tempstrarr.Length; i++) tempintarr[i] = int.Parse(tempstrarr[i]);
                }
                else tempintarr = new int[0];
                if (linchasta != "")
                {
                    tempstr = linchasta;
                    tempstrarr = tempstr.Split(',');
                    tempintarr1 = new int[tempstrarr.Length];
                    for (i = 0; i < tempstrarr.Length; i++) tempintarr1[i] = int.Parse(tempstrarr[i]);
                }
                else tempintarr1 = new int[0];
                LinesArray[linID] = new Line(type, extra, color, tempintarr, tempintarr1);
                FreeLineNum.Remove(linID);
                if (type == 2 && StationsArray[extra].Lines.Count == 1) ChangeStationCount++;
                LineCount++;
            }
        }
        public void AddStation(int x,int y,string name,int [] lines)
        {
            int prox, proy,  extraMark = 0, closeMark = 0,
                i = 0, found = 0, exed = 0, prodistan = 0, distan = 0, todistan = 0, tempid = 0;            
            if (StationCount+1 >= StationsArray.Length) return;
            StationCount++;
            if (lines.Length > 1) ChangeStationCount++;
            tempid = FreeStationNum[0];
            FreeStationNum.Remove(tempid);
            XmlDocument XmlLine = new XmlDocument();
            XmlLine.Load(LineXml);
            XmlNodeList linelist=XmlLine.SelectSingleNode("All_Lines").ChildNodes;
            foreach (int line in lines)
            {
                extraMark = 0;
                closeMark = 0;
                i = 0; 
                found = 0;
                exed = 0;
                prodistan = 0;
                distan = 0;
                todistan = 0; 
                if(LinesArray[line].Stations.Count==1)
                {
                    closeMark = 1;
                }
                else
                {
                    prox = StationsArray[LinesArray[line].Stations[0]].X;
                    proy = StationsArray[LinesArray[line].Stations[0]].Y;
                    prodistan = (x - prox) * (x - prox) + (y - proy) * (y - proy);
                    for (i=1;i<LinesArray[line].Stations.Count; i++ )
                    {
                        if (found == 1) break;
                        if (LinesArray[line].Stations[i] == LinesArray[line].Extral) exed++;
                        distan=(StationsArray[LinesArray[line].Stations[i]].X-prox)
                            *(StationsArray[LinesArray[line].Stations[i]].X-prox)
                            +(StationsArray[LinesArray[line].Stations[i]].Y-proy)
                            *(StationsArray[LinesArray[line].Stations[i]].Y-proy);
                        prox = StationsArray[LinesArray[line].Stations[i]].X;
                        proy = StationsArray[LinesArray[line].Stations[i]].Y;
                        todistan = (x - prox) * (x - prox) + (y - proy) * (y - proy);
                        if (exed == 2) { extraMark = i; prodistan = todistan; exed--; continue; }
                        if (todistan <= distan && prodistan <= distan) { closeMark = i; found = 1; }
                        prodistan = todistan;
                    }
                    if (closeMark == 0)
                    {
                        closeMark = LinesArray[line].Stations.Count;
                        if (LinesArray[line].Type == 2)
                        {
                            prox = StationsArray[LinesArray[line].Stations[extraMark-1]].X;
                            proy = StationsArray[LinesArray[line].Stations[extraMark-1]].Y;
                            prodistan = (x - prox) * (x - prox) + (y - proy) * (y - proy);
                            prox=StationsArray[LinesArray[line].Stations[LinesArray[line].Stations.Count-1]].X;
                            proy=StationsArray[LinesArray[line].Stations[LinesArray[line].Stations.Count-1]].Y;
                            todistan=(x - prox) * (x - prox) + (y - proy) * (y - proy);
                            if (prodistan < todistan) {closeMark = extraMark;} ;
                        }
                        prox= StationsArray[LinesArray[line].Stations[0]].X;
                        proy = StationsArray[LinesArray[line].Stations[0]].Y;
                        prodistan = (x - prox) * (x - prox) + (y - proy) * (y - proy);
                        prox = StationsArray[LinesArray[line].Stations[closeMark-1]].X;
                        proy = StationsArray[LinesArray[line].Stations[closeMark-1]].Y;
                        todistan = (x - prox) * (x - prox) + (y - proy) * (y - proy);
                        if ( prodistan < todistan) { closeMark = 0; }
                    }
                }
                LinesArray[line].Stations.Insert(closeMark, tempid);
                if (lines.Length > 1) LinesArray[line].ChangeStations.Add(tempid);
                foreach(XmlNode linenode in linelist )
                {
                    XmlElement lineele=(XmlElement)linenode;
                    if(lineele.GetAttribute("Line_ID")==line.ToString())
                    {
                        XmlNodeList nodelist =lineele.ChildNodes;
                        foreach(XmlNode nodenode in nodelist )
                        {
                            XmlElement nodeele =(XmlElement)nodenode;
                            if(nodeele.Name=="Line_Stations")
                            {
                                nodeele.InnerText=LinesArray[line].StationsToString();
                            }
                            if(nodeele.Name=="Line_ChangeStations")
                            {
                                if(lines.Length>1)nodeele.InnerText=LinesArray[line].ChangeStationsToString();
                            }
                        }
                        break;
                    }
                }
            }
            XmlLine.Save(LineXml);            
            StationsArray[tempid] = new Station(x, y, name, lines);
            XmlDocument XmlStation = new XmlDocument();
            XmlStation.Load(StationXml);
            XmlNode StationRoot = XmlStation.SelectSingleNode("All_Stations");
            XmlElement stationnode = XmlStation.CreateElement("Station");
            stationnode.SetAttribute("Station_ID", tempid.ToString());
            XmlElement stationsub1 = XmlStation.CreateElement("Station_Name");
            stationsub1.InnerText = name;
            stationnode.AppendChild(stationsub1);
            XmlElement stationsub2 = XmlStation.CreateElement("Station_Position");
            stationsub2.InnerText = StationsArray[tempid].PositionToString(); ;
            stationnode.AppendChild(stationsub2);
            XmlElement stationsub3 = XmlStation.CreateElement("Station_Lines");
            stationsub3.InnerText = StationsArray[tempid].LinesToString();
            stationnode.AppendChild(stationsub3);
            StationRoot.AppendChild(stationnode);
            XmlStation.Save(StationXml);
        }
        public void AddLine(int type,string colorname,string [] stationsname,int[] positionx,int[] positiony)
        {
            int i,j,idmark,extra=0,tempid,tempid1;
            int[] stations; int[] changestations;
            List<int> templist=new List<int>();
            stations=new int [stationsname.Length];            
            int [] templine=new int[1];          
            if (LineCount > LinesArray.Length) return;
            LineCount++;
            tempid = FreeLineNum[0];
            FreeLineNum.Remove(tempid);
            templine[0] = tempid;
            XmlDocument XmlStation = new XmlDocument();
            XmlStation.Load(StationXml);
            XmlNode StationRoot = XmlStation.SelectSingleNode("All_Stations");
            XmlNodeList StationList = StationRoot.ChildNodes;
            for (i = 0; i < stationsname.Length; i++)
            {
                idmark=0;
                for (j = 1; j < StationMax; j++) 
                {
                    if (StationsArray[j]!=null&&StationsArray[j].Name == stationsname[i]) { idmark = j; break; }
                }
                if (idmark > 0) 
                {
                    if (StationsArray[idmark].Lines.Count == 1)
                    {
                        ChangeStationCount++;
                        if (StationsArray[idmark].Lines[0] == tempid) extra = idmark;
                        else
                        {
                            int line = StationsArray[idmark].Lines[0];
                            LinesArray[line].ChangeStations.Add(idmark);
                            XmlDocument XmlLine1 = new XmlDocument();
                            XmlLine1.Load(LineXml);
                            XmlNodeList linelist1 = XmlLine1.SelectSingleNode("All_Lines").ChildNodes;
                            foreach (XmlNode linenode1 in linelist1)
                            {
                                XmlElement lineele1 = (XmlElement)linenode1;
                                if (lineele1.GetAttribute("Line_ID") == line.ToString())
                                {
                                    XmlNodeList nodelist1 = lineele1.ChildNodes;
                                    foreach (XmlNode nodenode1 in nodelist1)
                                    {
                                        XmlElement nodeele1 = (XmlElement)nodenode1;
                                        if (nodeele1.Name == "Line_ChangeStations")
                                        {
                                            nodeele1.InnerText = LinesArray[line].ChangeStationsToString();
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            XmlLine1.Save(LineXml);
                            StationsArray[idmark].Lines.Add(tempid);
                        }
                    }
                    else 
                    {
                        if (StationsArray[idmark].Lines.Contains(tempid)==false)
                            StationsArray[idmark].Lines.Add(tempid);
                    }
                    stations[i] = idmark; 
                    templist.Add(idmark);
                    foreach (XmlNode stationnode in StationList)
                    {
                        XmlElement stationele = (XmlElement)stationnode;
                        if (stationele.GetAttribute("Station_ID") == idmark.ToString())
                        {
                            XmlNodeList nodelist = stationele.ChildNodes;
                            foreach (XmlNode nodenode in nodelist)
                            {
                                XmlElement nodeele = (XmlElement)nodenode;
                                if (nodeele.Name == "Station_Lines")
                                {
                                    nodeele.InnerText = StationsArray[idmark].LinesToString();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                else 
                {               
                    if (StationCount+1 >= StationsArray.Length) return;
                    StationCount++;
                    tempid1 = FreeStationNum[0];
                    FreeStationNum.Remove(tempid1);
                    StationsArray[tempid1] = new Station(positionx[i], positiony[i], stationsname[i], templine);
                    stations[i] = tempid1;
                    XmlElement stationnode = XmlStation.CreateElement("Station");
                    stationnode.SetAttribute("Station_ID", tempid1.ToString());
                    XmlElement stationsub1 = XmlStation.CreateElement("Station_Name");
                    stationsub1.InnerText = stationsname[i];
                    stationnode.AppendChild(stationsub1);
                    XmlElement stationsub2 = XmlStation.CreateElement("Station_Position");
                    stationsub2.InnerText = StationsArray[tempid1].PositionToString();
                    stationnode.AppendChild(stationsub2);
                    XmlElement stationsub3 = XmlStation.CreateElement("Station_Lines");
                    stationsub3.InnerText = StationsArray[tempid1].LinesToString();
                    stationnode.AppendChild(stationsub3);
                    StationRoot.AppendChild(stationnode); 
                }
            }
            XmlStation.Save(StationXml);
            changestations = new int[templist.Count];
            for (j = 0; j < templist.Count; j++) changestations[j] = templist[j];
            LinesArray[tempid] = new Line(type, extra, colorname, stations, changestations);
            XmlDocument XmlLine = new XmlDocument();
            XmlLine.Load(LineXml);
            XmlNode LineRoot = XmlLine.SelectSingleNode("All_Lines");
            XmlNodeList LineList = LineRoot.ChildNodes;
            XmlElement linenode = XmlLine.CreateElement("Line");
            linenode.SetAttribute("Line_ID", tempid.ToString());
            XmlElement linesub1 = XmlLine.CreateElement("Line_Color");
            linesub1.InnerText = colorname ;
            linenode.AppendChild(linesub1);
            XmlElement linesub2 = XmlLine.CreateElement("Line_Type");
            linesub2.InnerText =type.ToString();
            linenode.AppendChild(linesub2);
            XmlElement linesub3 = XmlLine.CreateElement("Line_Stations");
            linesub3.InnerText = LinesArray[tempid].StationsToString();
            linenode.AppendChild(linesub3);
            XmlElement linesub4 = XmlLine.CreateElement("Line_ChangeStations");
            linesub4.InnerText = LinesArray[tempid].ChangeStationsToString();
            linenode.AppendChild(linesub4);
            XmlElement linesub5 = XmlLine.CreateElement("Line_Extral");
            linesub5.InnerText = extra.ToString();
            linenode.AppendChild(linesub5);
            LineRoot.AppendChild(linenode);
            XmlLine.Save(LineXml);
        }
        public void DeleteStation(int a)
        {
            int templine,i,linecount;
            XmlDocument XmlLine = new XmlDocument();
            XmlLine.Load(LineXml);
            XmlNode LineRoot = XmlLine.SelectSingleNode("All_Lines");
            XmlNodeList LineList = LineRoot.ChildNodes;
            linecount =StationsArray[a].Lines.Count;
            if(linecount ==1)
            {
                templine = StationsArray[a].Lines[0];
                if(a==LinesArray[templine].Extral)
                {
                    while (LinesArray[templine].ChangeStations.Contains(a))
                    {
                        LinesArray[templine].ChangeStations.Remove(a);
                    }
                    ChangeStationCount--;
                }
                while (LinesArray[templine].Stations.Contains(a))
                {
                    LinesArray[templine].Stations.Remove(a);
                }
                foreach (XmlNode linenode in LineList)
                {
                    XmlElement lineele = (XmlElement)linenode;
                    if (lineele.GetAttribute("Line_ID") == templine.ToString())
                    {
                        XmlNodeList nodelist = lineele.ChildNodes;
                        foreach (XmlNode nodenode in nodelist)
                        {
                            XmlElement nodeele = (XmlElement)nodenode;
                            if (nodeele.Name == "Line_Stations")
                            {
                                nodeele.InnerText = LinesArray[templine].StationsToString();
                                continue ;
                            }
                            if(nodeele.Name == "Line_ChangeStations")
                            {
                                nodeele.InnerText = LinesArray[templine].ChangeStationsToString();
                                continue ;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                for (i = 0; i < linecount; i++)
                {
                    templine = StationsArray[a].Lines[i];
                    while (LinesArray[templine].ChangeStations.Contains(a))
                    {
                        LinesArray[templine].ChangeStations.Remove(a);
                    }
                    while (LinesArray[templine].Stations.Contains(a))
                    {
                        LinesArray[templine].Stations.Remove(a);
                    }
                    foreach (XmlNode linenode in LineList)
                    {
                        XmlElement lineele = (XmlElement)linenode;
                        if (lineele.GetAttribute("Line_ID") == templine.ToString())
                        {
                            XmlNodeList nodelist = lineele.ChildNodes;
                            foreach (XmlNode nodenode in nodelist)
                            {
                                XmlElement nodeele = (XmlElement)nodenode;
                                if (nodeele.Name == "Line_Stations")
                                {
                                    nodeele.InnerText = LinesArray[templine].StationsToString();
                                    continue;
                                }
                                if (nodeele.Name == "Line_ChangeStations")
                                {
                                    nodeele.InnerText = LinesArray[templine].ChangeStationsToString();
                                    continue;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            XmlLine.Save(LineXml);
            XmlDocument XmlStation = new XmlDocument();
            XmlStation.Load(StationXml);
            XmlNode StationRoot = XmlStation.SelectSingleNode("All_Stations");
            XmlNodeList StationList = StationRoot.ChildNodes;
            foreach (XmlNode stationnode in StationList)
            {
                XmlElement stationele = (XmlElement)stationnode;
                if (stationele.GetAttribute("Station_ID") == a.ToString())
                {
                    StationRoot.RemoveChild(stationnode);
                    break;
                }
            }
            XmlStation.Save(StationXml);
            StationCount--;
            StationsArray[a] = null;
            FreeStationNum.Add(a);
        }
        public void DeleteLine(int b)
        {
            int templine,linecount;
            XmlDocument XmlStation=new XmlDocument();
            XmlStation.Load(StationXml);
            XmlNode StationRoot=XmlStation.SelectSingleNode("All_Stations");
            XmlNodeList StationList=StationRoot.ChildNodes;
            XmlDocument XmlLine = new XmlDocument();
            XmlLine.Load(LineXml);
            XmlNode LineRoot = XmlLine.SelectSingleNode("All_Lines");
            XmlNodeList LineList = LineRoot.ChildNodes;
            foreach (int station in LinesArray[b].Stations)
            {
                if (StationsArray[station] == null) { ChangeStationCount--; continue; }
                linecount =StationsArray[station].Lines.Count ;
                if (linecount == 1)
                {
                    foreach (XmlNode stationnode in StationList)
                    {
                        XmlElement stationele = (XmlElement)stationnode;
                        if (stationele.GetAttribute("Station_ID") == station.ToString())
                        {
                            StationRoot.RemoveChild(stationnode);
                            break;
                        }
                    }
                    StationCount--;
                    StationsArray[station] = null;
                    FreeStationNum.Add(station);
                }
                else if (linecount == 2)
                {
                    while (StationsArray[station].Lines.Contains(b))
                    {
                        StationsArray[station].Lines.Remove(b);
                    }
                    ChangeStationCount--;
                    foreach (XmlNode stationnode in StationList)
                    {
                        XmlElement stationele = (XmlElement)stationnode;
                        if (stationele.GetAttribute("Station_ID") == station.ToString())
                        {
                            XmlNodeList nodelist = stationele.ChildNodes;
                            foreach (XmlNode nodenode in nodelist)
                            {
                                XmlElement nodeele = (XmlElement)nodenode;
                                if (nodeele.Name == "Station_Lines")
                                {
                                    nodeele.InnerText = StationsArray[station].LinesToString();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    templine = StationsArray[station].Lines[0];
                    while (LinesArray[templine].ChangeStations.Contains(station))
                    {
                        LinesArray[templine].ChangeStations.Remove(station);
                    }
                    foreach (XmlNode linenode in LineList)
                    {
                        XmlElement lineele = (XmlElement)linenode;
                        if (lineele.GetAttribute("Line_ID") == templine.ToString())
                        {
                            XmlNodeList nodelist = lineele.ChildNodes;
                            foreach (XmlNode nodenode in nodelist)
                            {
                                XmlElement nodeele = (XmlElement)nodenode;
                                if (nodeele.Name == "Line_ChangeStations")
                                {
                                    nodeele.InnerText = LinesArray[templine].ChangeStationsToString();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                else
                {
                    while (StationsArray[station].Lines.Contains(b))
                    {
                        StationsArray[station].Lines.Remove(b);
                    }
                    foreach (XmlNode stationnode in StationList)
                    {
                        XmlElement stationele = (XmlElement)stationnode;
                        if (stationele.GetAttribute("Station_ID") == station.ToString())
                        {
                            XmlNodeList nodelist = stationele.ChildNodes;
                            foreach (XmlNode nodenode in nodelist)
                            {
                                XmlElement nodeele = (XmlElement)nodenode;
                                if (nodeele.Name == "Station_Lines")
                                {
                                    nodeele.InnerText = StationsArray[station].LinesToString();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }                
            }
            XmlStation.Save(StationXml);
            foreach (XmlNode linenode in LineList)
            {
                XmlElement lineele = (XmlElement)linenode;
                if (lineele.GetAttribute("Line_ID") == b.ToString())
                {
                    LineRoot.RemoveChild(linenode);
                    break;
                }
            }
            XmlLine.Save(LineXml);
            LinesArray[b] = null;
            LineCount--;
            FreeLineNum.Add(b);
            FreeLineNum.Sort();
        }
        public void InitialGragh()
        {
            if (ChangeStationCount == 0) return;
            int i,a,b,j=0,weight,w=0,v=0,count,extraed=0,miw = 0, min;
            List <int> tempList=new List<int>() ;
            int[,] final = new int[ChangeStationCount, ChangeStationCount];           
            GraghDistanceTable = new int[ChangeStationCount, ChangeStationCount];
            GraghShortPath = new string[ChangeStationCount, ChangeStationCount];            
            GraghLinkTable = new int[ChangeStationCount, ChangeStationCount];
            GraghVertexNum = new int[ChangeStationCount];
            for(int n=0 ;n<ChangeStationCount;n++)
                for(int m=0;m<ChangeStationCount;m++)GraghLinkTable[n,m]=INFINITY;
            foreach (Line line in LinesArray)
            {
                if (line == null) continue;
                if (line.ChangeStations.Count == 0) continue;
                int exed = 0;
                foreach (int station in line.Stations)
                {
                    if (line.ChangeStations.Contains(station) == true)
                    {
                        if (station == line.Extral)
                        {
                            exed++;
                            if (exed == 2) line.ChangeStations.Add(station);
                            else 
                            {
                                while (line.ChangeStations.Contains(station))
                                {
                                    line.ChangeStations.Remove(station);
                                }
                                line.ChangeStations.Add(station);
                            }
                        }
                        else
                        {
                            while (line.ChangeStations.Contains(station))
                            {
                                line.ChangeStations.Remove(station);
                            }
                            line.ChangeStations.Add(station);
                        }
                    }
                }
                a=line.ChangeStations[0];
                if (tempList.Contains(a) == false)
                {
                    GraghVertexNum[j] = a;
                    w = j;
                    tempList.Add(a);
                    j++;
                }
                else
                {
                    for (count = 0; count < tempList.Count; count++)
                    {
                        if (GraghVertexNum[count] == a)
                        {
                            w = count;
                            break;
                        }
                    }
                }
                extraed=0;
                if(line.Type==3)
                {                    
                    b=line.LastChangeStation(line.Stations[line.Stations.Count-1]);
                    weight=line.Distance(a,b);
                    if(tempList.Contains(b)==false)
                    {
                        GraghVertexNum[j]=b;
                        v=j;
                        tempList.Add(b);
                        j++;
                    }
                    else
                    {
                        for(count=0;count<tempList.Count;count++)
                        {
                            if(GraghVertexNum[count]==b)
                            {
                                v=count;
                                break;
                            }
                        }
                    }
                    GraghLinkTable[w,v]=GraghLinkTable[v,w]=weight;
                }
                for (i = 1; i < line.ChangeStations.Count; i++)
                {
                    b=line.ChangeStations[i];
                    if (b == line.Extral) extraed++;
                    if (extraed == 2)
                    {
                        a = b;
                        for (count = 0; count < tempList.Count; count++)
                        {
                            if (GraghVertexNum[count] == b)
                            {
                                w = count;
                                break;
                            }
                        }
                        extraed--;
                        continue;
                    }
                    weight = line.Distance(a, b);
                    if(tempList.Contains(b)==false)
                    {
                        GraghVertexNum[j] = b;
                        v = j;
                        tempList.Add(b);
                        j++;
                    }
                    else
                    {
                        for (count = 0; count < tempList.Count; count++)
                        {
                            if (GraghVertexNum[count] == b)
                            {
                                v = count;
                                break;
                            }
                        }
                    }
                    GraghLinkTable[w, v] = GraghLinkTable[v, w] = weight;
                    w = v;
                    a = b;
                }
            }
            //Dijkstra算法求最短路径
            for (int start = 0; start < ChangeStationCount; start++)
            {
                for (v = 0; v < ChangeStationCount; v++)
                {
                    final[start, v] = 0;
                    GraghDistanceTable[start, v] = GraghLinkTable[start, v];
                    GraghShortPath[start, v] = "";
                    if (GraghDistanceTable[start, v] < INFINITY)
                    {
                        GraghShortPath[start, v] += (GraghVertexNum[start].ToString()+ "," + GraghVertexNum[v].ToString());
                    }
                }
                for (v = 0; v < ChangeStationCount; v++)
                {
                    min = INFINITY;
                    for (w = 0; w < ChangeStationCount; w++)
                    {
                        if (final[start, w] == 0 && GraghDistanceTable[start, w] < min) { miw = w; min = GraghDistanceTable[start, w]; }
                    }
                    final[start, miw] = 1;
                    for (w = 0; w < ChangeStationCount; w++)
                    {
                        if (final[start, w] == 0 && min + GraghLinkTable[miw, w] < GraghDistanceTable[start, w])
                        {
                            GraghDistanceTable[start, w] = min + GraghLinkTable[miw, w];
                            GraghShortPath[start, w] = GraghShortPath[start, miw];
                            GraghShortPath[start, w] += ("," + GraghVertexNum[w].ToString());
                        }
                    }
                }
                GraghShortPath[start, start] = GraghVertexNum[start].ToString();
                GraghDistanceTable[start, start] = 0;
            }
        }       
        public string ShortestPath(int a, int b)
        {
            string shortpath = "";
            int i=0,j=0,minlength=INFINITY,nowlength,tempa,tempb;
            int[] nearas = new int[2];
            int[] nearbs = new int[2];
            foreach (int line in StationsArray[a].Lines)
            {
                if (StationsArray[b].Lines.Contains(line))
                {
                    if (LinesArray[line].Type == 2)
                    {
                        int branch = 0, exed = 0;
                        foreach (int tempsta in LinesArray[line].Stations)
                        {
                            if (tempsta == a|| tempsta == b)
                            {
                                if(exed == 0) break;
                                else if (exed == 1)
                                {
                                    branch++;
                                    if (branch == 2) break;
                                }
                                else 
                                {
                                    if (branch == 1) shortpath = LinesArray[line].Extral.ToString() + ","; break;
                                }
                            }
                            if (tempsta == LinesArray[line].Extral) exed++;
                        }
                    }
                    shortpath = a.ToString() + ","+shortpath + b.ToString();
                    return shortpath;
                }
            }
            if (StationsArray[a].Lines.Count > 1 || a == LinesArray[StationsArray[a].Lines[0]].Extral) 
            { 
                nearas[0] = a; 
                nearas[1] = 0; 
            }
            else
            {
                nearas[0] = LinesArray[StationsArray[a].Lines[0]].LastChangeStation(a);
                nearas[1] = LinesArray[StationsArray[a].Lines[0]].NextChangeStation(a);
                if (nearas[0] == 0) { nearas[0] = nearas[1]; nearas[1] = -1; }
                if (nearas[1] == 0) nearas[1] = -1;
            }
            if (StationsArray[b].Lines.Count > 1||b==LinesArray[StationsArray[b].Lines[0]].Extral)
            {
                nearbs[0] = b;
                nearbs[1] = 0;
            }
            else
            {
                nearbs[0] = LinesArray[StationsArray[b].Lines[0]].LastChangeStation(b);
                nearbs[1] = LinesArray[StationsArray[b].Lines[0]].NextChangeStation(b);
                if (nearbs[0] == 0) { nearbs[0] = nearbs[1]; nearbs[1] = -1; }
                if (nearbs[1] == 0) nearbs[1] = -1;
            }
            foreach(int neara in nearas)
            {
                if(neara<=0)continue;
                foreach (int nearb in nearbs)
                {
                    if (nearb <= 0) continue ;
                    for (i = 0; i < ChangeStationCount; i++)
                    {
                        if (GraghVertexNum[i] == neara) break;
                    }
                    for (j = 0; j < ChangeStationCount; j++)
                    {
                        if (GraghVertexNum[j] == nearb) break;
                    }
                    tempa = LinesArray[StationsArray[a].Lines[0]].Distance(neara, a);
                    tempb=LinesArray[StationsArray[b].Lines[0]].Distance(nearb,b);
                    nowlength = tempa + GraghDistanceTable[i, j] + tempb;
                    if (minlength > nowlength)
                    {
                        minlength = nowlength;
                        shortpath = GraghShortPath[i, j];
                    }
                }
            }
            if (nearas[1] != 0) shortpath = a.ToString() + "," + shortpath;
            if (nearbs[1] != 0) shortpath = shortpath + "," + b.ToString();
            return shortpath;
        }
    }
}
