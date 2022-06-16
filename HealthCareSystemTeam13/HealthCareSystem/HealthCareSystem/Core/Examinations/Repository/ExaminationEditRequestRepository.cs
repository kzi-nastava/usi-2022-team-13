using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Repository
{
    class ExaminationEditRequestRepository: IExaminationEditRequestRepository
    {
        public OleDbConnection Connection { get; set; }
        public ExaminationEditRequestRepository()
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
        public void SendExaminationEditRequest(int examinationId, DateTime currentTime, bool isEdit, int doctorId, DateTime newDateTime, int roomId)
        {
            string query = "insert into PatientEditRequest (id_examination, dateOf, isChanged, isDeleted, id_doctor, dateTimeOfExamination, id_room) VALUES(@id_examination, @dateOf, @isChanged, @isDeleted, @id_doctor, @dateTimeOfExamination, @id_room)";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_examination", examinationId);
                cmd.Parameters.AddWithValue("@dateOf", currentTime.ToString());
                if (isEdit)
                {
                    cmd.Parameters.AddWithValue("@isChanged", true);
                    cmd.Parameters.AddWithValue("@isDeleted", false);
                    cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                    cmd.Parameters.AddWithValue("@dateTimeOfExamination", newDateTime.ToString());
                    cmd.Parameters.AddWithValue("@id_room", roomId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isChanged", false);
                    cmd.Parameters.AddWithValue("@isDeleted", true);
                    cmd.Parameters.AddWithValue("@id_doctor", DBNull.Value);
                    cmd.Parameters.AddWithValue("@dateTimeOfExamination", DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_room", DBNull.Value);
                }

                cmd.ExecuteNonQuery();

            }

        }
    }
}
