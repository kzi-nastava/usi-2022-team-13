using HealthCareSystem.Core.Rooms.DynamicEqipmentRequests.Model;
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
    public partial class OutOfStockForm : Form
    {
        SecretaryRepository secretaryRepository;
        string Username;
        public OutOfStockForm(string username)
        {
            secretaryRepository = new SecretaryRepository();
            secretaryRepository.CheckDynamicEquipmentRequests();
            secretaryRepository.PullEquipmentInWarehouse();
            InitializeComponent();
            amountBox.Increment = 1;
            amountBox.DecimalPlaces = 0;
            amountBox.Minimum = 0;
            FillDataGridView();
            this.Username = username;
        }
        private void FillDataGridView()
        {
            requestsDataGrid.DataSource = secretaryRepository.EquipmentInWarehouse;
            DataGridViewSettings();
        }
        private void DataGridViewSettings()
        {
            requestsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            requestsDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            requestsDataGrid.MultiSelect = false;
        }

        private void requestButton_Click(object sender, EventArgs e)
        {
            int equipmentID = (int)requestsDataGrid.SelectedRows[0].Cells[0].Value;
            int amount = (int)amountBox.Value;
            int secretaryId = Convert.ToInt32(secretaryRepository.GetUserId(Username));
            DynamicEquipmentRequest request = new DynamicEquipmentRequest(equipmentID, amount, DateTime.Now, secretaryId);
            secretaryRepository.InsertSingleDynamicEquipmentRequest(request);
        }

    }
}
