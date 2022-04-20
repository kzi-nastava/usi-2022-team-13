using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using HealthCareSystem.Core.Scripts.Repository;

namespace HealthCareSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = tbUsername.Text;
            string password = tbPassword.Text;

            int user = CheckLoginInput(username, password);

        }

        private static int CheckLoginInput(string username, string password)
        {
            /*
                Checks if the input is valid, if it is check with database, if that's valid
                then proceed with login.
                
            */
            bool isValid =  IsValidInput(username, password);

            return 1;

        }

        private static bool IsValidInput(string username, string password)
        {
            if (username == "" || password == "" || username.Contains(" "))
            {
                MessageBox.Show("Please fulfill both fields.", "Failed Login!");
                return false;
            }


            MessageBox.Show("Sucesfull  Login. Welcome " + username, "Succesfull Login!");

            return true;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

           InsertionRepository dbInsertion = new InsertionRepository();
           dbInsertion.ExecuteQueries();

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            InsertionRepository dbInsertion = new InsertionRepository();
            dbInsertion.DeleteRecords();
        }
    }
}
