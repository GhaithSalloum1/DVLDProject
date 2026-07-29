using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using DVLD_Business;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.People
{
    public partial class frmListPeople1 : Form
    {

        private static DataTable _dtListPeople = DVLD_Business.clsPerson.GetAllPeople();




        public frmListPeople1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //frmListPeople1.ActiveForm.Close();
        }

        private void bgWorkerListPeople_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = DVLD_Business.clsPerson.GetAllPeople();
            e.Result = dt;
        }

        private void frmListPeople1_Load(object sender, EventArgs e)
        {
            dgvListPeople.DataSource = _dtListPeople;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
