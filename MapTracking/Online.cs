using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace poeMapTracking
{
    public partial class Online : Form
    {
        FileSystemWatcher logWatch;
        public Online()
        {
            logWatch = new FileSystemWatcher();
            InitializeComponent();
        }

        private void findConfig_Click(object sender, EventArgs e)
        {
            this.OpenLog.Multiselect = false;
            DialogResult dr =  this.OpenLog.ShowDialog();
            if (this.OpenLog.FileName != null || this.OpenLog.FileName != "")
            {
                this.tbClientLocation.Text = this.OpenLog.FileName;
                FileInfo file = new FileInfo(this.tbClientLocation.Text);
                logWatch.Path = file.Directory.FullName;
                logWatch.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                logWatch.Filter = file.Name;
                //logWatch.Changed += new FileSystemEventArgs(OnChange);
            }
        }//end findcfg
        private static void OnChange(object source, FileSystemEventArgs e)
        { 
            //StreamReader sr = new StreamReader(e.FullPath);
            //string[] file = sr.ReadToEnd().Split(new string[]{Environment.NewLine},StringSplitOptions.None);
        }
    }
}
