﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core
{
    class GUIHelpers
    {
        public static void DataGridViewSettings(DataGridView dgw)
        {
            dgw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgw.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            

            dgw.Columns[0].Width = 90;

            if(dgw.Columns.Count > 3)
            {
                dgw.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgw.Columns[3].Width = 90;
            }
            
            if (dgw.Columns.Count > 4)
            {
                
                dgw.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgw.Columns[4].Width = 90;
            }
            if (dgw.Columns.Count > 5)
            {

                dgw.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgw.Columns[5].Width = 90;
            }
            dgw.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgw.MultiSelect = false;


        }
        public static bool IsDgwRowSelected(DataGridView dgw)
        {
            if (dgw.SelectedRows.Count == 0)
                MessageBox.Show("Please select a row first.");

            else if (dgw.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgw.SelectedRows[0];
                if (row.Cells[0].Value == null)
                    MessageBox.Show("You selected an empty row.");
                else
                    return true;
            }
            else
                MessageBox.Show("Please select only 1 row.");
            return false;
        }
        public static void ButtonEnter(Button button)
        {
            button.BackColor = Color.White;
            button.ForeColor = Color.Black;
        }
        public static void ButtonLeave(Button button)
        {
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.White;
        }
        public static void FillTable(DataTable table, string query, OleDbConnection connection)
        {
            if(connection.State == ConnectionState.Closed) connection.Open();;
            using (var cmd = new OleDbCommand(query, connection))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
        }
    }
}
