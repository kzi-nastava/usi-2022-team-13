using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.TransferHistoryOfEquipment.Model;
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

namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{
    public partial class MoveEquipmentDialog : Form
    {
        public int RoomId { get; set; }
        public TypeOfRoom RoomType { get; set; }
        public int Amount { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        
        private RoomRepository RoomRepository;

        public MoveEquipmentDialog(int roomId, TypeOfRoom roomType, int amount, int equipmentId, string equipmentName)
        {
            this.Amount = amount;
            this.RoomType = roomType;
            this.RoomId = roomId;
            this.EquipmentId = equipmentId;
            this.EquipmentName = equipmentName;
            
            RoomRepository = new RoomRepository();

            InitializeComponent();
            FillDestinationComboBox();
            LoadEditData();
        }

        private void LoadEditData()
        {

            lblRoomId.Text = "Selected room id: " + RoomId + "\nSelected room type: " + RoomType.ToString();
            lblEquipmentId.Text = "Selected equipment id: " + EquipmentId + "\nSelected equipment name: " + EquipmentName;

            //From full amount of equipment we subtract amount of those transfers from that room that haven't been realised yet
            string query = "select * from EquipmentTransferHistory where id_original_room = " + RoomId + " and id_equipment = " + EquipmentId;
            List<TransferHistoryOfEquipment> transferHistory = RoomRepository.GetTransferHistory(query);
            int trueAmount = Amount - transferHistory.Count;
            lblTrueAmount.Text = trueAmount.ToString();
            
            nudAmount.Maximum = Amount;
        }

        private void FillDestinationComboBox()
        {
            //query selects every room that is not the origin of equipment
            string query = "select * from Rooms where ID <> " + RoomId;
            List<Room> rooms = RoomRepository.GetRooms(query);

            cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDestination.DataSource = rooms;
            cmbDestination.SelectedIndex = -1;
        }

        private void MoveEquipmentDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
