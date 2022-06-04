using HealthCareSystem.Core.Ingredients.Model;
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
    public partial class IngredientsView : Form
    {
        private RoomRepository RoomRepository;
        public IngredientsView()
        {
            RoomRepository = new RoomRepository();
            RoomRepository.PullIngredients();
            InitializeComponent();
            FillDataGridView();
        }

        private void FillDataGridView()
        {
            dgwIngredients.DataSource = RoomRepository.Ingredients;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            dgwIngredients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwIngredients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwIngredients.MultiSelect = false;
        }

        public void RefreshDataGridView()
        {
            RoomRepository.PullIngredients();
            dgwIngredients.DataSource = RoomRepository.Ingredients;
            dgwIngredients.Refresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditIngredients addEditView = new AddEditIngredients((int)dgwIngredients.SelectedRows[0].Cells[0].Value, true);

            addEditView.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ingredientId = (int)dgwIngredients.SelectedRows[0].Cells[0].Value;
            Ingredient ingredient = RoomRepository.GetIngredient(ingredientId);


            AddEditIngredients addEditView = new AddEditIngredients(ingredientId, false);
            addEditView.ShowDialog();

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            int ingredientId = (int)dgwIngredients.SelectedRows[0].Cells[0].Value;
            Ingredient ingredient = RoomRepository.GetIngredient(ingredientId);


            DialogResult wantToCancel = MessageBox.Show("Are you sure?", "Cancel removing a ingredient", MessageBoxButtons.YesNo);

            if (wantToCancel == DialogResult.Yes)
            {
                RoomRepository.RemoveIngredient((int)dgwIngredients.SelectedRows[0].Cells[0].Value);
                MessageBox.Show("Succesfully removed a ingredient!");
                RefreshDataGridView();
            }
        } 
    }
}
