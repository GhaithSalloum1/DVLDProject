using DVLD_Business;
using DVLDProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {

        private clsPerson _Person;

        private int _PersonID;
        public int PersonID
        {
            get { return _PersonID; }
        }


        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void _FillPersonInfo()
        {
            lblPersonID.Text = _Person.PersonID.ToString();
            llEditPersonInfo.Enabled = true;
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            pbGenderPic.Image = _Person.Gender == 0 ? Resources.Man_32 : Resources.Woman_32;
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDOB.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            // _LoadPersonImage(); a method I'll implement later to load the person's image if available
        }

        public void LoadPerson(int PersonID)
        {
            if (clsPerson.IsPersonExists(PersonID))
            {
                _Person = clsPerson.Find(PersonID);
                _FillPersonInfo();
            }
            else
            {
                _ResetPersonInfo();
                MessageBox.Show("Person with PersonID " + PersonID + "Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadPerson(string NationalNo)
        {
            if (clsPerson.IsPersonExists(NationalNo))
            {
                _Person = clsPerson.Find(NationalNo);
                _FillPersonInfo();
            }
            else
            {
                _ResetPersonInfo();
                MessageBox.Show("Person with NationalNo " + NationalNo + "Not Found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _ResetPersonInfo()
        {
            _PersonID = -1;

            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblName.Text = "[????]";
            pbGenderPic.Image = Resources.Man_32;
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDOB.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbImage.Image = Resources.Male_512; // Person Image
        }
    }
}
