﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Ingredients.Model;

namespace HealthCareSystem.Core.Medications.Repository
{
    class IngredientsRepository
    {
        public OleDbConnection Connection { get; set; }
        public DataTable Ingredients { get; private set; }

        public IngredientsRepository()
        {
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Data/HCDb.mdb;
                    Persist Security Info=False;";



            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
        public void PullIngredients()
        {
            Ingredients = new DataTable();
            string ingredientsQuery = "select * from ingredients";
            FillTable(Ingredients, ingredientsQuery);
        }


        public void RemoveIngredient(int ingredientId)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            string query = "delete from ingredients where id = " + ingredientId + "";
            DatabaseCommander.ExecuteNonQueries(query, Connection);
        }

        public void InsertIngredient(string ingredientName)
        {
            var insertQuery = "INSERT INTO ingredients(nameOfIngredient) VALUES(@nameOfIngredient)";
            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@nameOfIngredient", ingredientName);
                cmd.ExecuteNonQuery();

            }
        }


        public List<Ingredient> GetIngredients(string query)
        {
            List<Ingredient> ingredients = new List<Ingredient>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ingredients.Add(new Ingredient(Convert.ToInt32(reader["id"]), reader["nameOfIngredient"].ToString()));

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return ingredients;
        }


        public Ingredient GetIngredient(int id)
        {
            string query = "select * from ingredients where id=" + id;
            Ingredient ingredient = GetSelectedIngredient(query);
            return ingredient;
        }


        public bool DoesIngredientExists(string name)
        {
            string query = "select * from Ingredients where nameOfIngredient='" + name + "'";
            Ingredient ingredient = GetSelectedIngredient(query);

            if (ingredient.Name == name) return false;
            return true;
        }


        public Ingredient GetSelectedIngredient(string query)
        {

            if (Connection.State == ConnectionState.Closed) Connection.Open();
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

            Ingredient ingredient = new Ingredient();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ingredient = new Ingredient(Convert.ToInt32(reader["id"]), reader["nameOfIngredient"].ToString());
            }
            return ingredient;
        }
    }
}
