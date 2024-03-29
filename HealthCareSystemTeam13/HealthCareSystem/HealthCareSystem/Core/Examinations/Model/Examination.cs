﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Model
{

    enum TypeOfExamination
    {
        Operation, BasicExamination
    }
    class Examination
    {
        public int IdDoctor { get; set; }

        public int IdPatient { get; set; }

        public bool IsEdited = false;

        public bool IsCancelled = false;

        public bool IsFinished = false;
        public DateTime DateOf { get; set; }
        public TypeOfExamination TypeOfExamination { get; set; }

        public bool IsUrgent = false;
        public int IdRoom { get; set; }
        public int Duration { get; set; }
        public int Id { get; set; }
        public Examination()
        {

        }
        public Examination(int idDoctor, int idPatient, bool isEdited, bool isCancelled, bool isFinished, DateTime dateOf, 
            TypeOfExamination typeOfExamination, bool isUrgent, int idRoom, int Duration)
        {
            this.IdDoctor = idDoctor;
            this.IdPatient = idPatient;
            this.IsEdited = isEdited;
            this.IsCancelled = isCancelled;
            this.IsFinished = isFinished;
            this.DateOf = dateOf;
            this.TypeOfExamination = typeOfExamination;
            this.IsUrgent = isUrgent;
            this.IdRoom = idRoom;
            this.Duration = Duration;
        }
        public Examination(int id, int idDoctor, int idPatient, bool isEdited, bool isCancelled, bool isFinished, DateTime dateOf,
            TypeOfExamination typeOfExamination, bool isUrgent, int idRoom, int Duration)
        {
            this.Id = id;
            this.IdDoctor = idDoctor;
            this.IdPatient = idPatient;
            this.IsEdited = isEdited;
            this.IsCancelled = isCancelled;
            this.IsFinished = isFinished;
            this.DateOf = dateOf;
            this.TypeOfExamination = typeOfExamination;
            this.IsUrgent = isUrgent;
            this.IdRoom = idRoom;
            this.Duration = Duration;
        }

        public Examination(int idDoctor, DateTime dateOf, TypeOfExamination type, int idRoom)
        {
            IdDoctor = idDoctor;
            DateOf = dateOf;
            TypeOfExamination = type;
            IdRoom = idRoom;

        }
        public override string ToString()
        {
            return Convert.ToString(IdDoctor) + "; " + DateOf.ToString() + "; " + Convert.ToString(IdRoom);
         }
    }
}
