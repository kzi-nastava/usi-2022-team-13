using HealthCareSystem.Core.Examinations.Model;
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
    }
}
