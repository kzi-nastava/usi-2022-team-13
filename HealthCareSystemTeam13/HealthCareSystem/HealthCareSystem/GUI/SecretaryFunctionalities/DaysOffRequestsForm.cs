using HealthCareSystem.Core.GUI.SecretaryFunctionalities;
using HealthCareSystem.Core.Users.Secretaries.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.GUI.SecretaryFunctionalities
{
    public partial class DaysOffRequestsForm : Form
    {
        SecretaryRepository _secretaryRepository;
        string Username;
        public DaysOffRequestsForm(string username)
        {
            _secretaryRepository = new SecretaryRepository();
            _secretaryRepository.PullDaysOffRequests();
            InitializeComponent();
            FillDataGridView();
            this.Username = username;
        }
        private void FillDataGridView()
        {
            requestsDataGrid.DataSource = _secretaryRepository.DaysOffRequests;
            DataGridViewSettings();
        }
        private void DataGridViewSettings()
        {
            requestsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            requestsDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            requestsDataGrid.MultiSelect = false;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            int requestId = (int)requestsDataGrid.SelectedRows[0].Cells[0].Value;
            _secretaryRepository.ManageDaysOffRequest(Username, requestId, true);
            MessageBox.Show("Succesfully accepted request!");
        }

        private void denyButton_Click(object sender, EventArgs e)
        {
            int requestId = (int)requestsDataGrid.SelectedRows[0].Cells[0].Value;
            AddComment addComment = new AddComment(Username, requestId);
            addComment.ShowDialog();
        }
    }
}
