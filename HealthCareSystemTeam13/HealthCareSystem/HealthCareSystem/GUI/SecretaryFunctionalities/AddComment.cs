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
using HealthCareSystem.Core.Users.Doctors.Repository;

namespace HealthCareSystem.GUI.SecretaryFunctionalities
{
    public partial class AddComment : Form
    {
        string Username;
        int RequestId;
        DaysOffRepository _daysOffRepository;
        public AddComment(string username, int requestId)
        {
            this.Username = username;
            this.RequestId = requestId;
            _daysOffRepository = new DaysOffRepository();
            InitializeComponent();
        }

        private void denyButton_Click(object sender, EventArgs e)
        {
            _daysOffRepository.ManageDaysOffRequest(Username, RequestId, false, commentTextBox.Text);
        }
    }
}
