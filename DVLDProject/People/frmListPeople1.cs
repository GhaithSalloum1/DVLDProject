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
using DVLDProject.People.Controls;

namespace DVLDProject.People
{
    public partial class frmListPeople1 : Form
    {

        private static DataTable _dtListPeople = DVLD_Business.clsPerson.GetAllPeople();


        public frmListPeople1()
        {
            InitializeComponent();
        }

        private void _RefreshPeople()
        {
            _dtListPeople = DVLD_Business.clsPerson.GetAllPeople();
            dgvListPeople.DataSource = _dtListPeople;
            lblTotal.Text = dgvListPeople.Rows.Count.ToString() + " Records";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //frmListPeople1.ActiveForm.Close();
        }

        private void bgWorkerListPeople_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = _dtListPeople;
            e.Result = dt;
        }

        private void frmListPeople1_Load(object sender, EventArgs e)
        {
            dgvListPeople.DataSource = _dtListPeople;
            cbFilter.SelectedIndex = 0;
            lblTotal.Text = dgvListPeople.Rows.Count.ToString() + " Records";
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbSearch.Visible = (cbFilter.Text != "None");

            if (txtbSearch.Visible)
            {
                txtbSearch.Text = "";
                txtbSearch.Focus();
            }
        }

        private void txtbSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "Gender";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            DataView dv = new DataView(_dtListPeople);

            if (string.IsNullOrEmpty(txtbSearch.Text) || txtbSearch.Text == "None") 
            {
                dv.RowFilter = "";
            }
            else if (FilterColumn == "PersonID" || FilterColumn == "Gender")
            {
                if (int.TryParse(txtbSearch.Text, out int numericValue))
                {
                    dv.RowFilter = string.Format("{0} = {1}", FilterColumn, numericValue);
                }
                else
                {
                    dv.RowFilter = "1 = 0";
                }
            }
            else
            {
                dv.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtbSearch.Text);
            }

            dgvListPeople.DataSource = dv;
            lblTotal.Text = dgvListPeople.Rows.Count.ToString() + " Records";

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeople();
        }

        private void dgvListPeople_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form frm = new FrmShowPersonInfo((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new FrmShowPersonInfo((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeople();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this person with the ID " + dgvListPeople.CurrentRow.Cells[0].Value + "?", "Delete Person", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                clsPerson.DeletePerson((int)dgvListPeople.CurrentRow.Cells[0].Value);
                MessageBox.Show("Person with ID " + dgvListPeople.CurrentRow.Cells[0].Value + " has been deleted successfully.", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RefreshPeople();
            }
            else
            {
                MessageBox.Show("Deletion cancelled.", "Delete Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeople();
        }

    }
}
