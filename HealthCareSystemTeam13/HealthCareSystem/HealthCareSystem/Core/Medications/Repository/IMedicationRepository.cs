using HealthCareSystem.Core.Medications.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Repository
{
    interface IMedicationRepository
    {
        int GetMedicationNotificationTime(int patientId);
        void SetMedicationNotificationTime(int newTime, int patientId);
        void InsertConnectionOfReceiptAndMedication(int receiptId, int medicationId);
        String GetMedicationNameById(int medicationId);
        BindingList<Medication> GetMedications();
        void UpdateMedication(string query);
        void InsertRejectedMedication(string reasonForDenying, int medicationId, int doctorId);
        void PullMedicine();
        void PullMedications();
        Medication GetSelectedMedication(string query);
        void InsertMedicationContainsIngredient(int medicationId, int ingredientId);
        bool DoesMedicationExists(string name);
        void InsertMedication(string ingredientName);
        List<int> GetAlergicMedicationsIds(int patientId);
        DataTable GetMedicationsDatatable();
        DataTable GetMedicineDatatable();
    }
}
