using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_Business;
using System.Windows.Forms;

namespace DVLDProject.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public frmAddUpdatePerson()
        {
            InitializeComponent();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            pbPersonPicture.Image = Properties.Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pbPersonPicture.Image = Properties.Resources.Female_512;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
        }
    }
}
