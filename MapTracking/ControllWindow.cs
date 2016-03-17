using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poeMapTracking
{
    public partial class ControllWindow : Form
    {
        bool online;
        bool toClear = false;
        Form active;
        public ControllWindow()
        {
            InitializeComponent();
            online = false;
        }

        private void closedChild()
        {
            active = null;
        }
        private void Offline_Click(object sender, EventArgs e)
        {
            if (active!=null)
                return;
            online = false;
            toClear = false;
            active = new poeMapTracking.Offline();
            active.FormClosed += delegate { closedChild(); };
            active.Show();
        }

        private void OnlineButton_Click(object sender, EventArgs e)
        {
            if (active != null)
                return;
            online = true;
            toClear = false;
            active = new poeMapTracking.Online();
            active.FormClosed += delegate { closedChild(); };
            active.Show();
        }
    }
}
