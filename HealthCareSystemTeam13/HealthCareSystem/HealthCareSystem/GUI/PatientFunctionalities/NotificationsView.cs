using HealthCareSystem.Core.Users.Patients.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Medications.Repository;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class NotificationsView : Form
    {
        public string Username { get; set; }
        private readonly IPatientRepository _patientRepository;
        private readonly MedicationRepository _medicationRepository;
        public NotificationsView(string username)
        {
            InitializeComponent();
            this.Username = username;
            _patientRepository = new PatientRepository(this.Username);
            _medicationRepository = new MedicationRepository();
        }

        private void NotificationsView_Load(object sender, EventArgs e)
        {
            lbNotifications.MaximumSize = new Size(450, 0);
            lbNotifications.AutoSize = true;
            tbNewTime.Text = Convert.ToString(_medicationRepository.GetMedicationNotificationTime(_patientRepository.GetPatientId()));
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string text = tbNewTime.Text.Trim();
            var isNumeric = int.TryParse(text, out int hours);
            if (isNumeric)
            {
                if (hours >= 0)
                {
                    _medicationRepository.SetMedicationNotificationTime(hours, _patientRepository.GetPatientId());
                    MessageBox.Show("Succesfully set new notification time!");
                }
                else
                    MessageBox.Show("Please enter a positive number.");
            }
            else
                MessageBox.Show("Please enter a number.");
        }
    }
}
