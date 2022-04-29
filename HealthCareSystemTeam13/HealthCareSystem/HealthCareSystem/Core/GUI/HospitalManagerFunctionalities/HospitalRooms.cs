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
    }
}
