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
using static HealthCareSystem.Core.Rooms.Renovations.Model.Renovation;

namespace HealthCareSystem.Core.GUI.HospitalManagerFunctionalities
{

    
    public partial class AddRenovations : Form
    {
        private RoomRepository RoomRepository;
        public AddRenovations()
        {

            RoomRepository = new RoomRepository();
            InitializeComponent();


            FillRenovationTypeComboBox();
            FillFirstRoomComboBox();

            cmbSecondRoom.Enabled = false;

        }

        private void FillRenovationTypeComboBox()
        {
            List<TypeOfRenovation> renovationTypes = new List<TypeOfRenovation>
            {
                TypeOfRenovation.Regular,
                TypeOfRenovation.Merging,
                TypeOfRenovation.Splitting
            };
            cmbType.ValueMember = null;
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.DataSource = renovationTypes;
            cmbType.SelectedIndex = -1;
        }
        private void FillFirstRoomComboBox()
        {

            string firstQuery = "select * from Rooms where type <> 'Warehouse'";
            List<Room> firstRooms = RoomRepository.GetRooms(firstQuery);

            cmbFirstRoom.ValueMember = null;
            cmbFirstRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFirstRoom.DataSource = firstRooms;
            cmbFirstRoom.SelectedIndex = -1;
        }

        private void FillSecondRoomComboBox()
        {
            Room selectedFirst = (Room)cmbFirstRoom.SelectedItem;
            string secondQuery = "select * from Rooms where type <> 'Warehouse' and Id <> " + selectedFirst.ID; 
            List<Room> secondRooms = RoomRepository.GetRooms(secondQuery);

            cmbSecondRoom.ValueMember = null;
            cmbSecondRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSecondRoom.DataSource = secondRooms;
            cmbSecondRoom.SelectedIndex = -1;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

            TypeOfRenovation selectedType;
            try
            {
                selectedType = (TypeOfRenovation)cmbType.SelectedItem;
                if (selectedType == TypeOfRenovation.Merging)
                {
                    cmbSecondRoom.Enabled = true;
                    FillSecondRoomComboBox();
                }
                else
                {
                    cmbSecondRoom.Enabled = false;
                    cmbSecondRoom.SelectedIndex = -1;
                }
            }
            catch (Exception)
            {
                    cmbSecondRoom.Enabled = false;
                    cmbSecondRoom.SelectedIndex = -1;
            }


          
        }
        private void cmbFirstRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Room selectedFirst = (Room)cmbFirstRoom.SelectedItem;
                FillSecondRoomComboBox();
            }
            catch (Exception)
            {
                
            }
            
        }

        private bool IsFormValid()
        {
            DateTime renovationStart = dtpDateStart.Value;
            DateTime renovationEnd = dtpDateEnd.Value;
            if (renovationEnd <= renovationStart)
            {
                MessageBox.Show("You cannot schedule renovation where end time is behind start time or end time is at the same time start time");
                return false;

            }else if (renovationStart <= DateTime.Now || renovationEnd <= DateTime.Now)
            {
                MessageBox.Show("Both start and end renovation dates must be in the future");
                return false;
            }
            else if (cmbFirstRoom.SelectedIndex == -1 || cmbType.SelectedIndex == -1 || (cmbSecondRoom.SelectedIndex == -1 && (TypeOfRenovation)cmbType.SelectedItem == TypeOfRenovation.Merging))
            {
                MessageBox.Show("You must select items from every field that is not disabled");
                return false;
            }
           
            return true;
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            IsFormValid();
        }

        private void dtpDateEnd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblSecondRoom_Click(object sender, EventArgs e)
        {

        }

        private void lblType_Click(object sender, EventArgs e)
        {

        }

        private void cmbSecondRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpDateStart_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblDateStart_Click(object sender, EventArgs e)
        {

        }

        private void lblDateEnd_Click(object sender, EventArgs e)
        {

        }

        private void lblFirstRoom_Click(object sender, EventArgs e)
        {

        }
    }
}
