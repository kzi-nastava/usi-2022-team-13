using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Rooms.Renovations.Model;
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
        private RenovationRepository RenovationRepository;
        public AddRenovations()
        {
            RenovationRepository = new RenovationRepository();
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

            string firstQuery = "select * from Rooms where type <> 'Warehouse' and ID not in (select id_room from Examination where isFinished = false) and " +
                "ID not in (select id_room from Renovations where dateOfFinish > #" + DateTime.Now.ToString() + "#) and " +
                "ID not in (select isnull(id_other_room) from Renovations where dateOfFinish > #" + DateTime.Now.ToString() + "#)";
            List<Room> firstRooms = RoomRepository.GetRooms(firstQuery);
            
            cmbFirstRoom.ValueMember = null;
            cmbFirstRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFirstRoom.DataSource = firstRooms;
            cmbFirstRoom.SelectedIndex = -1;
        }

        private void FillSecondRoomComboBox()
        {
            Room selectedFirst = null;
            if (cmbFirstRoom.Text != "") {
                selectedFirst = (Room)cmbFirstRoom.SelectedValue;
                string secondQuery = "select * from Rooms where type <> 'Warehouse' and Id <> " + selectedFirst.ID +
                " and ID not in (select id_room from Examination where isFinished = false) and " +
                "ID not in (select id_room from Renovations where dateOfFinish > #" + DateTime.Now.ToString() + "#) and " +
                "ID not in (select isnull(id_other_room) from Renovations where dateOfFinish > #" + DateTime.Now.ToString() + "#)";
                List<Room> secondRooms = RoomRepository.GetRooms(secondQuery);

                cmbSecondRoom.ValueMember = null;
                cmbSecondRoom.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbSecondRoom.DataSource = secondRooms;
                cmbSecondRoom.SelectedIndex = -1;
            }

            
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeOfRenovation selectedType = TypeOfRenovation.Regular;
            try
            {
                

                if (cmbType.Text != "")
                {
                    selectedType = (TypeOfRenovation)cmbType.SelectedValue;

                }
                else
                {
                    cmbSecondRoom.Enabled = false;
                    cmbSecondRoom.SelectedIndex = -1;
                }

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

            }


          
        }
        private void cmbFirstRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Room selectedFirst = null;
                if(cmbFirstRoom.Text != "")
                {
                    selectedFirst = (Room)cmbFirstRoom.SelectedValue;
                }
               
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
            else if (cmbFirstRoom.SelectedIndex == -1 || cmbType.SelectedIndex == -1 || (cmbSecondRoom.SelectedIndex == -1 && (TypeOfRenovation)cmbType.SelectedValue == TypeOfRenovation.Merging))
            {
                MessageBox.Show("You must select items from every field that is not disabled");
                return false;
            }
           
            return true;
        }


        private void btnCreate_Click(object sender, EventArgs e)
        { 
            if (IsFormValid())
            {
               
                int roomId = ((Room)cmbFirstRoom.SelectedValue).ID;
                DateTime startingDate = dtpDateStart.Value;
                DateTime endingDate = dtpDateEnd.Value;
                int secondRoomId = -1;
                try {
                    if(cmbSecondRoom.Text != "")
                    {
                        secondRoomId = ((Room)cmbSecondRoom.SelectedValue).ID;
                    }

                }
                catch(Exception)
                {
                    
                }
                
                TypeOfRenovation type = (TypeOfRenovation)cmbType.SelectedValue;

                Renovation newRenovation = new Renovation(roomId, startingDate, endingDate, secondRoomId, type);
              
                RenovationRepository.InsertRenovation(newRenovation);
                MessageBox.Show("Succesfully added new renovation");
                this.Hide();
            }
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
