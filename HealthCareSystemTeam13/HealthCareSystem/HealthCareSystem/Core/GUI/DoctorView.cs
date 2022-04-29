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

namespace HealthCareSystem
{
    public partial class DoctorView : Form
    {
        public string Username { get; set; }
        public LoginForm AuthForm;
        private DoctorRepository doctorRepository;
        public DoctorView(string username, LoginForm authForm)
        {
            Username = username;
            AuthForm = authForm;
            doctorRepository = new DoctorRepository(username, true);
            InitializeComponent();

            FillDataGridView();

        }

        private void FillDataGridView()
        {

            // dgwExaminations.DataSource = patientRepository.examinations;
            // DataGridViewSettings();
        }

        private void DoctorView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void DoctorView_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
