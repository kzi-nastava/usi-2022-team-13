using HealthCareSystem.Core.Medications.Model;
using HealthCareSystem.Core.Medications.Repository;
using HealthCareSystem.Core.Users.Doctors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core.GUI.DoctorsFunctionalities
{
    public partial class MedicationManagement : Form
    {
        private DoctorRepository _doctorRep;
        private MedicationRepository _medicationRep;
        public string DoctorUsername;
        public MedicationManagement(string doctorUsername)
        {
            _doctorRep = new DoctorRepository(doctorUsername, true);
            DoctorUsername = doctorUsername;
            _doctorRep.Username = DoctorUsername;
            _medicationRep = new MedicationRepository();
            InitializeComponent();

            FillMedicationComboBox();
        }

        private void FillMedicationComboBox()
        {
            BindingList<Medication> medications = _medicationRep.GetMedications();


            cbDrug.ValueMember = null;
            
            cbDrug.DropDownStyle = ComboBoxStyle.DropDownList;

            cbDrug.DisplayMember = "Name";
            cbDrug.DataSource = medications;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbAllowDrug_CheckedChanged(object sender, EventArgs e)
        {
            if(!rbAllowDrug.Checked)
            {
                rtbDrug.Enabled = true;
            }
            else
            {
                rtbDrug.Enabled = false;
            }
        }

        private void rbReturnDrug_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnDrugUsage_Click(object sender, EventArgs e)
        {

            Medication selectedMedication = (Medication)cbDrug.SelectedValue;
            if(rbAllowDrug.Checked)
            {
                string updateQuery = "update medications set status = 'Approved' where id = " + selectedMedication.Id ;
                _medicationRep.UpdateMedication(updateQuery);

                MessageBox.Show("Successfully allowed the medication!");

            } else if (rbReturnDrug.Checked)
            {
                string reasonForDenying = rtbDrug.Text;
                string updateQuery = "update medications set status = 'Denied' where id = " + selectedMedication.Id;
                _medicationRep.UpdateMedication(updateQuery);


                int doctorId = _doctorRep.GetDoctorId();
                string insertQuery = "insert into RejectedMedications (id_medication, id_doctor, description)" +
                    " values (" + selectedMedication.Id + ", " + doctorId + ", '" + reasonForDenying + "')";

                _medicationRep.InsertRejectedMedication(reasonForDenying, selectedMedication.Id, doctorId);
                
                MessageBox.Show("Successfully rejected the medication!");
            }
            else
            {
                MessageBox.Show("You need to select one option!");
                return;
            }
            this.Close();
        }
    }
}
