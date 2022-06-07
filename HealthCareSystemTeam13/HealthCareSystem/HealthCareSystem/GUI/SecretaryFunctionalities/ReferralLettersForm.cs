using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Secretaries.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users.Doctors.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class ReferralLettersForm : Form
    {
        ReferralLetterRepository _referralLetterRepository;
        public ReferralLettersForm()
        {
            _referralLetterRepository = new ReferralLetterRepository();
            _referralLetterRepository.PullReferralLetters();
            InitializeComponent();
            FillDataGridView();
        }
        private void FillDataGridView()
        {
            lettersDataGrid.DataSource = _referralLetterRepository.ReferralLetters;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            lettersDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lettersDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lettersDataGrid.MultiSelect = false;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            int patientID = (int)lettersDataGrid.SelectedRows[0].Cells[2].Value;
            int currentDoctorID = (int)lettersDataGrid.SelectedRows[0].Cells[1].Value;
            int forwardedDoctorID = (int)lettersDataGrid.SelectedRows[0].Cells[3].Value;
            DoctorSpeciality speciality;
            Enum.TryParse<DoctorSpeciality>((string)lettersDataGrid.SelectedRows[0].Cells[5].Value, out speciality);
            TypeOfExamination examinationType;
            Enum.TryParse<TypeOfExamination>((string)lettersDataGrid.SelectedRows[0].Cells[5].Value, out examinationType);
            ReferralLetter referralLetter = new ReferralLetter(currentDoctorID, patientID, forwardedDoctorID, examinationType, speciality);
            AddExamination addExaminationForm = new AddExamination(referralLetter);
            _referralLetterRepository.DeleteSingleReferralLetter(Convert.ToString(lettersDataGrid.SelectedRows[0].Cells[0].Value));
            addExaminationForm.ShowDialog();
        }
    }
}
