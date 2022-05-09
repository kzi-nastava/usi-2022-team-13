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
    public partial class EquipmentMover : Form
    {
        private RoomRepository RoomRepository;
        public EquipmentMover()
        {
            RoomRepository = new RoomRepository();
            RoomRepository.PullEquipment();
            InitializeComponent();
            FillDataGridView();
        }
        private void FillDataGridView()
        {
            dgwEquipment.DataSource = RoomRepository.Equipment;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            dgwEquipment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwEquipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwEquipment.MultiSelect = false;

        }

        public void RefreshDataGridView()
        {
            RoomRepository.PullEquipment();
            dgwEquipment.DataSource = RoomRepository.Equipment;
            dgwEquipment.Refresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            MoveEquipmentDialog moveEquipmentView = new MoveEquipmentDialog();

            moveEquipmentView.ShowDialog();
        }

        private bool CanChangeRoom()
        {
            if (dgwEquipment.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.");

            }
            else if (dgwEquipment.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgwEquipment.SelectedRows[0];
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("You selected an empty row.");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Please select only 1 row.");
            }
            return false;
        }

    }
}
