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
    public partial class Renovations : Form
    {
        private RoomRepository RoomRepository;
        public Renovations()
        {
            RoomRepository = new RoomRepository();
            RoomRepository.PullRenovations();
            InitializeComponent();
            FillDataGridView();
        }

        private void FillDataGridView()
        {

            dgwRenovations.DataSource = RoomRepository.Renovations;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            dgwRenovations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgwRenovations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwRenovations.MultiSelect = false;


        }

        public void RefreshDataGridView()
        {
            RoomRepository.PullRenovations();
            dgwRenovations.DataSource = RoomRepository.Renovations;
            dgwRenovations.Refresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void btnNewRenovation_Click(object sender, EventArgs e)
        {
            AddRenovations addRenovationsView = new AddRenovations();

            addRenovationsView.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertionRepository a = new InsertionRepository();
            a.UpdateRenovations();
        }
    }
}
