using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.TransferHistoryOfEquipment.Model;
using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Scripts.Repository;
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
        public int OriginRoomId { get; set; }
        public int DestinationRoomId { get; set; }
        public TypeOfRoom RoomType { get; set; }
        public int Amount { get; set; }

        public int TrueAmount { get; set; }
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }

        public DateTime TransferDate { get; set; }

        private IRoomRepository _roomRepository;
        private ITransferHistoryRepository _transferHistoryRepository;

        public MoveEquipmentDialog(int roomId, TypeOfRoom roomType, int amount, int equipmentId, string equipmentName)
        {
            this.Amount = amount;
            this.RoomType = roomType;
            this.OriginRoomId = roomId;
            this.EquipmentId = equipmentId;
            this.EquipmentName = equipmentName;

            _transferHistoryRepository = new TransferHistoryRepository();
            _roomRepository = new RoomRepository();

            InitializeComponent();
            FillDestinationComboBox();
            LoadEditData();
        }

        private void LoadEditData()
        {

            lblRoomId.Text = "Selected room id: " + OriginRoomId + "\nSelected room type: " + RoomType.ToString();
            lblEquipmentId.Text = "Selected equipment id: " + EquipmentId + "\nSelected equipment name: " + EquipmentName;

            //From full amount of equipment we subtract amount of those transfers from that room that haven't been realised yet
            string query = "select * from EquipmentTransferHistory where id_original_room = " + OriginRoomId + " and id_equipment = " + EquipmentId + " and isExecuted = false";
            List<TransferHistoryOfEquipment> transferHistory = _transferHistoryRepository.GetTransferHistory(query);
   
            TrueAmount = GetTrueAmount(transferHistory);

            lblTrueAmount.Text = "Max amount for selection is: " + TrueAmount + " (Due to future transfers)";
            nudAmount.Maximum = TrueAmount;
            if (TrueAmount < Amount)
            {
                lblTrueAmount.Visible = true;
            }
        }
        private int GetTrueAmount(List<TransferHistoryOfEquipment> transferHistory)
        {
            int sum = 0;
            foreach (TransferHistoryOfEquipment singleTransfer in transferHistory) sum += singleTransfer.Amount;
            return Amount - sum;
        }

        private void FillDestinationComboBox()
        {
            //query selects every room that is not the origin of equipment
            string query = "select * from Rooms where ID <> " + OriginRoomId;
            List<Room> rooms = _roomRepository.GetRooms(query);

            cmbDestination.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDestination.DataSource = rooms;
            cmbDestination.SelectedIndex = -1;
        }

        private bool IsFormValid()
        {
            DateTime transferDate = dtpExecutionDate.Value;
            if(transferDate <= DateTime.Now)
            {
                MessageBox.Show("You cannot make a transfer with a past date");
                return false;

            }else if(cmbDestination.SelectedIndex == -1){

                MessageBox.Show("You must select destination room");
                return false;

            }else if(TrueAmount == 0){
                MessageBox.Show("You cannot put 0 for the amount");
                return false;
            }
            Room destinationRoom = (Room)cmbDestination.SelectedItem;
            DestinationRoomId = destinationRoom.ID;
            this.TransferDate = transferDate;
            return true;
        }





        private void MoveEquipmentDialog_Load(object sender, EventArgs e)
        {

        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                int amountForTransfer = (int)nudAmount.Value;
                TransferHistoryOfEquipment newTransfer = new TransferHistoryOfEquipment(OriginRoomId, DestinationRoomId,TransferDate, false, amountForTransfer, EquipmentId);
                _transferHistoryRepository.InsertTransferHistoryOfEquipment(newTransfer);
                MessageBox.Show("Succesfully added new tranfer for date: " + TransferDate.ToString());
                this.Hide();
            }
     
        }
    }
}
