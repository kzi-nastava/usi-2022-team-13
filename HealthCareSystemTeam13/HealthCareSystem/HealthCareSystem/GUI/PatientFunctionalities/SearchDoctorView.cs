using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
using HealthCareSystem.Core.Users.Doctors.Service;
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
using HealthCareSystem.Core.Users.Doctors;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class SearchDoctorView : Form
    {
        public string Username { get; set; }
        private List<Doctor> _doctors;
        private readonly DoctorRepository _doctorRepository;

        public SearchDoctorView(string username)
        {
            this.Username = username;
            InitializeComponent();
            _doctorRepository = new DoctorRepository();
            SetDgwDoctors();
        }

        private void SetDgwDoctors()
        {
            _doctors = _doctorRepository.GetDoctorsWithAverageRating();

            dgwDoctors.DataSource = _doctors;
            GUIHelpers.DataGridViewSettings(dgwDoctors);
            dgwDoctors.Font = new Font("Lucida Bright", 10);
        }

        private void tbSearchDoctor_TextChanged(object sender, EventArgs e)
        {
            string keyword = tbSearchDoctor.Text.Trim();
            if (keyword != "")
                dgwDoctors.DataSource = DoctorService.GetDoctorsByKeyword(_doctors, keyword.ToLower());
            else
                dgwDoctors.DataSource = _doctors;
        }

        private void btnSortByAverageRating_Click(object sender, EventArgs e)
        {
            _doctors = DoctorSorter.SortDoctors(_doctors, 1);
            dgwDoctors.DataSource = _doctors;
            dgwDoctors.Refresh();
        }

        private void btnSortByName_Click(object sender, EventArgs e)
        {
            _doctors = DoctorSorter.SortDoctors(_doctors, 2);
            dgwDoctors.DataSource = _doctors;
            dgwDoctors.Refresh();
        }

        private void btnSortByLastName_Click(object sender, EventArgs e)
        {
            _doctors = DoctorSorter.SortDoctors(_doctors, 3);
            dgwDoctors.DataSource = _doctors;
            dgwDoctors.Refresh();
        }

        private void btnSortBySpeciality_Click(object sender, EventArgs e)
        {
            _doctors = DoctorSorter.SortDoctors(_doctors, 4);
            dgwDoctors.DataSource = _doctors;
            dgwDoctors.Refresh();
        }

        private void btnAppoint_Click(object sender, EventArgs e)
        {

            if (GUIHelpers.IsDgwRowSelected(dgwDoctors)) {
                int selectedId = (int)dgwDoctors.SelectedRows[0].Cells[0].Value;
                foreach(Doctor dr in _doctors)
                {
                    if(dr.ID == selectedId)
                    {
                        AddEditExamination addEditView = new AddEditExamination(0, true, Username, 1, dr.ID);
                        addEditView.ShowDialog();
                        break;
                    }

                }
            }
        }
    }
}
