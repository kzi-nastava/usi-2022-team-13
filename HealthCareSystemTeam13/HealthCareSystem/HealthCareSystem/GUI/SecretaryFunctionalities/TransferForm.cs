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

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class TransferForm : Form
    {
        SecretaryRepository _secretaryRepository;
        RoomHasEquipment OutOfStock;
        public TransferForm(RoomHasEquipment roomHasEquipment)
        {
            _secretaryRepository = new SecretaryRepository();
            _secretaryRepository.CheckDynamicEquipmentRequests();
            _secretaryRepository.PullTransferDynamicEquipment(roomHasEquipment.Id);
            InitializeComponent();
            amountBox.Increment = 1;
            amountBox.DecimalPlaces = 0;
            amountBox.Minimum = 0;
            FillDataGridView();
            this.OutOfStock = roomHasEquipment;
        }
        private void FillDataGridView()
        {
            equipmentDataGrid.DataSource = _secretaryRepository.EquipmentInWarehouse;
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
                _secretaryRepository.UpdateSigleDynamicEquipment(amount, roomHasEquipmentID);
                _secretaryRepository.UpdateSigleDynamicEquipment(amount, OutOfStock);
                Close();
            }
        }
    }
}
