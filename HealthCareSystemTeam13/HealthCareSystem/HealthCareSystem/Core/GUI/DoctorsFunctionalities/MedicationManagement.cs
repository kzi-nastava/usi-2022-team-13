using HealthCareSystem.Core.Medications.Model;
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
        private DoctorRepository DoctorRep;
        private string DoctorUsername;
        public MedicationManagement(string doctorUsername)
        {
            DoctorRep = new DoctorRepository(doctorUsername, true);
            DoctorUsername = doctorUsername;
            DoctorRep.Username = DoctorUsername;
            InitializeComponent();

            FillMedicationComboBox();
        }

        private void FillMedicationComboBox()
        {
            BindingList<Medication> medications = DoctorRep.GetMedications();


            cbDrug.ValueMember = null;
            
            cbDrug.DropDownStyle = ComboBoxStyle.DropDownList;

            cbDrug.DisplayMember = "Name";
            cbDrug.DataSource = medications;

        }

    }
}
