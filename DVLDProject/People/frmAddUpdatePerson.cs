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
using System.Runtime.InteropServices;

namespace DVLDProject.People
{
    public partial class frmAddUpdatePerson : Form
    {

        public event EventHandler<int> DataBack;

        private int _PersonID = -1;
        private clsPerson _Person = new clsPerson();
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

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach (DataRow Row in dtCountries.Rows)
            {
                cbCountry.Items.Add(Row["CountryName"]);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsCountry Country = clsCountry.Find(cbCountry.SelectedItem.ToString());

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.NationalityCountryID = Country.ID;
            _Person.Address = txtAddress.Text.Trim();
            _Person.Gender = rbMale.Checked ? (byte)0 : (byte)1;
            _Person.DateOfBirth = dtDOB.Value;
            _Person.ImagePath = string.IsNullOrWhiteSpace(pbPersonPicture.ImageLocation) ? null : pbPersonPicture.ImageLocation;

            if (_Person.Save())
            {
                lblAddUpdatePerson.Text = "Update Person";
                lblPersonID.Text = _Person.PersonID.ToString();

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Error Saving Data.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _FillCountriesInComboBox();
            cbCountry.SelectedIndex = cbCountry.FindString("Syria");
        }

        private void txtBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox temp = sender as TextBox;

            if (string.IsNullOrWhiteSpace(temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(temp, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(temp, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNationalNo.Text.Trim()))   
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (clsPerson.IsPersonExists(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "A person with this National Number already exists");    
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }
    }
}
