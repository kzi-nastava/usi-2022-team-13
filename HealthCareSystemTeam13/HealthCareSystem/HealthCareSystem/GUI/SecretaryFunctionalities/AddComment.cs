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
    public partial class AddComment : Form
    {
        string Username;
        int RequestId;
        SecretaryRepository _secretaryRepository;
        public AddComment(string username, int requestId)
        {
            this.Username = username;
            this.RequestId = requestId;
            _secretaryRepository = new SecretaryRepository();
            InitializeComponent();
        }

        private void denyButton_Click(object sender, EventArgs e)
        {
            _secretaryRepository.ManageDaysOffRequest(Username, RequestId, false, commentTextBox.Text);
        }
    }
}
