using HealthCareSystem.Core.GUI.DoctorsFunctionalities;
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
        private DoctorRepository DoctorRep;
        public DoctorView(string username, LoginForm authForm)
        {
            Username = username;
            AuthForm = authForm;
            DoctorRep = new DoctorRepository(username, true);
            DoctorRep.PullExaminations();
            InitializeComponent();

            FillDataGridView();

        }

        private void FillDataGridView()
        {

            dgwExaminations.DataSource = DoctorRep.examinations;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            dgwExaminations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwExaminations.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwExaminations.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwExaminations.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwExaminations.Columns[0].Width = 90;
            dgwExaminations.Columns[3].Width = 90;
            dgwExaminations.Columns[4].Width = 90;
            dgwExaminations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwExaminations.MultiSelect = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InsertOrChangeExamination InsertOrChangeExaminationView = new InsertOrChangeExamination(
                (int)dgwExaminations.SelectedRows[0].Cells[0].Value, true, Username, 1);

            InsertOrChangeExaminationView.ShowDialog();


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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        public void RefreshDataGridView()
        {
            DoctorRep.PullExaminations();
            dgwExaminations.DataSource = DoctorRep.examinations;
            dgwExaminations.Refresh();
        }
    }
}
