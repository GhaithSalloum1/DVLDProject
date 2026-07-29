using DVLDProject.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void tsPeople_Click(object sender, EventArgs e)
        {
            Form frm = new frmListPeople1();
            frm.ShowDialog();
        }
    }
}
