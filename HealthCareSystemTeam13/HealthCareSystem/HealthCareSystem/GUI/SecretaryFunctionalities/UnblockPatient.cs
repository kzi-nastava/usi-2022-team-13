using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users.Secretaries.Repository;


namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class UnblockPatient : Form
    {
        SecretaryRepository _secretaryRepository;
        public UnblockPatient()
        {
            InitializeComponent();
            _secretaryRepository = new SecretaryRepository();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string blockedPatientId = patientIdBox.Text;
            _secretaryRepository.DeleteSingleBlockedPatient(blockedPatientId);
            this.Close();
        }
    }
}
