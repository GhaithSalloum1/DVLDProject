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
    }
}
