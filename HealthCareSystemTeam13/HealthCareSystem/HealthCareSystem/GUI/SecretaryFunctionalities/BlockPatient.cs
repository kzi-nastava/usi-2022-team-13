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
    public partial class BlockPatient : Form
    {
        string Username;
        private readonly IPatientRepository _patientRepository;
        public BlockPatient(string username)
        {
            InitializeComponent();
            _patientRepository = new PatientRepository();
            this.Username = username;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string patientId = patientIdBox.Text;
            _patientRepository.BlockSinglePatient(patientId, Username);
            this.Close();
        }

        private void BlockPatient_Load(object sender, EventArgs e)
        {

        }
    }
}
