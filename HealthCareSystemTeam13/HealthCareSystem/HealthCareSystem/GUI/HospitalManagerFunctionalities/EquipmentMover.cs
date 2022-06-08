using HealthCareSystem.Core.Rooms.Model;
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
using static HealthCareSystem.Core.Rooms.HospitalEquipment.Model.Equipment;

namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    public partial class EquipmentMover : Form
    {
        private RoomRepository RoomRepository;
        private EquipmentRepository EquipmentRepository;
        public EquipmentMover()
        {
            RoomRepository = new RoomRepository();
            EquipmentRepository = new EquipmentRepository();
            EquipmentRepository.PullEquipment();
            InitializeComponent();
            FillDataGridView();
            FillRoomTypeComboBox();
            FillAmountTypeComboBox();
            FillEquipmentTypeComboBox();
        }
        private void FillRoomTypeComboBox()
        {
            List<string> roomTypes = new List<string>
            {
                "Any",
                TypeOfRoom.DayRoom.ToString(),
                TypeOfRoom.DeliveryRoom.ToString(),
                TypeOfRoom.ExaminationRoom.ToString(),
                TypeOfRoom.IntensiveCareUnit.ToString(),
                TypeOfRoom.NurseryRoom.ToString(),
                TypeOfRoom.OperationRoom.ToString(),
            };
            cmbRoomType.ValueMember = null;
            cmbRoomType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoomType.DataSource = roomTypes;
        }

        private void FillEquipmentTypeComboBox()
        {
            List<string> equipmentTypes = new List<string>
            {
                "Any",
                EquipmentType.Dynamic.ToString(),
                EquipmentType.Static.ToString()
            };
            cmbEquipmentType.ValueMember = null;
            cmbEquipmentType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipmentType.DataSource = equipmentTypes;
        }

        private void FillAmountTypeComboBox()
        {
            List<string> amounts = new List<string>
            {
                "Any",
                "1-10",
                "10+"
            };
            cmbAmount.ValueMember = null;
            cmbAmount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAmount.DataSource = amounts;
        }

        private void FillDataGridView()
        {
            dgwEquipment.DataSource = EquipmentRepository.Equipment;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            dgwEquipment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwEquipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwEquipment.MultiSelect = false;
            dgwEquipment.ReadOnly = true;

        }

        public void RefreshDataGridView()
        {
            
            dgwEquipment.DataSource = RoomRepository.Equipment;
            dgwEquipment.Refresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            EquipmentRepository.PullEquipment();
            RefreshDataGridView();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {

            int roomId = (int)dgwEquipment.SelectedRows[0].Cells[0].Value;
            TypeOfRoom roomType;
            Enum.TryParse((string)dgwEquipment.SelectedRows[0].Cells[1].Value, out roomType);
            int equipmentId = (int)dgwEquipment.SelectedRows[0].Cells[2].Value;
            string equipmentName = (string)dgwEquipment.SelectedRows[0].Cells[3].Value;
            int amount = (int)dgwEquipment.SelectedRows[0].Cells[5].Value;

            MoveEquipmentDialog moveEquipmentView = new MoveEquipmentDialog(roomId, roomType, amount, equipmentId, equipmentName);

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string amount = (string)cmbAmount.SelectedItem;
            string roomType = (string)cmbRoomType.SelectedItem;
            string equipmentType = (string)cmbEquipmentType.SelectedItem;
            
            RoomRepository.PullFoundRows(tbxSearch.Text, amount, roomType, equipmentType);
            RefreshDataGridView();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

        }
    }
}
