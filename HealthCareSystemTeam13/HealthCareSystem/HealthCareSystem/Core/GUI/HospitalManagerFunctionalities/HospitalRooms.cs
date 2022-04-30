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
    public partial class HospitalRooms : Form
    {
        private RoomRepository roomRepository;
        public HospitalRooms()
        {
            roomRepository = new RoomRepository();
            roomRepository.PullRooms();
            InitializeComponent();
            FillDataGridView();
        }

        private void FillDataGridView()
        {

            dgwHospitalRooms.DataSource = roomRepository.Rooms;
            DataGridViewSettings();


        }
        private void DataGridViewSettings()
        {
            dgwHospitalRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        
            dgwHospitalRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwHospitalRooms.MultiSelect = false;


        }

        private bool CanChangeRoom()
        {
            if (dgwHospitalRooms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.");

            }
            else if (dgwHospitalRooms.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgwHospitalRooms.SelectedRows[0];
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
        public void RefreshDataGridView()
        {
            roomRepository.PullRooms();
            dgwHospitalRooms.DataSource = roomRepository.Rooms;
            dgwHospitalRooms.Refresh();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            int roomId = (int)dgwHospitalRooms.SelectedRows[0].Cells[0].Value;
            Room room = roomRepository.GetRoom(roomId);

            if(room.Type.ToString().Equals("Warehouse"))
            {
                MessageBox.Show("You cannot remove a warehouse");

            }
            else if (roomRepository.DoesRoomHaveFutureExaminations(room))
            {
                MessageBox.Show("You cannot remove a room that will have future examinations in it");
            }
            else
            {
                DialogResult wantToCancel = MessageBox.Show("Are you sure?", "Cancel removing a room", MessageBoxButtons.YesNo);

                if (wantToCancel == DialogResult.Yes)
                {
                        roomRepository.RemoveRoom((int)dgwHospitalRooms.SelectedRows[0].Cells[0].Value);
                        MessageBox.Show("Succesfully removed a room!");
                        RefreshDataGridView();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditRooms addEditView = new AddEditRooms((int)dgwHospitalRooms.SelectedRows[0].Cells[0].Value, true);

            addEditView.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int roomId = (int)dgwHospitalRooms.SelectedRows[0].Cells[0].Value;
            Room room = roomRepository.GetRoom(roomId);

            if (room.Type.ToString().Equals("Warehouse"))
            {
                MessageBox.Show("You cannot edit a warehouse");

            }
            else if (roomRepository.DoesRoomHaveFutureExaminations(room))
            {
                MessageBox.Show("You cannot edit a room that will have future examinations in it");
            }
            else
            {
                AddEditRooms addEditView = new AddEditRooms(roomId, false);
                addEditView.ShowDialog();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
    }
}
