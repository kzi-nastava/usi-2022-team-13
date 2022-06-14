using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Examinations.Model;

namespace HealthCareSystem.Core.Examinations.Repository
{
    class AnamnesisRepository
    {
        public OleDbConnection Connection { get; set; }
        public AnamnesisRepository()
        {
            try
            {
                Connection = DatabaseConnection.GetConnection();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }

        internal List<DoctorAnamnesis> GetAnamnesises(List<Examination> examinations)
        {
            List<DoctorAnamnesis> anamnesises = new List<DoctorAnamnesis>();

            foreach (Examination examination in examinations)
            {
                DoctorAnamnesis anamnesis = GetDoctorAnamnesis(examination.Id);
                if (anamnesis != null) anamnesises.Add(anamnesis);
            }


            return anamnesises;
        }

        public List<DoctorAnamnesis> GetAnamnesisesByKeyword(List<DoctorAnamnesis> anamnesises, string keyword)
        {
            List<DoctorAnamnesis> filteredAnamnesises = new List<DoctorAnamnesis>();
            foreach (DoctorAnamnesis anamnesis in anamnesises)
            {
                if (IsKeywordInAnamnesis(keyword, anamnesis))
                    filteredAnamnesises.Add(anamnesis);
            }
            return filteredAnamnesises;
        }

        private static bool IsKeywordInAnamnesis(string keyword, DoctorAnamnesis anamnesis)
        {
            return anamnesis.Notice.ToLower().Contains(keyword.ToLower()) || anamnesis.Conclusions.ToLower().Contains(keyword.ToLower());
        }

        public DoctorAnamnesis GetDoctorAnamnesis(int examinationId)
        {

            string query = "select a.id_examination as ExaminationId, a.notice as Notice, a.conclusions as Conclusions, e.dateOf as DateOfExamination, d.firstName + ' ' + d.lastName as Doctor, d.speciality as Speciality from (Anamnesises a inner join Examination e on a.id_examination = e.id) inner join doctors d on e.id_doctor = d.id where e.id = " + examinationId + "";

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            DoctorAnamnesis anamnesis = null;

            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                anamnesis = SetDoctorAnamnesisValues(reader);
            }

            return anamnesis;
        }

        private static DoctorAnamnesis SetDoctorAnamnesisValues(OleDbDataReader reader)
        {
            return new DoctorAnamnesis(Convert.ToInt32(
                                        reader["ExaminationId"]),
                                        reader["Notice"].ToString(),
                                        reader["Conclusions"].ToString(),
                                        (DateTime)reader["DateOfExamination"],
                                        reader["Doctor"].ToString(),
                                        reader["Speciality"].ToString()
                                        );
        }

        public Anamnesis GetAnamnesis(int examinationId)
        {

            OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Anamnesises where id_examination = " + examinationId + "", Connection);
            Anamnesis anamnesis = null;

            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                anamnesis = SetAnamnesisValues(reader);
            }

            return anamnesis;
        }

        private static Anamnesis SetAnamnesisValues(OleDbDataReader reader)
        {
            return new Anamnesis(Convert.ToInt32(reader["id_examination"]),
                                            reader["notice"].ToString(),
                                            reader["conclusions"].ToString(),
                                            (DateTime)reader["dateOf"]);
        }


    }
}
