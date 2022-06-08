using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;
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
using HealthCareSystem.Core.Rooms.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class TransferForm : Form
    {
        EquipmentRepository _equipmentRepository;
        RoomHasEquipment OutOfStock;
        public TransferForm(RoomHasEquipment roomHasEquipment)
        {
            _equipmentRepository = new EquipmentRepository();
            _equipmentRepository.CheckDynamicEquipmentRequests();
            _equipmentRepository.PullTransferDynamicEquipment(roomHasEquipment.EquipmentId);
            InitializeComponent();
            amountBox.Increment = 1;
            amountBox.DecimalPlaces = 0;
            amountBox.Minimum = 0;
            FillDataGridView();
            this.OutOfStock = roomHasEquipment;
        }
        private void FillDataGridView()
        {
            equipmentDataGrid.DataSource = _equipmentRepository.TransferDynamicEquipment;
            DataGridViewSettings();
        }
        private void DataGridViewSettings()
        {
            equipmentDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            equipmentDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            equipmentDataGrid.MultiSelect = false;
        }

        private void requestButton_Click(object sender, EventArgs e)
        {
            int amount = (int)amountBox.Value;
            if(amount > (int)equipmentDataGrid.SelectedRows[0].Cells[3].Value)
            {
                MessageBox.Show("Selected amount greater then room has to offer!");
            } 
            else
            {
                int roomHasEquipmentID = (int)equipmentDataGrid.SelectedRows[0].Cells[0].Value;
                _equipmentRepository.UpdateSigleDynamicEquipment(amount, roomHasEquipmentID);
                _equipmentRepository.UpdateSigleDynamicEquipment(amount, OutOfStock);
                Close();
            }
        }
    }
}
