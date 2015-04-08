using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SubWay
{
    public partial class SubForm : Form
    {
        float zoomRate=1.0f,stepX=5.6f, stepY=4.2f;
        int offsetX = 0, offsetY = 0,DrawLine = 0,stationD = 10,changeStationD=14;
        int[,] drawNamePosition;
        string stringPath,ChangePath;
        enum DrawMode { OneLine,OnePath,All,None };
        DrawMode drawMode = DrawMode.None;
        SubWeb sub;

        private void refresh()
        {
            stepX = 5.6f;
            stepY = 4.2f;
            zoomRate=1.0f;
            offsetX = 0;
            offsetY = 0;
            stationD = 10;
            changeStationD = 14;
            drawMode = DrawMode.All;
            panelForm.Refresh();
            sub.InitialGragh();
            labelChangePath.Hide();
            comboBoxDeleteLine.Items.Clear();
            for (int i = 1; i < SubWeb.LineMax; i++)
            {
                if (sub.LinesArray[i] == null) continue;
                comboBoxDeleteLine.Items.Add(i.ToString() + "号线");
            }
            comboBoxDeleteStation.Items.Clear();
            comboBoxStart.Items.Clear();
            comboBoxEnd.Items.Clear();
            for (int i = 1; i < SubWeb.StationMax; i++)
            {
                if (sub.StationsArray[i] == null) continue;
                comboBoxDeleteStation.Items.Add(sub.StationsArray[i].Name);
                comboBoxStart.Items.Add(sub.StationsArray[i].Name);
                comboBoxEnd.Items.Add(sub.StationsArray[i].Name);
            }
            drawNamePosition = new int[SubWeb.StationMax,2];
            for (int i = 0; i < SubWeb.StationMax; i++)
            {
                drawNamePosition[i,0] = 0;
                drawNamePosition[i,1] = 0;
            }
            for (int i = 1; i < SubWeb.LineMax; i++)
            {
                if (sub.LinesArray[i] == null) continue;
                int tempIDpro,tempIDnow,tempX,tempY;
                tempIDnow = sub.LinesArray[i].Stations[0];
                tempIDpro = sub.LinesArray[i].Stations[1];
                tempX=sub.StationsArray[tempIDnow].X-sub.StationsArray[tempIDpro].X;
                tempY=sub.StationsArray[tempIDnow].Y-sub.StationsArray[tempIDpro].Y;
                if (tempX == 0) 
                {
                    if (sub.StationsArray[tempIDnow].X > 174) drawNamePosition[tempIDnow, 0] = -1;
                    else drawNamePosition[tempIDnow, 0] = 1;
                }
                else if (tempY == 0)
                {
                    if (sub.StationsArray[tempIDnow].Y > 176) drawNamePosition[tempIDnow, 1] = -1;
                    else drawNamePosition[tempIDnow, 1] = 1;
                }
                else
                {
                    if (tempX > 0) drawNamePosition[tempIDnow, 0] = 1;
                    else drawNamePosition[tempIDnow, 0] = -1;
                }
                tempIDpro = tempIDnow;
                for (int j = 1; j < sub.LinesArray[i].Stations.Count; j++)
                {
                    tempIDnow = sub.LinesArray[i].Stations[j];
                    tempX = sub.StationsArray[tempIDnow].X - sub.StationsArray[tempIDpro].X;
                    tempY = sub.StationsArray[tempIDnow].Y - sub.StationsArray[tempIDpro].Y;
                    if (tempX == 0)
                    {
                        if (sub.StationsArray[tempIDnow].X > 174) drawNamePosition[tempIDnow, 0] = -1;
                        else if (drawNamePosition[tempIDpro, 0] == 0)
                        { 
                            drawNamePosition[tempIDpro, 0] = 1; 
                            drawNamePosition[tempIDnow, 0] = -1; 
                        }
                        else drawNamePosition[tempIDnow, 0] = -drawNamePosition[tempIDpro, 0];
                    }
                    else if (tempY == 0)
                    {
                        if (sub.StationsArray[tempIDnow].Y > 176) drawNamePosition[tempIDnow, 1] = -1;
                        else if (drawNamePosition[tempIDpro, 1] == 0)
                        {
                            drawNamePosition[tempIDpro, 1] = 1;
                            drawNamePosition[tempIDnow, 1] = -1;
                        }
                        else drawNamePosition[tempIDnow, 1] = -drawNamePosition[tempIDpro, 1];
                    }
                    else
                    {
                        if (tempX < 0 && tempY < 0)
                        {
                            drawNamePosition[tempIDnow, 0] = -1;
                        }
                        else if (tempX < 0 && tempY > 0)
                        {
                            drawNamePosition[tempIDnow, 0] = 1;
                            drawNamePosition[tempIDnow, 1] = 1;
                        }
                        else if (tempX > 0 && tempY < 0)
                        {
                            drawNamePosition[tempIDnow, 0] = 1;
                            drawNamePosition[tempIDnow, 1] = 1;
                        }
                        else
                        {
                            drawNamePosition[tempIDnow, 0] = 1;
                            drawNamePosition[tempIDnow, 1] = -1;
                        }
                    }
                    tempIDpro = tempIDnow;
                }
            }
        }

        private void SubForm_Load(object sender, EventArgs e)
        {
            sub = new SubWeb("Stations.xml", "Lines.xml");
            refresh();
        }

        private void buttonAddStationOK_Click(object sender, EventArgs e)
        {
            if (textBoxStationName.Text == "")
            {
                MessageBox.Show("未输入站名。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxStationPosition.Text == "")
            {
                MessageBox.Show("未输入坐标。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(textBoxStationLine.Text == "")
            {
                MessageBox.Show("未输入所在线路。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int x, y;
            int[] lines;
            char[] tempch={'(',')','（','）',' '};
            char[] tempch1 = { ',', '，' };
            string name,tempString;
            string[] tempStringArray;
            name = textBoxStationName.Text;
            for (int i = 1; i < SubWeb.StationMax; i++)
            {
                if (sub.StationsArray[i] == null) continue;
                if (sub.StationsArray[i].Name == name) 
                {
                    MessageBox.Show("已经存在同名站点，请用其他站名。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                    return;
                }
            }
            tempString = textBoxStationPosition.Text;
            tempString = tempString.Trim(tempch);
            tempStringArray = tempString.Split(tempch1);
            if (tempStringArray.Length != 2)
            {
                MessageBox.Show("坐标输入有误。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (int.TryParse(tempStringArray[0],out x) == false)
            {
                MessageBox.Show("坐标输入有误。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (int.TryParse(tempStringArray[1], out y) == false)
            {
                MessageBox.Show("坐标输入有误。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (x < 0 || y < 0)
            {
                MessageBox.Show("坐标输入有误,坐标值不应小于0。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(x>180||y>180)
            {
                MessageBox.Show("坐标输入有误,坐标值不应大于180。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tempString = textBoxStationLine.Text;
            tempStringArray = tempString.Split(tempch1);
            lines = new int[tempStringArray.Length];
            for(int i=0;i<tempStringArray.Length;i++)
            {
                if (int.TryParse(tempStringArray[i], out lines[i]) == false)
                {
                    MessageBox.Show("所在线路输入有误。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (lines[i]<=0||lines[i] >= SubWeb.LineMax)
                {
                    MessageBox.Show("所在线路输入有误。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (sub.LinesArray[lines[i]] == null)
                {
                    MessageBox.Show("输入了不存在的线路。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            sub.AddStation(x,y,name,lines);
            MessageBox.Show("新站点添加成功！",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            refresh();
        }

        private void buttonAddLineOK_Click(object sender, EventArgs e)
        {
            int type;
            char[] ch={'(',')','（','）',' '};
            char[] chs = { ';','；' };
            char[] chs1 = { ',', '，',' '};
            string colorName, stringType, tempString;
            string[] stationNames, lineStations,tempStringArray;
            int[] positionXs, positionYs;
            if (comboBoxColor.SelectedIndex == 0)
            {
                MessageBox.Show("未选择颜色。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (listBoxAddLineStyle.SelectedItem.ToString()=="")
            {
                MessageBox.Show("未选择线路类型。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            stringType=listBoxAddLineStyle.SelectedItem.ToString();
            if(stringType=="经典线")type=1;
            else if(stringType=="双叉线")type=2;
            else type=3;
            if (textBoxLineStation.Text == "")
            {
                MessageBox.Show("请输入线路经过的站点。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (type == 2 && textBoxLineBranch.Text =="")
            {
                MessageBox.Show("请输入另一个支路的站点。",
                           "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (type != 2) textBoxLineBranch.Text = "";
            colorName = comboBoxColor.SelectedItem.ToString();
            tempString = textBoxLineStation.Text+textBoxLineBranch.Text;
            tempString = tempString.Replace('\n', ' ');
            tempString = tempString.Replace('\r', ' ');
            lineStations = tempString.Trim(chs1).Trim(chs).Split(chs);
            stationNames=new string[lineStations.Length];
            positionXs=new int[lineStations.Length];
            positionYs=new int[lineStations.Length];
            for (int i = 0; i < lineStations.Length; i++)
            {
                //if(lineStations[i]=="")continue;
                lineStations[i]=lineStations[i].Trim(ch);
                tempStringArray = lineStations[i].Split(chs1);
                if(tempStringArray.Length!=3)
                {
                    MessageBox.Show("站点坐标输入有误。",
                           "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                stationNames[i] = tempStringArray[0];
                if (int.TryParse(tempStringArray[1].Trim(ch), out positionXs[i]) == false)
                {
                    MessageBox.Show("站点坐标输入有误。",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (int.TryParse(tempStringArray[2].Trim(ch), out positionYs[i]) == false)
                {
                    MessageBox.Show("站点坐标输入有误。",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (positionXs[i] < 0 || positionYs[i] < 0)
                {
                    MessageBox.Show("站点坐标输入有误,坐标值不应小于0。",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (positionXs[i] > 180 || positionYs[i] > 180)
                {
                    MessageBox.Show("坐标输入有误,坐标值不应大于180。",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            sub.AddLine(type, colorName, stationNames, positionXs, positionYs);
            MessageBox.Show("线路添加成功。",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            refresh();
        }

        private void buttonDeleteStationOK_Click(object sender, EventArgs e)
        {
            string tempString;
            int tempInt=0;
            if (comboBoxDeleteStation.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要删除的站点。",
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tempString = comboBoxDeleteStation.SelectedItem.ToString();
            for (int i = 0; i < SubWeb.StationMax; i++)
            {
                if (sub.StationsArray[i] == null) continue;
                if (sub.StationsArray[i].Name == tempString)
                { 
                    tempInt = i; 
                    break; 
                }
            }
            sub.DeleteStation(tempInt);
            MessageBox.Show("站点已删除。",
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            refresh();
        }

        private void buttonDeleteLineOk_Click(object sender, EventArgs e)
        {
            string tempString;
            int tempInt = 0;
            if (comboBoxDeleteLine.SelectedIndex ==-1)
            {
                MessageBox.Show("请选择要删除的线路。",
                                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tempString = comboBoxDeleteLine.SelectedItem.ToString();
            for (int i = 0; i < SubWeb.LineMax; i++)
            {
                if (sub.LinesArray[i] == null) continue;
                if (i.ToString()+"号线"== tempString)
                {
                    tempInt = i;
                    break;
                }
            }
            sub.DeleteLine(tempInt);
            MessageBox.Show("线路已删除。",
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            refresh();
        }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Font fontString = new Font("黑体", 4*(zoomRate+1));
            Font fontBig=new Font("黑体",10*zoomRate);
            Pen penChangeStation = new Pen(Color.Black, 2*zoomRate);
            penChangeStation.DashStyle = DashStyle.Dot;
            Pen penPath = new Pen(Color.Silver, zoomRate*1.5f);
            penPath.DashStyle = DashStyle.DashDotDot;
            AdjustableArrowCap arrow = new AdjustableArrowCap(2, 2, false);
            penPath.CustomEndCap = arrow;
            SolidBrush brushStation = new SolidBrush(Color.White);
            Point proPoint = new Point();
            Point nowPoint = new Point();
            string[] pathStringArray;
            int[] pathIntArray;
            int proID, nowID;
            int extral, exed;
            switch (drawMode)
            {
                case DrawMode.All:
                    stepX = zoomRate * 5.6f;
                    stepY = zoomRate * 4.2f;
                    stationD = (int)(zoomRate * 10);
                    changeStationD = (int)(zoomRate * 14);
                    for (int i = 1; i < SubWeb.LineMax; i++)
                    {
                        if (sub.LinesArray[i] == null) continue;
                        extral = sub.LinesArray[i].Extral;
                        exed = 0;
                        Pen penLine = new Pen(Color.FromName(sub.LinesArray[i].ColorName), 4 * zoomRate);
                        Pen penStation = new Pen(Color.FromName(sub.LinesArray[i].ColorName), 2 * zoomRate);
                        proID=sub.LinesArray[i].Stations[0];
                        proPoint.X = (int)(sub.StationsArray[proID].X * stepX + offsetX);
                        proPoint.Y = (int)(sub.StationsArray[proID].Y * stepY + offsetY);
                        if (sub.StationsArray[proID].Lines.Count == 1)
                        {
                            g.DrawEllipse(penStation, proPoint.X - stationD / 2, proPoint.Y - stationD / 2, stationD, stationD);
                        }
                        if (sub.LinesArray[i].Type == 3)
                        {
                            nowID=sub.LinesArray[i].Stations[sub.LinesArray[i].Stations.Count-1];
                            nowPoint.X = (int)(sub.StationsArray[nowID].X * stepX + offsetX);
                            nowPoint.Y = (int)(sub.StationsArray[nowID].Y * stepY + offsetY);
                            if (sub.StationsArray[proID].Lines.Count == 1) g.DrawLine(penLine, proPoint, nowPoint);
                            else 
                            {
                                int mark = 0;
                                foreach (int lineID in sub.StationsArray[proID].Lines)
                                {
                                    if(lineID==i)continue;
                                    if (sub.StationsArray[nowID].Lines.Contains(lineID))
                                    {
                                        mark = lineID;
                                        break;
                                    }
                                }
                                if (mark == 0) g.DrawLine(penLine, proPoint, nowPoint);
                                else 
                                {
                                    if (mark > i) g.DrawLine(penLine, proPoint.X - zoomRate * 2.0f,
                                        proPoint.Y - zoomRate * 2.0f, nowPoint.X - zoomRate * 2.0f, nowPoint.Y - zoomRate * 2.0f);
                                    else g.DrawLine(penLine, proPoint.X + zoomRate *2.0f,
                                        proPoint.Y + zoomRate * 2.0f, nowPoint.X + zoomRate * 2.0f, nowPoint.Y + zoomRate * 2.0f);
                                }
                            }
                        }
                        for (int j = 1; j < sub.LinesArray[i].Stations.Count; j++)
                        {
                            nowID = sub.LinesArray[i].Stations[j];
                            nowPoint.X = (int)(sub.StationsArray[nowID].X * stepX + offsetX);
                            nowPoint.Y = (int)(sub.StationsArray[nowID].Y * stepY + offsetY);
                            if (sub.LinesArray[i].Stations[j] == extral) exed++;
                            if (exed == 2) { proID = nowID; proPoint = nowPoint; exed--; continue; }
                            if (sub.StationsArray[proID].Lines.Count == 1) g.DrawLine(penLine, proPoint, nowPoint);
                            else
                            {
                                int mark = 0;
                                foreach (int lineID in sub.StationsArray[proID].Lines)
                                {
                                    if (lineID == i) continue;
                                    if (sub.StationsArray[nowID].Lines.Contains(lineID))
                                    {
                                        mark = lineID;
                                        break;
                                    }
                                }
                                if (mark == 0) g.DrawLine(penLine, proPoint, nowPoint);
                                else
                                {
                                    if (mark > i) g.DrawLine(penLine, proPoint.X - zoomRate * 2.0f,
                                        proPoint.Y - zoomRate * 2.0f, nowPoint.X - zoomRate * 2.0f, nowPoint.Y -zoomRate *2.0f);
                                    else g.DrawLine(penLine, proPoint.X + zoomRate * 2.0f,
                                        proPoint.Y + zoomRate * 2.0f, nowPoint.X + zoomRate * 2.0f, nowPoint.Y + zoomRate *2.0f);
                                }
                            }
                            proID = nowID;
                            proPoint = nowPoint ;
                            if (sub.StationsArray[proID].Lines.Count == 1)
                            {
                                g.DrawEllipse(penStation, proPoint.X - stationD / 2, proPoint.Y - stationD / 2, stationD, stationD);
                            }
                        }
                    }
                    for (int i = 1; i < SubWeb.StationMax; i++)
                    {
                        if (sub.StationsArray[i] == null) continue;
                        Point pointTemp = new Point();
                        pointTemp.X = (int)(sub.StationsArray[i].X * stepX + offsetX);
                        pointTemp.Y = (int)(sub.StationsArray[i].Y * stepY + offsetY);
                        if (sub.StationsArray[i].Lines.Count == 1)
                        {
                            g.FillEllipse(brushStation,  pointTemp.X- stationD / 2 + zoomRate/2,
                                pointTemp.Y - stationD / 2 + zoomRate/2, stationD - 2, stationD - 2);
                        }
                        else
                        {
                            g.DrawEllipse(penChangeStation, pointTemp.X - changeStationD/2,
                               pointTemp.Y - changeStationD / 2, changeStationD, changeStationD);
                            g.FillEllipse(brushStation, pointTemp.X-changeStationD/2+zoomRate/2,
                                pointTemp.Y - changeStationD / 2+zoomRate/2, changeStationD - 2, changeStationD - 2);
                        }
                        g.DrawString(sub.StationsArray[i].Name, fontString, Brushes.Black,
                            pointTemp.X + 2.0f * (zoomRate + 1) + (sub.StationsArray[i].Name.Length+0.60f) * 3.0f * (zoomRate + 1) * (drawNamePosition[i, 0] - 1),
                            pointTemp.Y + 5.0f * (zoomRate + 1)*(drawNamePosition[i,1]-0.5f));
                    }
                    break;
                case DrawMode.OneLine:
                    stepX = zoomRate * 5.6f;
                    stepY = zoomRate * 4.2f;
                    stationD = (int)(zoomRate * 10);
                    changeStationD = (int)(zoomRate * 14);
                    Pen penOneLine = new Pen(Color.FromName(sub.LinesArray[DrawLine].ColorName), 4 * zoomRate);
                    Pen penLineStation = new Pen(Color.FromName(sub.LinesArray[DrawLine].ColorName), 2 * zoomRate);
                    extral = sub.LinesArray[DrawLine].Extral;
                    exed = 0;
                    proID=sub.LinesArray[DrawLine].Stations[0];
                    proPoint.X = (int)(sub.StationsArray[proID].X * stepX + offsetX);
                    proPoint.Y = (int)(sub.StationsArray[proID].Y * stepY + offsetY);
                    if (sub.StationsArray[proID].Lines.Count==1)
                    {
                        g.DrawEllipse(penLineStation, proPoint.X - stationD / 2, proPoint.Y - stationD / 2, stationD, stationD);
                    }
                    if (sub.LinesArray[DrawLine].Type == 3)
                    {
                        nowID=sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count-1];
                        nowPoint.X = (int)(sub.StationsArray[nowID].X * stepX + offsetX);
                        nowPoint.Y = (int)(sub.StationsArray[nowID].Y * stepY + offsetY);
                        g.DrawLine(penOneLine, proPoint, nowPoint);
                    }
                    for (int j = 1; j < sub.LinesArray[DrawLine].Stations.Count; j++)
                    {
                        nowID = sub.LinesArray[DrawLine].Stations[j];
                        nowPoint.X = (int)(sub.StationsArray[nowID].X * stepX + offsetX);
                        nowPoint.Y = (int)(sub.StationsArray[nowID].Y * stepY + offsetY);
                        if (sub.LinesArray[DrawLine].Stations[j] == extral) exed++;
                        if (exed == 2) { proID = nowID; proPoint = nowPoint; exed--; continue; }
                        g.DrawLine(penOneLine, proPoint, nowPoint);
                        proID = nowID;
                        proPoint = nowPoint ;
                        if (sub.StationsArray[proID].Lines.Count== 1)
                        {
                            g.DrawEllipse(penLineStation, proPoint.X - stationD / 2, proPoint.Y - stationD / 2, stationD, stationD);
                        }
                    }
                    foreach (int i  in sub.LinesArray[DrawLine].Stations)
                    {
                        Point pointTemp = new Point();
                        pointTemp.X = (int)(sub.StationsArray[i].X * stepX + offsetX);
                        pointTemp.Y = (int)(sub.StationsArray[i].Y * stepY + offsetY);
                        if (sub.StationsArray[i].Lines.Count == 1)
                        {
                            g.FillEllipse(brushStation,  pointTemp.X- stationD / 2 + zoomRate/2,
                                pointTemp.Y - stationD / 2 + zoomRate/2, stationD - 2, stationD - 2);
                        }
                        else
                        {
                            g.DrawEllipse(penChangeStation, pointTemp.X - changeStationD/2,
                               pointTemp.Y - changeStationD / 2, changeStationD, changeStationD);
                            g.FillEllipse(brushStation, pointTemp.X-changeStationD/2+zoomRate/2,
                                pointTemp.Y - changeStationD / 2+zoomRate/2, changeStationD - 2, changeStationD - 2);
                        }
                        g.DrawString(sub.StationsArray[i].Name, fontString, Brushes.Black,
                            pointTemp.X + 3.0f * (zoomRate + 1) + (sub.StationsArray[i].Name.Length+1) * 3.0f * (zoomRate + 1) * (drawNamePosition[i, 0] - 1),
                            pointTemp.Y + 6.0f * (zoomRate + 1)*(drawNamePosition[i,1]-0.5f));
                    }
                    break;
                case DrawMode.OnePath:
                    stepX = zoomRate * 5.6f;
                    stepY = zoomRate * 4.2f;
                    stationD = (int)(zoomRate * 10);
                    changeStationD = (int)(zoomRate * 14);
                    int tempa, tempb,nowline=0;
                    string tempPath;
                    string[] tempStringArray;
                    Pen penStart = new Pen(Color.MediumSeaGreen, 4);
                    Pen penEnd = new Pen(Color.CornflowerBlue, 4);
                    pathStringArray = stringPath.Split(',');
                    pathIntArray = new int[pathStringArray.Length];
                    for (int i = 0; i < pathStringArray.Length; i++) pathIntArray[i] = int.Parse(pathStringArray[i]);
                    tempa = pathIntArray[0];
                    proPoint.X=(int)(sub.StationsArray[tempa].X * stepX + offsetX);
                    proPoint.Y=(int)(sub.StationsArray[tempa].Y * stepY + offsetY);
                    ChangePath = "起点站：" + sub.StationsArray[tempa].Name.ToString()+Environment.NewLine;
                    ChangePath += "终点站：" + sub.StationsArray[pathIntArray[pathIntArray.Length - 1]].Name.ToString() + Environment.NewLine;
                    for (int pathCount = 1; pathCount < pathStringArray.Length; pathCount++)
                    {
                        tempb = pathIntArray[pathCount];
                        foreach (int line in sub.StationsArray[tempa].Lines)
                        {
                            if (sub.StationsArray[tempb].Lines.Contains(line)) { nowline = line; break; }
                        }
                        while (pathCount < pathStringArray.Length - 1)
                        {
                            if (sub.StationsArray[pathIntArray[pathCount + 1]].Lines.Contains(nowline)&&
                                pathIntArray[pathCount] != sub.LinesArray[nowline].Extral)
                            {
                                pathCount++;
                                tempb = pathIntArray[pathCount];
                            }
                            else break;
                        }
                        Pen penOnePath = new Pen(Color.FromName(sub.LinesArray[nowline].ColorName), 4 * zoomRate);
                        Pen penPathStation = new Pen(Color.FromName(sub.LinesArray[nowline].ColorName), 2 * zoomRate);
                        tempPath = sub.LinesArray[nowline].PassedStation(tempa, tempb);
                        tempStringArray = tempPath.Split(',');
                        proID=tempa;
                        proPoint.X = (int)(sub.StationsArray[proID].X * stepX + offsetX);
                        proPoint.Y = (int)(sub.StationsArray[proID].Y * stepY + offsetY);
                        if (tempa != pathIntArray[0])
                        {
                            g.DrawEllipse(penChangeStation, proPoint.X - changeStationD / 2,
                                proPoint.Y - changeStationD / 2, changeStationD, changeStationD);
                            g.FillEllipse(brushStation, proPoint.X - changeStationD / 2 + zoomRate / 2,
                                proPoint.Y - changeStationD / 2 + zoomRate / 2, changeStationD - 2, changeStationD - 2);
                            ChangePath += "换乘" + nowline.ToString() + "号线到" + sub.StationsArray[tempb].Name.ToString() +Environment.NewLine;
                        }//打印换乘站
                        else
                        {
                            ChangePath += "乘" + nowline.ToString() + "号线到" + sub.StationsArray[tempb].Name.ToString() +Environment.NewLine;
                        }
                        if (tempPath != "")
                        {
                            foreach (string station in tempStringArray)
                            {
                                nowID = int.Parse(station);
                                nowPoint.X = (int)(sub.StationsArray[nowID].X * stepX + offsetX);
                                nowPoint.Y = (int)(sub.StationsArray[nowID].Y * stepY + offsetY);
                                //画站点以及站点间连线
                                g.DrawLine(penOnePath, proPoint, nowPoint);
                                g.DrawLine(penPath, proPoint, nowPoint);
                                g.DrawEllipse(penPathStation, proPoint.X - stationD / 2, proPoint.Y - stationD / 2, stationD, stationD);
                                g.FillEllipse(brushStation, proPoint.X - stationD / 2 + zoomRate / 2, proPoint.Y - stationD / 2 + zoomRate / 2, stationD - 2, stationD - 2);
                                if (proID != pathIntArray[0]) g.DrawString(sub.StationsArray[proID].Name, fontString, Brushes.Black,
                                    proPoint.X + 3.0f * (zoomRate + 1) + (sub.StationsArray[proID].Name.Length + 1) *
                                    3.0f * (zoomRate + 1) * (drawNamePosition[proID, 0] - 1),
                                    proPoint.Y + 6.0f * (zoomRate + 1) * (drawNamePosition[proID, 1] - 0.5f));
                                proID = nowID;
                                proPoint = nowPoint;
                            }
                        }
                        nowID = tempb;
                        nowPoint.X = (int)(sub.StationsArray[nowID].X * stepX + offsetX);
                        nowPoint.Y = (int)(sub.StationsArray[nowID].Y * stepY + offsetY);
                        //打印最后一段路
                        g.DrawLine(penOnePath, proPoint, nowPoint);
                        g.DrawLine(penPath, proPoint, nowPoint);
                        g.DrawEllipse(penPathStation, proPoint.X - stationD / 2, proPoint.Y - stationD / 2, stationD, stationD);
                        g.FillEllipse(brushStation, proPoint.X - stationD / 2 + zoomRate / 2, proPoint.Y - stationD / 2 + zoomRate / 2, stationD - 2, stationD - 2);
                        if (proID != pathIntArray[0]) g.DrawString(sub.StationsArray[proID].Name, fontString, Brushes.Black,
                            proPoint.X + 3.0f * (zoomRate + 1) + (sub.StationsArray[proID].Name.Length + 1) *
                            3.0f * (zoomRate + 1) * (drawNamePosition[proID, 0] - 1),
                            proPoint.Y + 6.0f * (zoomRate + 1) * (drawNamePosition[proID, 1] - 0.5f));
                        tempa = tempb;
                    }
                    //画终点
                    proID = tempa;
                    proPoint.X=(int)(sub.StationsArray[proID].X * stepX + offsetX);
                    proPoint.Y=(int)(sub.StationsArray[proID].Y * stepY + offsetY);
                    g.DrawEllipse(penEnd, proPoint.X - (12 * zoomRate), proPoint.Y - (12 * zoomRate), 24 * zoomRate, 24 * zoomRate);
                    g.FillEllipse(brushStation, proPoint.X - (12 * zoomRate -2), proPoint.Y - (12 * zoomRate-2 ), 23*zoomRate-2, 23*zoomRate-2);
                    g.DrawString("终", fontBig, Brushes.CornflowerBlue, proPoint.X - (10 * zoomRate - 2), proPoint.Y - (10 * zoomRate - 2));
                    g.DrawString(sub.StationsArray[proID].Name, fontString, Brushes.Black,
                             proPoint.X + 6.0f * (zoomRate + 1) + (sub.StationsArray[proID].Name.Length + 1) *
                             3.75f * (zoomRate + 1) * (drawNamePosition[proID, 0] - 1),
                             proPoint.Y + 6.0f * (zoomRate + 1) * (drawNamePosition[proID, 1] - 0.5f));
                    proID = pathIntArray[0];
                    proPoint.X=(int)(sub.StationsArray[proID].X * stepX + offsetX);
                    proPoint.Y=(int)(sub.StationsArray[proID].Y * stepY + offsetY);
                    g.DrawEllipse(penStart, proPoint.X - (12 * zoomRate), proPoint.Y - (12 * zoomRate), 24 * zoomRate, 24 * zoomRate);
                    g.FillEllipse(brushStation, proPoint.X - (12 * zoomRate-2), proPoint.Y - (12 * zoomRate-2 ), 23 * zoomRate-2, 23 * zoomRate-2);
                    g.DrawString("起", fontBig, Brushes.MediumSeaGreen, proPoint.X - (10 * zoomRate - 2), proPoint.Y - (10 * zoomRate - 2));
                    g.DrawString(sub.StationsArray[proID].Name, fontString, Brushes.Black,
                             proPoint.X + 6.0f * (zoomRate + 1) + (sub.StationsArray[proID].Name.Length + 1) *
                             3.75f * (zoomRate + 1) * (drawNamePosition[proID, 0] - 1),
                             proPoint.Y + 6.0f * (zoomRate + 1) * (drawNamePosition[proID, 1] - 0.5f));
                    break;
                case DrawMode.None: g.Clear(this.BackColor); break;
                default: break;
            }
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            panelManage.Hide();
            string tempStringStart, tempStringEnd;
            string[] tempstring;
            int tempIntStart=0, tempIntEnd=0,find=0;
            if (comboBoxStart.SelectedIndex == -1)
            {
                MessageBox.Show("请选择出发站。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tempStringStart = comboBoxStart.SelectedItem.ToString();
            if(comboBoxEnd.SelectedIndex == -1)
            {
                MessageBox.Show("请选择到达站。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tempStringEnd = comboBoxEnd.SelectedItem.ToString();
            if (tempStringEnd == tempStringStart)
            {
                MessageBox.Show("别开玩笑了，您在原地打转。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i=0; i < SubWeb.StationMax; i++)
            {
                if (find == 2) break;
                if (sub.StationsArray[i] == null) continue;
                if (sub.StationsArray[i].Name == tempStringStart) { tempIntStart = i; find++; continue; }
                if (sub.StationsArray[i].Name == tempStringEnd) { tempIntEnd = i; find++; continue; }
            }
            stringPath = sub.ShortestPath(tempIntStart, tempIntEnd);
            tempstring = stringPath.Split(',');
            if (tempstring[1] == "")
            {
                MessageBox.Show("无法通过换乘到达", "提示", MessageBoxButtons.OK);
                return;
            }
            comboBoxStart.Text = "";
            comboBoxStart.SelectedIndex = -1;
            comboBoxEnd.Text = "";
            comboBoxEnd.SelectedIndex = -1;
            offsetX =510 - (int)(stepX * (sub.StationsArray[tempIntStart].X+ sub.StationsArray[tempIntEnd].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[tempIntStart].Y + sub.StationsArray[tempIntEnd].Y) / 2);
            drawMode = DrawMode.OnePath;
            ChangePath = "";
            panelForm.Refresh();
            labelChangePath.Text = ChangePath;
            labelChangePath.Show();
        }
    }
}
