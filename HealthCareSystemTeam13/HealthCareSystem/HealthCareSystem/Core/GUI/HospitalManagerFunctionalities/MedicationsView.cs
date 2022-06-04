using HealthCareSystem.Core.Medications.Model;
using HealthCareSystem.Core.Rooms.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    public partial class MedicationsView : Form
    {
        
        private RoomRepository RoomRepository;
        public MedicationsView()
        {
            RoomRepository = new RoomRepository();
            RoomRepository.PullMedications();
            InitializeComponent();
            FillDataGridView();
            btnEdit.Enabled = false;
        }

        private void FillDataGridView()
        {
            dgwMedications.DataSource = RoomRepository.Medications;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            dgwMedications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwMedications.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwMedications.MultiSelect = false;
        }

        public void RefreshDataGridView()
        {
            RoomRepository.PullMedications();
            dgwMedications.DataSource = RoomRepository.Medications;
            dgwMedications.Refresh();
        }

        private void dgwMedications_SelectionChanged(object sender, EventArgs e)
        {
            if(!(dgwMedications.SelectedRows.Count <= 0) && !(dgwMedications.SelectedRows[0].Cells[2].Value == DBNull.Value))
            { 
                if ((string)dgwMedications.SelectedRows[0].Cells[2].Value == "Denied")
                {
                    btnEdit.Enabled = true;
                }
                else
                {
                    btnEdit.Enabled = false;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditMedication addEditView = new AddEditMedication((int)dgwMedications.SelectedRows[0].Cells[0].Value, true);

            addEditView.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int medicationId = (int)dgwMedications.SelectedRows[0].Cells[0].Value;

            AddEditMedication addEditView = new AddEditMedication(medicationId, false);
            addEditView.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
    }
}
