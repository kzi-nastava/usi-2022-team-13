﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Model
{
    enum MedicationStatus
    {
        InProgress, Approved, Denied
    }
    class Medication
    {
        public string Name { get; set; }
        
        public int ID { get; set; }
        public MedicationStatus Status { get; set; }
       
        public Medication()
        {

        }

        public Medication(int id, string name, MedicationStatus status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }
        public Medication(string name, MedicationStatus status)
        {
            this.Name = name;
            this.Status = status;
        }
    }
}
