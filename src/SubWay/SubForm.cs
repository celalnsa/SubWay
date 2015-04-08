using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//此部分是关于界面设计的源代码，主要功能的源码在SubClass.cs中
namespace SubWay
{
    public partial class SubForm : Form
    {
        Button[] buttons;
        int mouseX, mouseY;

        public SubForm()
        {
            InitializeComponent();
            buttons = new Button[50];
            buttons[1] = buttonL1;
            buttons[2] = buttonL2;
            buttons[3] = buttonL3;
            buttons[4] = buttonL4;
            buttons[5] = buttonL5;
            buttons[6] = buttonL6;
            buttons[7] = buttonL7;
            buttons[8] = buttonL8;
            buttons[9] = buttonL9;
            buttons[10] = buttonL10;
            buttons[11] = buttonL11;
            buttons[12] = buttonL12;
            buttons[13] = buttonL13;
            buttons[14] = buttonL14;
            buttons[15] = buttonL15;
            buttons[16] = buttonL16;
            buttons[17] = buttonL17;
            buttons[18] = buttonL18;
            buttons[19] = buttonL19;
            buttons[20] = buttonL20;
            buttons[21] = buttonL21;
            buttons[22] = buttonL22;
            buttons[23] = buttonL23;
            buttons[24] = buttonL24;
            buttons[25] = buttonL25;
            buttons[26] = buttonL26;
            buttons[27] = buttonL27;
            buttons[28] = buttonL28;
            buttons[29] = buttonL29;
            buttons[30] = buttonL30;
            buttons[31] = buttonL31;
            buttons[32] = buttonL32;
            buttons[33] = buttonL33;
            buttons[34] = buttonL34;
            buttons[35] = buttonL35;
            buttons[36] = buttonL36;
            buttons[37] = buttonL37;
            buttons[38] = buttonL38;
            buttons[39] = buttonL39;
            buttons[40] = buttonL40;
            buttons[41] = buttonL41;
            buttons[42] = buttonL42;
            buttons[43] = buttonL43;
            buttons[44] = buttonL44;
            buttons[45] = buttonL45;
            buttons[46] = buttonL46;
            buttons[47] = buttonL47;
            buttons[48] = buttonL48;
            buttons[49] = buttonL49;
            panelAllLine.Hide();
            panelManage.Hide(); 
            panelBranch.Hide();
            panelLine.Hide();
            panelSation.Hide();
            listBoxEnd.Hide();
            listBoxStart.Hide();
            listBoxColor.Hide();
            panelx.Hide();
            listBoxDeleteLine.Hide();
            listBoxDeleteStation.Hide();
            labelChangePath.Hide();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonManage_Click(object sender, EventArgs e)
        {
            if (panelManage.Visible == false)
            {
                labelChangePath.Hide();
                panelManage.Show();
                panelLine.Hide();
                panelSation.Hide();
            }
            else
            {
                labelChangePath.Hide();
                panelManage.Hide();
            }
        }

        private void buttonAllLine_Click(object sender, EventArgs e)
        {
            labelChangePath.Hide();
            if (panelAllLine.Visible == true)panelAllLine.Hide();
            else
            {
                for (int i = 1; i < SubWeb.LineMax; i++)
                {
                    if (sub.LinesArray[i] == null) { buttons[i].Hide(); }
                    else
                    {
                        buttons[i].BackColor = Color.FromName(sub.LinesArray[i].ColorName);
                        buttons[i].Show();
                    }
                }
                panelAllLine.Show();
            }
        }

        private void comboBoxStart_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxStart.DroppedDown == true) { listBoxStart.Hide(); return; }
            if (comboBoxStart.Text == "") { listBoxStart.Hide(); return; }
            listBoxStart.Items.Clear();
            int Lndex, Nndex = -1;
            Lndex = comboBoxStart.FindString(comboBoxStart.Text);
            while (Lndex > Nndex)
            {
                listBoxStart.Items.Add(comboBoxStart.Items[Lndex]);
                Nndex = Lndex;
                Lndex = comboBoxStart.FindString(comboBoxStart.Text, Nndex);
            }
            if (listBoxStart.Items.Count > 0)
            {
                listBoxStart.SetBounds(listBoxStart.Location.X, listBoxStart.Location.Y,
                    listBoxStart.Width, listBoxStart.ItemHeight * listBoxStart.Items.Count);
                listBoxStart.Show();
            }
            else listBoxStart.Hide();
        }

        private void comboBoxEnd_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxEnd.DroppedDown == true) { listBoxEnd.Hide(); return; }
            if (comboBoxEnd.Text == "") { listBoxEnd.Hide(); return; }
            listBoxEnd.Items.Clear();
            int Lndex, Nndex = -1;
            Lndex = comboBoxEnd.FindString(comboBoxEnd.Text);
            while (Lndex > Nndex)
            {
                listBoxEnd.Items.Add(comboBoxEnd.Items[Lndex]);
                Nndex = Lndex;
                Lndex = comboBoxEnd.FindString(comboBoxEnd.Text, Nndex);
            }
            if (listBoxEnd.Items.Count > 0)
            {
                listBoxEnd.SetBounds(listBoxEnd.Location.X, listBoxEnd.Location.Y,
                    listBoxEnd.Width, listBoxEnd.ItemHeight * listBoxEnd.Items.Count);
                listBoxEnd.Show();
            }
            else listBoxEnd.Hide();
        }

        private void listBoxStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStart.Text = listBoxStart.SelectedItem.ToString();
            listBoxStart.Hide();
        }

        private void listBoxEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEnd.Text = listBoxEnd.SelectedItem.ToString();
            listBoxEnd.Hide();
        }

        private void comboBoxColor_TextChanged(object sColorer, EventArgs e)
        {
            if (comboBoxColor.DroppedDown == true) { panelx.Hide(); listBoxColor.Hide(); return; }
            if (comboBoxColor.Text == "") { panelx.Hide(); listBoxColor.Hide(); return; }
            listBoxColor.Items.Clear();
            int Lndex, Nndex = -1;
            Lndex = comboBoxColor.FindString(comboBoxColor.Text);
            while (Lndex > Nndex)
            {
                listBoxColor.Items.Add(comboBoxColor.Items[Lndex]);
                Nndex = Lndex;
                Lndex = comboBoxColor.FindString(comboBoxColor.Text, Nndex);
            }
            if (listBoxColor.Items.Count > 0)
            {
                listBoxColor.SetBounds(listBoxColor.Location.X, listBoxColor.Location.Y,
                    listBoxColor.Width, listBoxColor.ItemHeight * (listBoxColor.Items.Count + 1));
                panelx.SetBounds(panelx.Location.X, panelx.Location.Y,
                    panelx.Width, listBoxColor.ItemHeight *listBoxColor.Items.Count+8);
                panelx.Show();
                listBoxColor.Show();
            }
            else
            {
                listBoxColor.Hide();
                panelx.Hide();
            }
        }

        private void listBoxColor_SelectedIndexChanged(object sColorer, EventArgs e)
        {
            comboBoxColor.Text = listBoxColor.SelectedItem.ToString();
            panelx.Hide();
            listBoxColor.Hide();
        }

        private void comboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelx.Hide();
            listBoxColor.Hide();
            buttonColor.BackColor = Color.FromName(comboBoxColor.SelectedItem.ToString());
        }

        private void comboBoxStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxStart.Hide();
        }

        private void comboBoxEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxEnd.Hide();
        }

        private void listBoxAddLineStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAddLineStyle.SelectedItem.ToString() == "双叉线") panelBranch.Show();
            else panelBranch.Hide();
        }

        private void buttonAddLine_Click(object sender, EventArgs e)
        {
            panelAddLine.Show();
        }

        private void buttonDeleteLine_Click(object sender, EventArgs e)
        {
            panelAddLine.Hide();
        }

        private void buttonAddStation_Click(object sender, EventArgs e)
        {
            panelAddStation.Show();
        }

        private void buttonDeleteStation_Click(object sender, EventArgs e)
        {
            panelAddStation.Hide();
        }

        private void comboBoxDeleteStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxDeleteStation.Hide();
        }

        private void comboBoxDeleteStation_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxDeleteStation.DroppedDown == true) { listBoxDeleteStation.Hide(); return; }
            if (comboBoxDeleteStation.Text == "") { listBoxDeleteStation.Hide(); return; }
            listBoxDeleteStation.Items.Clear();
            int Lndex, Nndex = -1;
            Lndex = comboBoxDeleteStation.FindString(comboBoxDeleteStation.Text);
            while (Lndex > Nndex)
            {
                listBoxDeleteStation.Items.Add(comboBoxDeleteStation.Items[Lndex]);
                Nndex = Lndex;
                Lndex = comboBoxDeleteStation.FindString(comboBoxDeleteStation.Text, Nndex);
            }
            if (listBoxDeleteStation.Items.Count > 0)
            {
                listBoxDeleteStation.SetBounds(listBoxDeleteStation.Location.X, listBoxDeleteStation.Location.Y,
                    listBoxDeleteStation.Width, listBoxDeleteStation.ItemHeight * (listBoxDeleteStation.Items.Count + 1));
                listBoxDeleteStation.Show();
            }
            else listBoxDeleteStation.Hide();
        }

        private void listBoxDeleteStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDeleteStation.Text = listBoxDeleteStation.SelectedItem.ToString();
            listBoxDeleteStation.Hide();
        }

        private void comboBoxDeleteLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxDeleteLine.Hide();
        }

        private void comboBoxDeleteLine_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxDeleteLine.DroppedDown == true) { listBoxDeleteLine.Hide(); return; }
            if (comboBoxDeleteLine.Text == "") { listBoxDeleteLine.Hide(); return; }
            listBoxDeleteLine.Items.Clear();
            int Lndex, Nndex = -1;
            Lndex = comboBoxDeleteLine.FindString(comboBoxDeleteLine.Text);
            while (Lndex > Nndex)
            {
                listBoxDeleteLine.Items.Add(comboBoxDeleteLine.Items[Lndex]);
                Nndex = Lndex;
                Lndex = comboBoxDeleteLine.FindString(comboBoxDeleteLine.Text, Nndex);
            }
            if (listBoxDeleteLine.Items.Count > 0)
            {
                listBoxDeleteLine.SetBounds(listBoxDeleteLine.Location.X, listBoxDeleteLine.Location.Y,
                    listBoxDeleteLine.Width, listBoxDeleteLine.ItemHeight * (listBoxDeleteLine.Items.Count + 1));
                listBoxDeleteLine.Show();
            }
            else listBoxDeleteLine.Hide();
        }

        private void listBoxDeleteLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDeleteLine.Text = listBoxDeleteLine.SelectedItem.ToString();
            listBoxDeleteLine.Hide();
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            panelLine.Show();
            panelSation.Hide();
        }

        private void buttonStation_Click(object sender, EventArgs e)
        {
            panelSation.Show();
            panelLine.Hide();
        }

        private void panelForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseX = MousePosition.X;
            mouseY = MousePosition.Y;
            if (textBoxStationPosition.Focused == true)
                textBoxStationPosition.Text = "(" + ((int)((mouseX -this.Location.X- offsetX) / stepX)).ToString() +
                    "," + ((int)((mouseY - this.Location.Y - offsetY) / stepY)).ToString() + ")";
        }

        private void panelForm_MouseUp(object sender, MouseEventArgs e)
        {
            if ((MousePosition.X - mouseX) * (MousePosition.X - mouseX) +
                (MousePosition.X - mouseX) * (MousePosition.X - mouseX) < 4) return;
            offsetX = offsetX+ MousePosition.X - mouseX;
            offsetY = offsetY + MousePosition.Y - mouseY;
            panelForm.Refresh();
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            if (zoomRate+1 <= 5)
            {
                offsetX = (int)(510 - (510 - offsetX) * (zoomRate + 1) / zoomRate);
                offsetY = (int)(510 - (510 - offsetY) * (zoomRate + 1) / zoomRate);
                zoomRate++;
                panelForm.Refresh();
            }
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            if (zoomRate -1>=0.4)
            {
                offsetX = (int)(510 - (510 - offsetX) * (zoomRate - 1) / zoomRate);
                offsetY = (int)(510 - (510 - offsetY) * (zoomRate - 1) / zoomRate);
                zoomRate--;
                panelForm.Refresh();
            }
        }

        private void buttonZoomInLittle_Click(object sender, EventArgs e)
        {
            if (zoomRate + 0.2 <= 5)
            {
                offsetX = (int)(510 - (510 - offsetX) * (zoomRate + 0.2) / zoomRate);
                offsetY = (int)(510 - (510 - offsetY) * (zoomRate + 0.2) / zoomRate);
                zoomRate+=0.2f;
                panelForm.Refresh();
            }
        }

        private void buttonZoomOutLittle_Click(object sender, EventArgs e)
        {
            if (zoomRate - 0.2 >= 0.5)
            {
                offsetX = (int)(510 - (510 - offsetX) * (zoomRate - 0.2) / zoomRate);
                offsetY = (int)(510 - (510 - offsetY) * (zoomRate - 0.2) / zoomRate);
                zoomRate -= 0.2f;
                panelForm.Refresh();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void buttonL1_Click(object sender, EventArgs e)
        {            
            DrawLine = 1;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510-(int)(stepX*(sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL2_Click(object sender, EventArgs e)
        {
            DrawLine = 2;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL3_Click(object sender, EventArgs e)
        {
            DrawLine = 3;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL4_Click(object sender, EventArgs e)
        {
            DrawLine = 4;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL5_Click(object sender, EventArgs e)
        {
            DrawLine = 5;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL6_Click(object sender, EventArgs e)
        {
            DrawLine = 6;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL7_Click(object sender, EventArgs e)
        {
            DrawLine = 7;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL8_Click(object sender, EventArgs e)
        {
            DrawLine = 8;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL9_Click(object sender, EventArgs e)
        {
            DrawLine = 9;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL10_Click(object sender, EventArgs e)
        {
            DrawLine = 10;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL11_Click(object sender, EventArgs e)
        {
            DrawLine = 11;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL12_Click(object sender, EventArgs e)
        {
            DrawLine = 12;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL13_Click(object sender, EventArgs e)
        {
            DrawLine = 13;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL14_Click(object sender, EventArgs e)
        {
            DrawLine = 14;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL15_Click(object sender, EventArgs e)
        {
            DrawLine = 15;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL16_Click(object sender, EventArgs e)
        {
            DrawLine = 16;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL17_Click(object sender, EventArgs e)
        {
            DrawLine = 17;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL18_Click(object sender, EventArgs e)
        {
            DrawLine =18;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL19_Click(object sender, EventArgs e)
        {
            DrawLine = 19;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL20_Click(object sender, EventArgs e)
        {
            DrawLine = 20;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL21_Click(object sender, EventArgs e)
        {
            DrawLine = 21;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL22_Click(object sender, EventArgs e)
        {
            DrawLine = 22;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL23_Click(object sender, EventArgs e)
        {
            DrawLine = 23;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL24_Click(object sender, EventArgs e)
        {
            DrawLine = 24;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL25_Click(object sender, EventArgs e)
        {
            DrawLine = 25;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL26_Click(object sender, EventArgs e)
        {
            DrawLine = 26;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL27_Click(object sender, EventArgs e)
        {
            DrawLine = 27;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL28_Click(object sender, EventArgs e)
        {
            DrawLine = 28;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL29_Click(object sender, EventArgs e)
        {
            DrawLine = 29;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL30_Click(object sender, EventArgs e)
        {
            DrawLine = 30;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL31_Click(object sender, EventArgs e)
        {
            DrawLine = 31;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL32_Click(object sender, EventArgs e)
        {
            DrawLine = 32;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL33_Click(object sender, EventArgs e)
        {
            DrawLine = 33;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL34_Click(object sender, EventArgs e)
        {
            DrawLine = 34;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL35_Click(object sender, EventArgs e)
        {
            DrawLine = 35;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL36_Click(object sender, EventArgs e)
        {
            DrawLine = 36;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL37_Click(object sender, EventArgs e)
        {
            DrawLine = 37;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL38_Click(object sender, EventArgs e)
        {
            DrawLine = 38;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL39_Click(object sender, EventArgs e)
        {
            DrawLine = 39;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL40_Click(object sender, EventArgs e)
        {
            DrawLine = 40;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL41_Click(object sender, EventArgs e)
        {
            DrawLine = 41;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL42_Click(object sender, EventArgs e)
        {
            DrawLine = 42;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL43_Click(object sender, EventArgs e)
        {
            DrawLine =43;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL44_Click(object sender, EventArgs e)
        {
            DrawLine = 44;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL45_Click(object sender, EventArgs e)
        {
            DrawLine = 45;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL46_Click(object sender, EventArgs e)
        {
            DrawLine = 46;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL47_Click(object sender, EventArgs e)
        {
            DrawLine = 47;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL48_Click(object sender, EventArgs e)
        {
            DrawLine =48;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
        private void buttonL49_Click(object sender, EventArgs e)
        {
            DrawLine = 49;
            if (sub.LinesArray[DrawLine] == null) return;
            offsetX = 510 - (int)(stepX * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].X
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].X) / 2);
            offsetY = 382 - (int)(stepY * (sub.StationsArray[sub.LinesArray[DrawLine].Stations[0]].Y
                + sub.StationsArray[sub.LinesArray[DrawLine].Stations[sub.LinesArray[DrawLine].Stations.Count - 1]].Y) / 2);
            drawMode = DrawMode.OneLine;
            panelForm.Refresh();
        }
    }
}
