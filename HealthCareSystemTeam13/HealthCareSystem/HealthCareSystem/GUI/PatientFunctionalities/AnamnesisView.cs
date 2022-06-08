using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Examinations.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class AnamnesisView : Form
    {
        public int ExaminationId { get; set; }
        private readonly AnamnesisRepository _anamnesisRepository;
        public AnamnesisView(int examinationId)
        {
            ExaminationId = examinationId;
            _anamnesisRepository = new AnamnesisRepository();
            InitializeComponent();
            SetValues();
        }

        private void SetValues()
        {
            Anamnesis anamnesis = _anamnesisRepository.GetAnamnesis(ExaminationId);
            lbDate.Text = anamnesis.DateOf.ToString();
            rtbNotice.Text = anamnesis.Notice;
            rtbConclusion.Text = anamnesis.Conclusions;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
