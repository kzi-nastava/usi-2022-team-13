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
    public partial class AddEditRooms : Form
    {
        public int RoomId { get; set; }
        public bool IsAddChosen { get; set; }
        private RoomRepository RoomRep;
        private TypeOfRoom SelectedRoomType;
        public AddEditRooms(int roomId, bool isAddChoosen)
        {

            RoomId = roomId;
            IsAddChosen = isAddChoosen;
            RoomRep = new RoomRepository();
            InitializeComponent();

            FillRoomTypeComboBox();
        }
        private void FillRoomTypeComboBox()
        {
            List<TypeOfRoom> roomTypes = new List<TypeOfRoom>
            {
                TypeOfRoom.DayRoom,
                TypeOfRoom.DeliveryRoom,
                TypeOfRoom.ExaminationRoom,
                TypeOfRoom.IntensiveCareUnit,
                TypeOfRoom.NurseryRoom,
                TypeOfRoom.OperationRoom,
            };
            cbRoomTypes.ValueMember = null;
            cbRoomTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRoomTypes.DisplayMember = "RoomType";
            cbRoomTypes.DataSource = roomTypes; 
        }


        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsAddChosen)
            {
                SelectedRoomType = (TypeOfRoom)cbRoomTypes.SelectedValue;
                RoomRep.InsertRoom(SelectedRoomType);
                MessageBox.Show("Successfully added a room!");

            }
            else
            {
                //UpdateContent(mergedTime);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbRoomTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
