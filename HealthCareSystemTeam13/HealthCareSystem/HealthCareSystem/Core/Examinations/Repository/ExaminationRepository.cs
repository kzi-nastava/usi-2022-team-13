using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Repository
{
    class ExaminationRepository
    {
        OleDbConnection Connection;
        RoomRepository RoomRep;
        public ExaminationRepository()
        {
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HCDb.mdb;
                Persist Security Info=False;";

                

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            RoomRep = new RoomRepository();

        }
        public List<Examination> GetAllOtherExaminations(int currentExaminationId)
        {
            List<Examination> examinations = new List<Examination>();
            Connection.Open();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Examination where not id = "+currentExaminationId+"", Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TypeOfExamination typeOfExamination;
                Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out typeOfExamination);

                examinations.Add(new Examination(
                    Convert.ToInt32(reader["id_doctor"]),
                    Convert.ToInt32(reader["id_patient"]),
                    false,
                    false,
                    false,
                    (DateTime)reader["dateOf"],
                    typeOfExamination,
                    false,
                    Convert.ToInt32(reader["id_room"]),
                    Convert.ToInt32(reader["duration"])
                    ));
            }
            Connection.Close();

            return examinations;
        }

        public List<Examination> GetRecommendedExaminations(Doctor selectedDoctor, string startTime, string endTime, DateTime examinationFinalDate, bool isDoctorPriority)
        {
            bool isExaminationFound = false;
            List<Examination> examinations = new List<Examination>();

            // get taken appointments in criteria
            List<Examination> takenExaminations = GetTakenExaminations(selectedDoctor.ID, startTime, endTime, examinationFinalDate, isDoctorPriority);

            DateTime startDate = Helpers.GetMergedDateTime(DateTime.Now.AddDays(1), startTime);
            DateTime endDate = Helpers.GetMergedDateTime(examinationFinalDate, endTime);
            int startHour = startDate.Hour;
            int startMinute = startDate.Minute;
            int endHour = endDate.Hour;
            int endMinute = endDate.Minute;

            /*
                KAKO URADITI
                    

                SELEKTOVATI SVE EXAMINATIONE PRVO ZNACI (PROMENITI GETTAKENEXAMINATIONS U NES DRUGO)
                PA ZATIM KAD SE GLEDA DOKTOR POREDIMO ID-OVE SA DOKTOROM I UBACUJEMO U NEKU LISTICU GDE SE POKLAPAJU
                BEZ OBIZRA NA VREME, AKO SE VREME POKLOPI UBACUJEMO U KONACNU NEKU DRUGU LISTU I NJU VRACAMO

                ISTO I ZA TIME
                POREDIMO VREMEENA DA LI SU U OPSEGU I ONDA JOS AKO JE I DOKTOR SLOBODAN U TO VREME OPET U NEKU SUPER LISTU, U SUPROTNOM U OBICNU

                AKO NEKIM CUDOM SE TERMIN NE NADJE, ONDA NEKI TOP 3 SE ODRADI SA NEKIM NAJBLIZIM TERMINIMA ZELEJNOM
                OPSEGU
             
             */


            if (isDoctorPriority)
            {
                // extending the search for doctors just slightly
                endDate.AddDays(2);

                while(startDate.CompareTo(endDate) <= 0)
                {
                    foreach(Examination takenExam in takenExaminations)
                    {
                        TimeSpan difference = startDate.Subtract(takenExam.DateOf);
                        if (Math.Abs(difference.TotalMinutes) < 15 && selectedDoctor.ID == takenExam.IdDoctor)
                        {
                            continue;
                        }
                        else
                        {
                            int roomId = RoomRep.GetAvailableRoomId(startDate, takenExaminations);
                            if (roomId > 0) {

                                examinations.Add(new Examination(selectedDoctor.ID, startDate, TypeOfExamination.BasicExamination, roomId));

                                isExaminationFound = true;
                                break;

                            }
                        }
                        
                    }
                    if (isExaminationFound)
                    {
                        break;
                    }

                    startDate.AddMinutes(15);
                    if(startDate.Hour  > endHour)
                    {
                        if(startDate.Minute > endMinute)
                        {
                            startDate.AddDays(1).AddHours(-(endHour - startHour)).AddMinutes(-(endMinute - startMinute));

                        }
                    }
                }
                
            }
            else
            {
                

            }
            if(examinations.Count() == 0)
            {
                // select top 3 from examination where date between startDate and endDate + 2, startTime - 2h, endTime + 2h
            }


            return examinations;
        }

        private List<Examination> GetTakenExaminations(int doctorId, string startTime, string endTime, DateTime examinationFinalDate, bool isDoctorPriority)
        {
            List<Examination> examinations = new List<Examination>();
            Connection.Open();

            DateTime start = Helpers.GetMergedDateTime(DateTime.Now, startTime);
            DateTime end = Helpers.GetMergedDateTime(examinationFinalDate, endTime);

            string query = "select * from examination where id_doctor = " + doctorId + " and dateOf between #" + start.ToString() + "# and #" + end.ToString() + "#";

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TypeOfExamination typeOfExamination;
                Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out typeOfExamination);

                examinations.Add(new Examination(
                    Convert.ToInt32(reader["id_doctor"]),
                    Convert.ToInt32(reader["id_patient"]),
                    false,
                    false,
                    false,
                    (DateTime)reader["dateOf"],
                    typeOfExamination,
                    false,
                    Convert.ToInt32(reader["id_room"]),
                    Convert.ToInt32(reader["duration"])
                    ));
            }
            Connection.Close();

            return examinations;
        }
    }
}
