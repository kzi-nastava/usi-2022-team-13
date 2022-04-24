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
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Authentication;
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

            LoginAuthentication loginAuthentication = new LoginAuthentication(tbUsername.Text, tbPassword.Text, this);
            loginAuthentication.OpenViewForm();

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
