using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users.Patients.Repository;
using HealthCareSystem.Core.Users.Secretaries.Repository;


namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class UnblockPatient : Form
    {
        PatientRepository _patientRepository;
        public UnblockPatient()
        {
            InitializeComponent();
            _patientRepository = new PatientRepository();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string blockedPatientId = patientIdBox.Text;
            _patientRepository.DeleteSingleBlockedPatient(blockedPatientId);
            this.Close();
        }
    }
}
