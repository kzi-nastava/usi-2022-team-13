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

            if (!IsAddChosen)
            {
                LoadEditData();
            }
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

        private void LoadEditData()
        {

            string query = "select * from Rooms where id=" + RoomId;
            Room room = RoomRep.GetSelectedRoom(query);
            cbRoomTypes.SelectedIndex = cbRoomTypes.FindStringExact(room.Type.ToString());
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
                SelectedRoomType = (TypeOfRoom)cbRoomTypes.SelectedValue;

                
                string updateQuery = "Update Rooms set type = '" + SelectedRoomType.ToString() + "' where id = " + RoomId;

                DatabaseCommander.ExecuteNonQueries(updateQuery, RoomRep.Connection);
                MessageBox.Show("Successfully edited room!");
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
