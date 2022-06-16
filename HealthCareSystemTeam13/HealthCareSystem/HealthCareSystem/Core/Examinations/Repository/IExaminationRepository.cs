using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Patients.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Repository
{
    interface IExaminationRepository
    {
        DataTable GetExaminations();
        DataTable GetRequestsPatients();
        DataTable GetClosestExaminations();
        DataTable GetFinishedExaminations();
        void PullExaminationForPatient(int patientId);
        void PullFinishedExaminations(int patientId);
        void InsertExamination(int patientId, int doctorId, DateTime examinationDateTime, int duration, int roomId, string selectedType = "");
        Dictionary<string, string> GetExamination(int examinationId);
        void CancelExamination(int examinationId);
        int GetRoomIdFromExaminationId(int examinationId);
        List<DateTime> GetDateOfExaminationsForDoctor(int doctorId);
        void PullExaminationRequests();
        void PullClosestExaminations(DoctorSpeciality speciality);
        void InsertSingleExamination(Examination examination);
        void DeleteSingleExamination(string requestID);
        void UpdateExamination(string requestID);
        List<Examination> GetDoctorsEximanitonsInNextTwoHours(string doctorId);
        List<Examination> GetRoomEximanitonsFromTo(DateTime fromDateTime, DateTime toDateTime, string roomId);
        Examination GetExaminationFromReader(OleDbDataReader reader);
        List<Examination> GetDoctorsExamiantions(DateTime from, DateTime to, string doctorId);
        Tuple<string, DateTime> AvailableExamination(DoctorSpeciality speciality, int duration);
        void MoveExamination(int id, DateTime dateTime);
        void InsertExaminationChanges(TypeOfChange typeOfChange, int patientId = 0);
        void MoveDoctorsExaminations(DateTime from, DateTime to, int doctorId);
        void PullExaminations(int doctorId);
        List<Examination> GetExaminationsInRoom(Room room);
        Examination SetExaminationValues(OleDbDataReader reader);
        List<Examination> GetRecommendedExaminations(Doctor selectedDoctor, string startTime, string endTime, DateTime examinationFinalDate, bool isDoctorPriority);
        List<Examination> GetFreeExaminationsWithDoctorPriority(int doctorId, DateTime startDate, DateTime endDate, List<Examination> takenExaminations);
        List<Examination> GetFreeExaminationsWithTimespanPriority(DateTime startDate, DateTime endDate, List<Examination> takenExaminations);
        List<Examination> GetFreeExaminations(int doctorId, DateTime startDate, DateTime endDate, List<Examination> takenExaminations);
        List<Examination> GetTopThreeExaminations(DateTime startDate, DateTime endDate, List<Examination> takenExaminations);
        List<Examination> GetTakenExaminations(int doctorId, string startTime, string endTime, DateTime examinationFinalDate);
        Examination GetExaminationValues(OleDbDataReader reader);
        List<Examination> GetFinishedExaminations(int patientId);
        List<Examination> GetAllOtherExaminations(int currentExaminationId);
        List<Examination> GetAllExaminations();
        void SetExaminationValues(List<Examination> examinations, OleDbDataReader reader);
        void PullExaminationsByDate(DateTime date);
        void PullExaminationsThreeDays();
    }
}
