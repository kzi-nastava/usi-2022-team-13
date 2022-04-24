using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem
{
    public partial class HospitalManagerView : Form
    {
        public HospitalManagerView()
        {
            InitializeComponent();
        }

        private void HospitalManagerView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
