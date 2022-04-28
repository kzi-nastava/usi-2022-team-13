using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core;
using HealthCareSystem.Core.Users.Patients.Repository;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class AddEditExamination : Form
    {
        public int ExaminationId { get; set; }
        public int DoctorId { get; set; }
        public DateTime ExaminationDate { get; set; }
        public int roomId { get; set; }
        public int duration { get; set; }
        public bool IsAddChoosen { get; set; }
        private PatientRepository Repository;

        public AddEditExamination(int examinationId, bool isAddChoosen)
        {
            ExaminationId = examinationId;
            IsAddChoosen = isAddChoosen;
            Repository = new PatientRepository("");
            InitializeComponent();

            if (IsAddChoosen)
            {
                LoadEditData();
            }

        }
        private void LoadEditData()
        {
            List<string> data = Repository.GetExamination(ExaminationId);
            foreach(var d in data) Console.WriteLine(d); // printa samo id doktora

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbRoomId_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddEditExamination_Load(object sender, EventArgs e)
        {

        }
    }
}
