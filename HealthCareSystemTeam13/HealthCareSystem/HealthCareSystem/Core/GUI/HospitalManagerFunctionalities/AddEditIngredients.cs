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
    public partial class AddEditIngredients : Form
    {
        public int IngredientId { get; set; }
        public bool IsAddChosen { get; set; }
        private RoomRepository RoomRep;
        private String IngredientName;

        public AddEditIngredients(int ingredientId, bool isAddChoosen)
        {

            IngredientId = ingredientId;
            IsAddChosen = isAddChoosen;
            RoomRep = new RoomRepository();
            InitializeComponent();

            if (!IsAddChosen)
            {
                LoadEditData();
            }
        }

        private void LoadEditData()
        {
            string query = "select * from ingredients where id=" + IngredientId;
            Ingredient ingredient = RoomRep.GetSelectedIngredient(query);
            tbIngredient.Text = ingredient.Name;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsAddChosen)
            {
                IngredientName = tbIngredient.Text;
                Console.WriteLine(IngredientName);
                if (!RoomRep.DoesIngredientExists(IngredientName))
                {
                    MessageBox.Show("There already exists ingredient with this name!");
                    this.Close();
                    return;
                    
                }


                RoomRep.InsertIngredient(IngredientName);
                MessageBox.Show("Successfully added an ingredient!");

            }
            else
            {
                IngredientName = tbIngredient.Text;
                if (!RoomRep.DoesIngredientExists(IngredientName))
                {
                    MessageBox.Show("There already exists ingredient with this name!");
                    this.Close();
                    return;
                }

                string updateQuery = "Update Ingredients set nameOfIngredient = '" + IngredientName + "' where id = " + IngredientId;
                RoomRep.UpdateContent(updateQuery);
                MessageBox.Show("Successfully edited an ingredient!");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
