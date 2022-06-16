using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Ingredients.Model;

namespace HealthCareSystem.Core.Medications.Repository
{
    interface IIngredientRepository
    {
        void RemoveIngredient(int ingredientId);
        void InsertIngredient(string ingredientName);
        List<Ingredient> GetIngredients(string query);
        Ingredient GetIngredient(int id);
        bool DoesIngredientExists(string name);
        Ingredient GetSelectedIngredient(string query);
        void PullIngredients();
        DataTable GetIngredients();

    }
}
