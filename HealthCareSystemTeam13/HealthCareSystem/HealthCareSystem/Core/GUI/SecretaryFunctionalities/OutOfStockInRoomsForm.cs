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
    public partial class OutOfStockInRoomsForm : Form
    {
        SecretaryRepository secretaryRepository;
        string Username;
        public OutOfStockInRoomsForm(string username)
        {
            secretaryRepository = new SecretaryRepository();
            secretaryRepository.CheckDynamicEquipmentRequests();
            secretaryRepository.PullDynamicEquipment();
            InitializeComponent();
            FillDataGridView();
            this.Username = username;
        }
        private void FillDataGridView()
        {
            dynamicEquipmentGrid.DataSource = secretaryRepository.DynamicEquipment;
            DataGridViewSettings();
        }
        private void DataGridViewSettings()
        {
            dynamicEquipmentGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dynamicEquipmentGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dynamicEquipmentGrid.MultiSelect = false;
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dynamicEquipmentGrid.Rows)
            {           
                if (Convert.ToInt32(row.Cells[3].Value) == 0) 
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void transferButton_Click(object sender, EventArgs e)
        {
            int roomsEquipmentId = (int)dynamicEquipmentGrid.SelectedRows[0].Cells[0].Value;
            int roomId = (int)dynamicEquipmentGrid.SelectedRows[0].Cells[1].Value;
            int equipmentID = (int)dynamicEquipmentGrid.SelectedRows[0].Cells[2].Value;
            int amount = (int)dynamicEquipmentGrid.SelectedRows[0].Cells[3].Value;
            RoomHasEquipment roomHasEquipment = new RoomHasEquipment(roomsEquipmentId, equipmentID, roomId, amount);
            TransferForm tf = new TransferForm(roomHasEquipment);
            tf.ShowDialog();
        }
    }
}
