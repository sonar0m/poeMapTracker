﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using MovablePython;
using poeMapTracking;
using System.Threading;
using System.Configuration;

namespace poeMapTracking
{
    delegate void SetTextCallback(string text);
    public partial class Offline : Form
    {
        #region maps tracking
        public poeMapTracking.State state;
        public poeMapTracking.MapTracker mapTracker;
        private Thread mapThread;
        #endregion
        #region UI
        public Hotkey hKeys1;
        private bool breakout = false;
        DataSet MapOutput;
        DataGridView dgvMapsOut;
        protected int rowCount = 0;

        public Offline()
        {
            InitializeComponent();
            this.dgvMapsOut = new DataGridView();

            #region data table

            MapOutput = new DataSet();
            MapOutput.Tables.Add("output");
            MapOutput.Tables[0].Columns.Add("row", 1.GetType());
            string temp = ConfigurationManager.AppSettings["seperator"];
            foreach (string colName in ConfigurationManager.AppSettings["outputOrder"].Replace("{", "").Replace("}", "").Split(new string[]{
            ConfigurationManager.AppSettings["seperator"].ToString()
            }, StringSplitOptions.RemoveEmptyEntries))
            {
                MapOutput.Tables[0].Columns.Add(colName, string.Empty.GetType());
            }
            LoadCsv();
            MapOutput.Tables[0].AcceptChanges();
            dgvMapsOut.DataSource = MapOutput.Tables[0];
            DataGridViewTextBoxCell template =  new DataGridViewTextBoxCell();
            template.Style.SelectionForeColor = System.Drawing.Color.Red;
            foreach (DataGridViewColumn col in dgvMapsOut.Columns)
            {
                col.ReadOnly = true;
                col.Width = 2;
            }

            #region style
            dgvMapsOut.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dgvMapsOut.Dock = DockStyle.Bottom;
            dgvMapsOut.Visible = true;
            DataGridViewCellStyle style = dgvMapsOut.RowsDefaultCellStyle;
            style.ForeColor = Color.Black;
            style.SelectionBackColor = Color.Gray;
            dgvMapsOut.AllowUserToAddRows = false;
            dgvMapsOut.ReadOnly = true;
            dgvMapsOut.AllowUserToOrderColumns = true;
            #endregion
            //dgvMapsOut.Show();

            this.panel1.Controls.Add(dgvMapsOut);
            dgvMapsOut.Update();
            #endregion
        }//end constructor

        private void LoadCsv()
        {
            #region CSV file;
            try
            {
                string fileName = ConfigurationManager.AppSettings["outputFile"];
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(fileName);
                    using (file)
                    {
                        string line = file.ReadLine();
                        if (line != ConfigurationManager.AppSettings["outputOrder"].Replace("{","").Replace("}",""))
                            return;
                        while (!file.EndOfStream)
                        {
                            line = file.ReadLine();
                            line = rowCount.ToString() + ConfigurationManager.AppSettings["seperator"] + line;
                            rowCount++;
                            MapOutput.Tables[0].Rows.Add(line.Split(new string[] { ConfigurationManager.AppSettings["seperator"] }, StringSplitOptions.None));
                        }
                    }
                }
            }
            catch
            {
            }
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region hot Keys
            hKeys1 = new Hotkey();
            string sKey = ConfigurationManager.AppSettings["key"];
            if (sKey == null)
            {
                try
                {
                    System.Configuration.Configuration config =
                     ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings.Add("key", Keys.F5.ToString());
                    config.Save(ConfigurationSaveMode.Minimal, false);
                    ConfigurationManager.RefreshSection("appSettings");
                }
                catch { }
                sKey = Keys.F5.ToString();
            }
            KeysConverter convert = new KeysConverter();
            try
            {
                if (sKey.Length == 1)
                    sKey = sKey.ToUpper();
                hKeys1.KeyCode = (Keys)convert.ConvertFromString(sKey.ToUpper());
            }
            catch { }
            sKey = ConfigurationManager.AppSettings["shift"];
            if (sKey != null)
                hKeys1.Shift = sKey.ToLower() == "true";
            sKey = ConfigurationManager.AppSettings["alt"];
            if (sKey != null)
                hKeys1.Alt = sKey.ToLower() == "true";
            sKey = ConfigurationManager.AppSettings["control"];
            if (sKey != null)
                hKeys1.Control = sKey.ToLower() == "true";
            hKeys1.Pressed += delegate
            {
                switch (mapTracker.state)
                {
                    case (State.waiting):
                        mapTracker.state = State.gettingMap;
                        break;
                    case (State.gettingMap):
                        if (mapTracker.runningMap.mapTier > 0)
                            mapTracker.state = State.running;
                        break;
                    case (State.running):
                        mapTracker.state = State.stopped;
                        break;
                    default:
                        mapTracker = new MapTracker();
                        mapTracker.state = State.waiting;
                        break;
                }
                setStateText("State: " + mapTracker.state);
            };
            if (!hKeys1.GetCanRegister(this)) { Console.WriteLine("Whoops, looks like attempts to register will fail or throw an exception, show an error/visual user feedback"); }
            else
            { hKeys1.Register(this); }

            #endregion
            #region map Tracker
            mapTracker = new MapTracker();
            this.mapThread = new Thread(startMap);
            this.mapThread.TrySetApartmentState(ApartmentState.STA);
            this.mapThread.Start();

            #endregion
        }//end load

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            this.dgvMapsOut.Height = this.panel1.Size.Height - 10;
            this.dgvMapsOut.Width = this.panel1.Size.Width - 10;
        }

        private void setStateText(string text)
        {
            this.StateText.Text = text;
        }

        private void setmt(string text)
        {
            string[] s = text.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            Label[] mt = new Label[] { this.mt1, mt2, mt3, mt4, mt5, mt6, mt7, mt8, mt9, mt10, mt11, mt12, mt13, mt14, mt15 };
            for (int i = 0; i < mt.Length; i++)
            {
                mt[i].Text = s[i];
                mt[i].Update();
            }
        }

        private void setOutputText(string text)
        {
            text = rowCount + ConfigurationManager.AppSettings["seperator"] + text;
            string[] s = text.Split(new string[] { ConfigurationManager.AppSettings["seperator"] }, StringSplitOptions.RemoveEmptyEntries);
            MapOutput.Tables[0].Rows.Add(s);//add the data  to the data grid
            MapOutput.Tables[0].AcceptChanges();
        }

        [STAThread]
        public void startMap()
        {
            string clipboard = string.Empty;
            do
            {
                string countOut = "";
                try
                {
                    countOut = "";
                    int[] count = this.mapTracker.getTierCount();
                    for (int i = 0; i < count.Length; i++)
                    {
                        countOut += string.Format("|, {0}:{1}", new string[] { (i + 1).ToString(), count[i].ToString() });
                    }
                    SetTextCallback m = new SetTextCallback(setmt);
                    try
                    {
                        this.Invoke(m, new object[] { countOut });
                    }
                    catch { }
                    if (countOut.Length > 2)
                        countOut = countOut.Substring(2);
                    this.StateText.Text = "State: " + this.mapTracker.state.ToString() + "       Count:";
                }// end try
                catch
                {
                    if (this.StateText.InvokeRequired)
                    {
                        SetTextCallback m = new SetTextCallback(setStateText);
                        try
                        {
                            this.Invoke(m, new object[] { "State: " + this.mapTracker.state.ToString() + "\t Count:" });
                        }
                        catch { }
                    }
                }
                #region state
                clipboard = Clipboard.GetText();
                if (!Map.isMap(clipboard) && mapTracker.state != State.stopped)
                    continue;
                switch (mapTracker.state)
                {
                    case State.gettingMap://complete setting map over ride if repeated??
                        doGettingMap(clipboard);
                        break;
                    case State.running://needs to be idented?? clear clipboard
                        doRunning(clipboard);
                        break;
                    case State.waiting://waiting to start exaimin info
                        doWaiting(clipboard);
                        break;
                    case State.stopped://write data go to waiting after wards
                        doStopped(clipboard);
                        break;
                    default:
                        break;
                }
                #endregion
            } while (!breakout);
        }//end startMap()
        #region State
        private void doStopped(string clipboard)
        {
            SetTextCallback m = new SetTextCallback(setOutputText);
            this.Invoke(m, new object[] { mapTracker.ToString() });
            writeTracking(mapTracker);
            mapTracker = new MapTracker();
        }

        private static void doWaiting(string clipboard)
        {
            //if (Map.isMap(clipboard))
            //    mapTracker.state = State.gettingMap;
            //if (Map.isMap(clipboard))
            //    Clipboard.Clear();
        }

        private void doRunning(string clipboard)
        {
            Map map = new Map(clipboard);
            mapTracker.mapsOutput.Add(map);
            Clipboard.Clear();
        }

        private void doGettingMap(string clipboard)
        {
            mapTracker.runningMap = new Map(clipboard);
            Clipboard.Clear();// clear the clipboard
        }
        #endregion
        private static void writeTracking(MapTracker mapTracker)
        {
            string fileName = ConfigurationManager.AppSettings["outputFile"];
            if (fileName == string.Empty)
                throw new System.IO.FileLoadException("could not find outputFile");
            if (!System.IO.File.Exists(fileName))
            {
                try
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
                    file.WriteLine(ConfigurationManager.AppSettings["outputOrder"].Replace("{", "").Replace("}", ""));
                    file.WriteLine(mapTracker.ToString());
                    file.Close();
                }
                catch { }
            }
            else
            {
                try
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true);
                    file.WriteLine(mapTracker.ToString());
                    file.Close();
                }
                catch { }
            }
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            this.breakout = true;
            this.Dispose();
            Thread.Sleep(1000);
            // .. later, at some point
            if (hKeys1.Registered)
            { hKeys1.Unregister(); }
        }


        private void ClrMaps_Click(object sender, EventArgs e)
        {
            this.mapTracker.mapsOutput = new List<Map>();
        }

        private void clrRun_Click(object sender, EventArgs e)
        {
            this.mapTracker = new MapTracker();
            this.mapTracker.state = State.waiting;
        }
        #endregion

        private void statuspanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mt_Click(object sender, EventArgs e)
        {
            if (((Label)sender).Name.StartsWith("mt"))
            {
                int i = 0;
                int.TryParse(((Label)sender).Name.Substring(2), out i);
                try
                {
                    this.mapTracker.clearTier(i);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
