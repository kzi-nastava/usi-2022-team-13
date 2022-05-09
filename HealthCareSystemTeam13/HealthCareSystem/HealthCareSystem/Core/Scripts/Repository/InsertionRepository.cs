using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Patients.Model;
using HealthCareSystem.Core.Users.Secretaries.Model;
using HealthCareSystem.Core.Medications.Model;
using HealthCareSystem.Core.Users.HospitalManagers;
using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Rooms.Equipment.Model;
using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;
using HealthCareSystem.Core.Ingredients.Model;
using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Medications.Receipts.Model;
using HealthCareSystem.Core.Rooms.Renovations.Model;
using HealthCareSystem.Core.Rooms.Equipment.TransferHistoryOfEquipment.Model;
using HealthCareSystem.Core.Rooms.DynamicEqipmentRequests.Model;
using HealthCareSystem.Core.Rooms.Equipment.RoomHasEquipment.Model;

namespace HealthCareSystem.Core.Scripts.Repository
{
    class InsertionRepository
    {
        private static OleDbConnection Connection;

        public InsertionRepository()
        {
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HCDb.mdb;
                Persist Security Info=False;";

                Connection.Open();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        public void ExecuteQueries()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
            //Users Insertion
            InsertUsers();
            InsertDoctors();
            InsertManagers();
            InsertPatients();
            InsertSecretaries();
            InsertBlockedPatients();

            //Room Insertion
            InsertRooms();

            //Medication Insertion
            InsertMedications();
            InsertIngredients();

            InsertMedicationsIngredients();
            InsertRejectedMedications();

            //Equipment
            InsertEquipment();

            //Surveys
            InsertHospitalSurveys();

            //PatientAlergies
            InsertPatientAlergies();

            InsertMedicalRecords();
            InsertExaminations();
            InsertAnamnesises();
            InsertInstructions();
            InsertDiseaseHistories();


            InsertRoomHasEquipment();
            InsertDynamicEquipmentRequests();

            //InsertRenovations();
            //InsertTransferHistoryOfEquipment();
          
            InsertPatientEditRequests();

            InsertReceipts();
            InsertReceiptMedication();

            InsertPatientExaminationChanges();

            Connection.Close();
        }





        private static void InsertDiseaseHistories()
        {
            List<DiseaseHistory> diseaseHistories = GetDiseaseHistories();

            foreach (DiseaseHistory diseaseHistory in diseaseHistories)
            {
                InsertSingleDiseaseHistory(diseaseHistory);
            }
        }

        private static List<DiseaseHistory> GetDiseaseHistories()
        {
            List<DiseaseHistory> diseaseHistory = new List<DiseaseHistory>();
            List<String> medicalRecordIds = GetMedicalRecordIds();


            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[0]), "Dementia"));
            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[1]), "Alzheimer"));
            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[2]), "Diabetes"));

            return diseaseHistory;
        }

        private static void InsertSingleDiseaseHistory(DiseaseHistory diseaseHistory)
        {
            var query = "INSERT INTO DiseaseHistory(id_medicalRecord, nameOfDisease) VALUES(@id_medicalRecord, @nameOfDisease)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_medicalRecord", diseaseHistory.MedicalRecordId);
                cmd.Parameters.AddWithValue("@nameOfDisease", diseaseHistory.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRecords()
        {
            try
            {
                if (Connection.State == System.Data.ConnectionState.Closed)
                {
                    Connection.Open();
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                // Deleting all records from database
                DatabaseHelpers.ExecuteNonQueries("Delete from BlockedPatients", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from PatientExaminationChanges", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from ReceiptMedications", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from Receipt", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from HospitalSurveys", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from PatientAlergicTo", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from MedicalRecord", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from Examination", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from Instructions", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from DiseaseHistory", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from RequestForDinamicEquipment", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from users", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from rooms", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from medications", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from Ingredients", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from MedicationContainsIngredient", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from RejectedMedications", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from Equipment", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from Anamnesises", Connection);
                DatabaseHelpers.ExecuteNonQueries("Delete from RoomHasEquipment", Connection);
        
                DatabaseHelpers.ExecuteNonQueries("Delete from EquipmentTransferHistory", Connection);
              
                Connection.Close();
            }
        }

        private static List<String> GetUserIDs(UserRole role)
        {
            var query = "select ID from Users where role='" + role.ToString() + "'";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static List<String> GetPatientIds()
        {
            var query = "select ID from Patients";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static List<String> GetSecretaryIDs()
        {
            var query = "select ID from Secretaries";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static List<String> GetDoctorIds()
        {
            var query = "select ID from Doctors";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static List<String> GetEquipmentIDs()
        {
            var query = "select ID from Equipment where type='" + Equipment.EquipmentType.Dynamic.ToString() + "'";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static List<String> GetRoomIDs()
        {
            var query = "select ID from Rooms";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static List<Equipment> GetEquipment()
        {
            List<Equipment> equipment = new List<Equipment>();

            equipment.Add(new Equipment("Bed", Equipment.EquipmentType.Static ));
            equipment.Add(new Equipment("Chair", Equipment.EquipmentType.Dynamic));
            equipment.Add(new Equipment("Computer", Equipment.EquipmentType.Dynamic));

            return equipment;
        }

        private static void InsertEquipment()
        {
            List<Equipment> equipment = GetEquipment();

            foreach (Equipment singleEquipment in equipment)
            {
                InsertSingleEquipment(singleEquipment);
            }

        }

        private static void InsertSingleEquipment(Equipment equipment)
        {
            var query = "INSERT INTO equipment(nameOf, type) VALUES(@name, @type)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@name", equipment.Name);
                cmd.Parameters.AddWithValue("@type", equipment.Type.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        private static List<HospitalSurvey> GetHospitalSurveys()
        {
            List<HospitalSurvey> hospitalSurveys = new List<HospitalSurvey>();

            hospitalSurveys.Add(new HospitalSurvey(5, 5, 5, 5, "Great service!" ));
            hospitalSurveys.Add(new HospitalSurvey(2, 5, 3, 2, "So-so!"));
            hospitalSurveys.Add(new HospitalSurvey(2, 2, 2, 2, "I really hated the hospital!"));

            return hospitalSurveys;
        }

        private static void InsertHospitalSurveys()
        {
            List<HospitalSurvey> hospitalSurveys = GetHospitalSurveys();
            List<String> patientIDs = GetPatientIds();
            foreach (HospitalSurvey hospitalSurvey in hospitalSurveys)
            {
                InsertSingleHospitalSurvey(hospitalSurvey, patientIDs);
            }

        }

        private static void InsertSingleHospitalSurvey(HospitalSurvey hospitalSurvey, List<String> patientIDs)
        {
            var query = "INSERT INTO hospitalSurveys(quality," +
                "higyene," +
                "isSatisfied," +
                "wouldRecomend," +
                "comment, id_patient) VALUES(@qualityOfService, @cleanliness, @happiness, @wouldRecommend, @comment, @idPatient)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@qualityOfService", hospitalSurvey.QualityOfService);
                cmd.Parameters.AddWithValue("@cleanliness", hospitalSurvey.Cleanliness);
                cmd.Parameters.AddWithValue("@happiness", hospitalSurvey.Happiness);
                cmd.Parameters.AddWithValue("@wouldRecommend", hospitalSurvey.WouldRecommend);
                cmd.Parameters.AddWithValue("@comment", hospitalSurvey.Comment);
                cmd.Parameters.AddWithValue("@idPatient", Convert.ToInt32(patientIDs[0]));
                cmd.ExecuteNonQuery();
            }
        }

        private static List<User> GetUsers()
        {
            List<User> users = new List<User>();

            users.Add(new User("markomarkovic", "marko123", UserRole.HospitalManagers));

            users.Add(new User("mirkobreskvica", "mirko123", UserRole.Doctors));
            users.Add(new User("marinaadamovic", "marina123", UserRole.Doctors));
            users.Add(new User("nikolaredic", "nikola123", UserRole.Doctors));


            users.Add(new User("jovanjabuka", "jovan123", UserRole.Patients));
            users.Add(new User("nevenkamilica", "neven123", UserRole.Patients));
            users.Add(new User("isidornevenko", "isidor123", UserRole.Patients));
            users.Add(new User("marasavic", "mara123", UserRole.Patients));

            users.Add(new User("tinabalerina", "tina123", UserRole.Secretaries));
            users.Add(new User("tomadiploma", "toma123", UserRole.Secretaries));
            users.Add(new User("codabilo", "danilo123", UserRole.Secretaries));
            users.Add(new User("1", "1", UserRole.Secretaries));

            return users;
        }

        private static void InsertUsers()
        {
            List<User> users = GetUsers();

            foreach(User user in users)
            {
                InsertSingleUser(user);
            }
  
        }

        private static void InsertSingleUser(User user)
        {
            var query = "INSERT INTO users(usrnm, pass, role) VALUES(@usrnm, @pass, @role)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@usrnm", user.Username);
                cmd.Parameters.AddWithValue("@pass", user.Password);
                cmd.Parameters.AddWithValue("@role", user.Role.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertDoctors()
        {
            List<Doctor> doctors = GetDoctors();

            foreach (Doctor doctor in doctors)
            {
                InsertSingleDoctor(doctor);
            }
        }

        private static List<Renovation> GetRenovations()
        {
            List<Renovation> renovations = new List<Renovation>();
            List<String> roomIDs = GetRoomIDs();

            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[0]), DateTime.Now, DateTime.Now.AddMonths(2)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[1]), DateTime.Now, DateTime.Now.AddMonths(1)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[2]), DateTime.Now, DateTime.Now.AddMonths(3)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[3]), DateTime.Now, DateTime.Now.AddMonths(4)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[4]), DateTime.Now, DateTime.Now.AddMonths(5)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[5]), DateTime.Now, DateTime.Now.AddMonths(1)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[6]), DateTime.Now, DateTime.Now.AddDays(15)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[7]), DateTime.Now, DateTime.Now.AddDays(25)));

            return renovations;
        }

        // FOR LATER PURPOSES

        /*private static void InsertRenovations()
        {
            List<Renovation> renovations = GetRenovations();

            foreach (Renovation renovation in renovations)
            {
                InsertSingleRenovation(renovation);
            }
        }*/

       /* private static void InsertSingleRenovation(Renovation renovation)
        {
            var query = "INSERT INTO Renovations(id_room, dateOfStart, dateOfFinish) VALUES(@id_room, @startingDate, @ending_date)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_room", renovation.RoomId);
                cmd.Parameters.AddWithValue("@startingDate", renovation.StartingDate.ToString());
                cmd.Parameters.AddWithValue("@ending_date", renovation.EndingDate.ToString());
                cmd.ExecuteNonQuery();
            }
        }*/

        /*private static List<TransferHistoryOfEquipment> GetTransferHistoryOfEquipment()
        {
            List<TransferHistoryOfEquipment> transferHistoryOfEquipment = new List<TransferHistoryOfEquipment>();
            List<String> roomIDs = GetRoomIDs();

            transferHistoryOfEquipment.Add(new TransferHistoryOfEquipment(Convert.ToInt32(roomIDs[0]), Convert.ToInt32(roomIDs[5]), DateTime.Now));
            transferHistoryOfEquipment.Add(new TransferHistoryOfEquipment(Convert.ToInt32(roomIDs[1]), Convert.ToInt32(roomIDs[6]), DateTime.Now));

            return transferHistoryOfEquipment;
        }

        private static void InsertTransferHistoryOfEquipment()
        {
            List<TransferHistoryOfEquipment> transferHistoryOfEquipment = GetTransferHistoryOfEquipment();

            foreach (TransferHistoryOfEquipment singleTransferHistoryOfEquipment in transferHistoryOfEquipment)
            {
                InsertSingleTransferHistoryOfEquipment(singleTransferHistoryOfEquipment);
            }
        }

        private static void InsertSingleTransferHistoryOfEquipment(TransferHistoryOfEquipment transferHistoryOfEquipment)
        {
            var query = "INSERT INTO EquipmentTransferHistory(id_original_room, id_new_room, dateOfChange) VALUES(@first_room_id, @second_room_id, @transferDate)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@first_room_id", transferHistoryOfEquipment.FirstRoomId);
                cmd.Parameters.AddWithValue("@second_room_id", transferHistoryOfEquipment.SecondRoomId);
                cmd.Parameters.AddWithValue("@transferDate", transferHistoryOfEquipment.TransferDate.ToString());
                cmd.ExecuteNonQuery();
            }
        }*/

        private static List<RoomHasEquipment> GetRoomHasEquipment()
        {
            List<RoomHasEquipment> roomHasEquipment = new List<RoomHasEquipment>();
            List<String> roomIDs = GetRoomIDs();
            List<String> equipmentIDs = GetEquipmentIDs();
            
            roomHasEquipment.Add(new RoomHasEquipment(Convert.ToInt32(equipmentIDs[0]), Convert.ToInt32(roomIDs[4]), 5));
            roomHasEquipment.Add(new RoomHasEquipment(Convert.ToInt32(equipmentIDs[1]), Convert.ToInt32(roomIDs[3]), 4));

            return roomHasEquipment;
        }

        private static void InsertRoomHasEquipment()
        {
            List<RoomHasEquipment> roomHasEquipment = GetRoomHasEquipment();

            foreach (RoomHasEquipment singleRoomHasEquipment in roomHasEquipment)
            {
                InsertSingleRoomHasEquipment(singleRoomHasEquipment);
            }
        }

        private static void InsertSingleRoomHasEquipment(RoomHasEquipment roomHasEquipment)
        {
            var query = "INSERT INTO RoomHasEquipment(id_room, id_equipment, amount) " +
                "VALUES(@room_id, @equipment_id, @quantity)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@room_id", roomHasEquipment.RoomId);
                cmd.Parameters.AddWithValue("@equipment_id", roomHasEquipment.EquipmentId);
                cmd.Parameters.AddWithValue("@quantity", roomHasEquipment.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        private static List<DynamicEquipmentRequest> GetDynamicEquipmentRequests()
        {
            List<DynamicEquipmentRequest> dynamicEquipmentRequests = new List<DynamicEquipmentRequest>();
            List<String> equipmentIDs = GetEquipmentIDs();

            dynamicEquipmentRequests.Add(new DynamicEquipmentRequest(Convert.ToInt32(equipmentIDs[0]), 10));
            dynamicEquipmentRequests.Add(new DynamicEquipmentRequest(Convert.ToInt32(equipmentIDs[1]), 15));
            // dynamicEquipmentRequests.Add(new DynamicEquipmentRequest(Convert.ToInt32(equipmentIDs[2]), 20));

            return dynamicEquipmentRequests;
        }

        private static void InsertDynamicEquipmentRequests()
        {
            List<DynamicEquipmentRequest> dynamicEquipmentRequests = GetDynamicEquipmentRequests();
            List<String> secreatyIDs = GetSecretaryIDs();
            foreach (DynamicEquipmentRequest dynamicEquipmentRequest in dynamicEquipmentRequests)
            {
                InsertSingleDynamicEquipmentRequest(dynamicEquipmentRequest, secreatyIDs[0]);
            }
        }

        private static void InsertSingleDynamicEquipmentRequest(DynamicEquipmentRequest dynamicEquipmentRequest, string secretaryId)
        {
            var query = "INSERT INTO RequestForDinamicEquipment(id_equipment, amount, id_secretary) VALUES(@id_equipment, @quantity, @id_secretary)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_equipment", dynamicEquipmentRequest.EquipmentId);
                cmd.Parameters.AddWithValue("@quantity", dynamicEquipmentRequest.Quantity);
                cmd.Parameters.AddWithValue("@secretary_id", Convert.ToInt32(secretaryId));
                cmd.ExecuteNonQuery();
            }
        }

        private static List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            List<String> userIDs = GetUserIDs(UserRole.Doctors);

            doctors.Add(new Doctor("Mirko", "Breskvica", Convert.ToInt32(userIDs[0]), DoctorSpeciality.BasicPractice));
            doctors.Add(new Doctor("Marina", "Adamovic", Convert.ToInt32(userIDs[1]), DoctorSpeciality.Dermatology));
            doctors.Add(new Doctor("Nikola", "Redic", Convert.ToInt32(userIDs[2]), DoctorSpeciality.Neurology));

            return doctors;
        }

        private static void InsertSingleDoctor(Doctor doctor)
        {
            var query = "INSERT INTO Doctors(firstName, lastName, user_id, speciality) VALUES(@firstName, @LastName, @user_id, @speciality)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@firstName", doctor.FirstName);
                cmd.Parameters.AddWithValue("@LastName", doctor.LastName);
                cmd.Parameters.AddWithValue("@user_id", doctor.UserId);
                cmd.Parameters.AddWithValue("@speciality", doctor.Speciality.ToString());
                cmd.ExecuteNonQuery();
            }
        }
        private static List<Patient> GetPatients()
        {
            List<Patient> patients = new List<Patient>();
            List<String> userIDs = GetUserIDs(UserRole.Patients);

            patients.Add(new Patient("Jovana", "Jabuka", Convert.ToInt32(userIDs[0]), true));
            patients.Add(new Patient("Neven", "Kamilica", Convert.ToInt32(userIDs[1]), false));
            patients.Add(new Patient("Isidor", "Nevenko", Convert.ToInt32(userIDs[2]), false));
            patients.Add(new Patient("Mara", "Savic", Convert.ToInt32(userIDs[3]), false));

            return patients;
        }

        private static void InsertPatients()
        {
            List<Patient> patients = GetPatients();

            foreach (Patient patient in patients)
            {
                InsertSinglePatient(patient);
            }
        }

        private static void InsertSinglePatient(Patient patient)
        {
            var query = "INSERT INTO Patients(firstName, lastName, user_id, isBlocked) VALUES(@firstName, @LastName, @user_id, @isBlocked)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@firstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                cmd.Parameters.AddWithValue("@user_id", patient.UserId);
                cmd.Parameters.AddWithValue("@isBlocked", patient.IsBlocked);
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertBlockedPatients()
        {
            List<BlockedPatient> blockedPatients = GetBlockedPatients();

            foreach (BlockedPatient blockedPatient in blockedPatients)
            {
                InsertSingleBlockedPatient(blockedPatient);
            }

        }

        private static List<BlockedPatient> GetBlockedPatients()
        {
            List<BlockedPatient> blockedPatients = new List<BlockedPatient>();
            List<String> patientsIDs = GetPatientIds();
            List<String> secretariesIDs = GetSecretaryIDs();

            blockedPatients.Add(new BlockedPatient(Convert.ToInt32(patientsIDs[0]), Convert.ToInt32(secretariesIDs[1]), new DateTime(2022, 4, 26)));

            return blockedPatients;
        }

        private static void InsertSingleBlockedPatient(BlockedPatient blockedPatient)
        {
            var query = "INSERT INTO BlockedPatients(id_patient, id_secretary, dateOf) VALUES(@id_patient, @id_secretary, @dateOf)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", blockedPatient.PatientID);
                cmd.Parameters.AddWithValue("@id_secretary", blockedPatient.SecretaryID);
                cmd.Parameters.AddWithValue("@dateOf", blockedPatient.DateOf);
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertSecretaries()
        {
            List<Secretary> secretaries = GetSecretaries();

            foreach (Secretary secretary in secretaries)
            {
                InsertSingleSecretary(secretary);
            }

        }

        private static List<Secretary> GetSecretaries()
        {
            List<Secretary> secretaries = new List<Secretary>();
            List<String> userIDs = GetUserIDs(UserRole.Secretaries);

            secretaries.Add(new Secretary("Tina", "Mihajlovic", Convert.ToInt32(userIDs[0])));
            secretaries.Add(new Secretary("Milica", "Tomic", Convert.ToInt32(userIDs[1])));
            secretaries.Add(new Secretary("Danilo", "Jevtic", Convert.ToInt32(userIDs[2])));

            return secretaries;
        }

        private static void InsertSingleSecretary(Secretary secretary)
        {
            var query = "INSERT INTO Secretaries(firstName, lastName, user_id) VALUES(@firstName, @LastName, @user_id)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@firstName", secretary.FirstName);
                cmd.Parameters.AddWithValue("@LastName", secretary.LastName);
                cmd.Parameters.AddWithValue("@user_id", secretary.UserId);
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertManagers()
        {
            List<HospitalManager> managers = GetHospitalManagers();

            foreach (HospitalManager manager in managers)
            {
                InsertSingleHospitalManager(manager);
            }

        }

        private static List<HospitalManager> GetHospitalManagers()
        {
            List<HospitalManager> hospitalManager = new List<HospitalManager>();
            List<String> userIDs = GetUserIDs(UserRole.HospitalManagers);

            hospitalManager.Add(new HospitalManager("Marko", "Markovic", Convert.ToInt32(userIDs[0])));

            return hospitalManager;
        }

        private static void InsertSingleHospitalManager(HospitalManager hospitalManager)
        {
            var query = "INSERT INTO HospitalManagers(firstName, lastName, user_id) VALUES(@firstName, @LastName, @user_id)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@firstName", hospitalManager.FirstName);
                cmd.Parameters.AddWithValue("@LastName", hospitalManager.LastName);
                cmd.Parameters.AddWithValue("@user_id", hospitalManager.UserId);
                cmd.ExecuteNonQuery();

            }
        }

        private static void InsertSurveys()
        {

        }

        private static void InsertRooms()
        {
            List<Room> rooms = GetRooms();
            foreach(Room room in rooms)
            {
                InsertSingleRoom(room);
            }
        }

        private static List<Room> GetRooms()
        {
            List<Room> rooms = new List<Room>();

            rooms.Add(new Room(TypeOfRoom.DayRoom));
            rooms.Add(new Room(TypeOfRoom.DayRoom));
            rooms.Add(new Room(TypeOfRoom.DeliveryRoom));
            rooms.Add(new Room(TypeOfRoom.DeliveryRoom));
            rooms.Add(new Room(TypeOfRoom.ExaminationRoom));
            rooms.Add(new Room(TypeOfRoom.ExaminationRoom));
            rooms.Add(new Room(TypeOfRoom.IntensiveCareUnit));
            rooms.Add(new Room(TypeOfRoom.IntensiveCareUnit));
            rooms.Add(new Room(TypeOfRoom.NurseryRoom));
            rooms.Add(new Room(TypeOfRoom.NurseryRoom));
            rooms.Add(new Room(TypeOfRoom.OperationRoom));
            rooms.Add(new Room(TypeOfRoom.OperationRoom));
            rooms.Add(new Room(TypeOfRoom.Warehouse));

            return rooms;
        }

        private static void InsertSingleRoom(Room room)
        {
            var query = "INSERT INTO rooms(type) VALUES(@type)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@type", room.Type.ToString());
                cmd.ExecuteNonQuery();

            }
        }

        private static void InsertMedications()
        {
            List<Medication> medications = GetMedications();
            foreach (Medication medication in medications)
            {
                InsertSingleMedication(medication);
            }
        }
        private static List<Medication> GetMedications()
        {
            List<Medication> medications = new List<Medication>();

            medications.Add(new Medication("Brufen", MedicationStatus.Approved));
            medications.Add(new Medication("Analgin", MedicationStatus.Denied));
            medications.Add(new Medication("Panklav", MedicationStatus.Approved));
            
            return medications;
        }

        private static void InsertSingleMedication(Medication medication)
        {
            var query = "INSERT INTO medications(nameOfMedication, status) VALUES(@nameOfMedication, @status)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@nameOfMedication", medication.Name);
                cmd.Parameters.AddWithValue("@status", medication.Status.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        private static List<String> GetMedicationIds()
        {
            var query = "select ID from Medications";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static void InsertRejectedMedications()
        {
            List<RejectedMedication> rejectedMedications = GetRejectedMedications();

            foreach (RejectedMedication rejectedMedication in rejectedMedications)
            {
                InsertSingleRejectedMedication(rejectedMedication);
            }

        }

        private static List<RejectedMedication> GetRejectedMedications()
        {
            List<RejectedMedication> rejectedMedications = new List<RejectedMedication>();
            List<String> medicationsIDs = GetMedicationIds();
            List<String> doctorsIDs = GetDoctorIds();

            rejectedMedications.Add(new RejectedMedication(Convert.ToInt32(medicationsIDs[1]), Convert.ToInt32(doctorsIDs[2]), "Medication is too strong."));

            return rejectedMedications;
        }

        private static void InsertSingleRejectedMedication(RejectedMedication rejectedMedication)
        {
            var query = "INSERT INTO RejectedMedications(id_medication, id_doctor, description) VALUES(@id_medication, @id_doctor, @description)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_medication", rejectedMedication.MedicationID);
                cmd.Parameters.AddWithValue("@id_doctor", rejectedMedication.DoctorID);
                cmd.Parameters.AddWithValue("@description", rejectedMedication.Description);
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertIngredients()
        {
            List<Ingredient> ingredients = GetIngredients();
            foreach (Ingredient ingredient in ingredients)
            {
                InsertSingleIngredient(ingredient);
            }
        }

        private static List<Ingredient> GetIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            ingredients.Add(new Ingredient("Penicilin"));
            ingredients.Add(new Ingredient("Celuloza"));
            ingredients.Add(new Ingredient("Monohidrat"));
            ingredients.Add(new Ingredient("Tikva"));

            return ingredients;
        }

        private static void InsertSingleIngredient(Ingredient ingredient)
        {
            var query = "INSERT INTO Ingredients(nameOfIngredient) VALUES(@nameOfIngredient)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@nameOfIngredient", ingredient.Name);
                cmd.ExecuteNonQuery();
            }
        }

        private static List<String> GetIngredientIDs()
        {
            var query = "select ID from Ingredients";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);

        }

        private static List<String> GetMedicalRecordIds()
        {
            var query = "select ID from MedicalRecord";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);

        }

        private static void InsertSinglePatientAlergies(string patientId, string ingredientId)
        {
            var query = "INSERT INTO PatientAlergicTo(id_patient, id_ingredient) VALUES(@id_patient, @id_ingredient)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", Convert.ToInt32(patientId));
                cmd.Parameters.AddWithValue("@id_ingredient", Convert.ToInt32(ingredientId));
                cmd.ExecuteNonQuery();
            }

        }
        private static void InsertPatientAlergies()
        {
            List<string> patientIDs = GetPatientIds();
            List<string> ingredientIDs = DatabaseHelpers.ExecuteReaderQueries("select id from ingredients", Connection);

            for(int i =0;i < patientIDs.Count();i++)
            {
                
                InsertSinglePatientAlergies(patientIDs[i], ingredientIDs[i]);

            }

        }

        private static void InsertMedicationsIngredients()
        {
            List<MedicationsIngredient> medicationsIngredients = GetMedicationsIngredients();

            foreach (MedicationsIngredient medicationsIngredient in medicationsIngredients)
            {
                InsertSingleMedicationsIngredient(medicationsIngredient);
            }

        }

        private static List<MedicationsIngredient> GetMedicationsIngredients()
        {
            List<MedicationsIngredient> medicationsIngredients = new List<MedicationsIngredient>();
            List<String> medicationsIDs = GetMedicationIds();
            List<String> ingredientsIDs = DatabaseHelpers.ExecuteReaderQueries("select id from Ingredients", Connection);

            medicationsIngredients.Add(new MedicationsIngredient(Convert.ToInt32(medicationsIDs[0]), Convert.ToInt32(ingredientsIDs[0])));
            medicationsIngredients.Add(new MedicationsIngredient(Convert.ToInt32(medicationsIDs[1]), Convert.ToInt32(ingredientsIDs[0])));
            medicationsIngredients.Add(new MedicationsIngredient(Convert.ToInt32(medicationsIDs[2]), Convert.ToInt32(ingredientsIDs[0])));

            return medicationsIngredients;
        }

        private static void InsertSingleMedicationsIngredient(MedicationsIngredient medicationsIngredient)
        {
            var query = "INSERT INTO MedicationContainsIngredient(id_medication, id_ingredient) VALUES(@id_medication, @id_ingredient)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_medication", medicationsIngredient.MedicationID);
                cmd.Parameters.AddWithValue("@id_ingredient", medicationsIngredient.IngredientID);
                cmd.ExecuteNonQuery();

            }
        }

        private static void InsertMedicalRecords()
        {
            List<MedicalRecord> medicalRecords = GetMedicalRecords();

            foreach (MedicalRecord medicalRecord in medicalRecords)
            {
                InsertSingleMedicalRecord(medicalRecord);
            }
        }

        private static List<MedicalRecord> GetMedicalRecords()
        {
            List<MedicalRecord> medicalRecords = new List<MedicalRecord>();
            List<String> patientIDs = GetPatientIds();

            medicalRecords.Add(new MedicalRecord(Convert.ToInt32(patientIDs[0]), 185, 85));
            medicalRecords.Add(new MedicalRecord(Convert.ToInt32(patientIDs[1]), 192, 92));
            medicalRecords.Add(new MedicalRecord(Convert.ToInt32(patientIDs[2]), 183, 75));

            return medicalRecords;
        }

        private static void InsertSingleMedicalRecord(MedicalRecord medicalRecord)
        {
            var query = "INSERT INTO MedicalRecord(id_patient, height, weight) VALUES(@id_patient, @height, @weight)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", medicalRecord.IdPatient);
                cmd.Parameters.AddWithValue("@height", medicalRecord.Height);
                cmd.Parameters.AddWithValue("@weight", medicalRecord.Weight);
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertExaminations()
        {
            List<Examination> examinations = GetExaminations();

            foreach (Examination examination in examinations)
            {
                InsertSingleExamination(examination);
            }
        }

        private static List<Examination> GetExaminations()
        {
            List<Examination> examinations = new List<Examination>();
            List<String> patientIDs = GetPatientIds();
            List<String> doctorIDs = GetDoctorIds();
            List<String> roomIDs = GetRoomIDs();


            examinations.Add(new Examination(Convert.ToInt32(doctorIDs[0]), Convert.ToInt32(patientIDs[2]), false, false, false, new DateTime(2022, 4, 26, 10, 10, 10), TypeOfExamination.BasicExamination, false, Convert.ToInt32(roomIDs[4]), 15));
            examinations.Add(new Examination(Convert.ToInt32(doctorIDs[0]), Convert.ToInt32(patientIDs[0]), false, false, false, DateTime.Now.AddDays(2),TypeOfExamination.BasicExamination, false, Convert.ToInt32(roomIDs[4]), 15));
            examinations.Add(new Examination(Convert.ToInt32(doctorIDs[1]), Convert.ToInt32(patientIDs[1]), false, false, false, DateTime.Now.AddDays(2),TypeOfExamination.BasicExamination, false, Convert.ToInt32(roomIDs[5]), 15));
            examinations.Add(new Examination(Convert.ToInt32(doctorIDs[2]), Convert.ToInt32(patientIDs[2]), false, false, false, DateTime.Now.AddDays(3),TypeOfExamination.BasicExamination, false, Convert.ToInt32(roomIDs[4]), 15));

            return examinations;
        }

        private static void InsertSingleExamination(Examination examination)
        {
            var query = "INSERT INTO Examination(id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration) " +
                "VALUES(@id_doctor, @id_patient, @isEdited, @isCancelled, @isFinished, @dateOf, @typeOfExamination, @isUrgent, @id_room, @duration)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", examination.IdDoctor);
                cmd.Parameters.AddWithValue("@id_patient", examination.IdPatient);
                cmd.Parameters.AddWithValue("@isEdited", examination.IsEdited);
                cmd.Parameters.AddWithValue("@isCancelled", examination.IsCancelled);
                cmd.Parameters.AddWithValue("@isFinished", examination.IsFinished);
                cmd.Parameters.AddWithValue("@dateOf", examination.DateOf.ToString());
                cmd.Parameters.AddWithValue("@typeOfExamination", examination.TypeOfExamination.ToString());
                cmd.Parameters.AddWithValue("@isUrgent", examination.IsUrgent);
                cmd.Parameters.AddWithValue("@id_room", examination.IdRoom);
                cmd.Parameters.AddWithValue("@duration", examination.Duration);
                cmd.ExecuteNonQuery();
            }
        }

        private static List<String> GetExaminationIDs()
        {
            var query = "select ID from Examination";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static void InsertAnamnesises()
        {
            List<Anamnesis> anamnesises = GetAnamnesises();

            foreach (Anamnesis anamnesis in anamnesises)
            {
                InsertSingleAnamnesis(anamnesis);
            }
        }

        private static List<Anamnesis> GetAnamnesises()
        {
            List<Anamnesis> anamnesises = new List<Anamnesis>();
            List<String> examinationIDs = GetExaminationIDs();

            anamnesises.Add(new Anamnesis(Convert.ToInt32(examinationIDs[0]), "Runny nose and coughs alot.", "Patient should drink antibiotics.", new DateTime(2022, 4, 26)));

            return anamnesises;
        }

        private static void InsertSingleAnamnesis(Anamnesis anamnesis)
        {
            var query = "INSERT INTO Anamnesises(id_examination, notice, conclusions, dateOf) VALUES(@id_examination, @notice, @conclusions, @dateOf)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_examination", anamnesis.ExaminationID);
                cmd.Parameters.AddWithValue("@notice", anamnesis.Notice);
                cmd.Parameters.AddWithValue("@conclusions", anamnesis.Conclusions);
                cmd.Parameters.AddWithValue("@dateOf", anamnesis.DateOf.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertInstructions()
        {
            List<Instruction> instructions = GetInstructions();

            foreach (Instruction instruction in instructions)
            {
                InsertSingleInstruction(instruction);
            }
        }

        private static List<Instruction> GetInstructions()
        {
            List<Instruction> instructions = new List<Instruction>();
            DateTime tomorrow = DateTime.Now.AddHours(20);

            instructions.Add(new Instruction(tomorrow, 3));
            instructions.Add(new Instruction(tomorrow, 4));
            instructions.Add(new Instruction(tomorrow, 2));

            return instructions;
        }

        private static void InsertSingleInstruction(Instruction instruction)
        {
            var query = "INSERT INTO Instructions(startTime, timesPerDay) VALUES(@startTime, @timesPerDay)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@startTime", instruction.StartTime.ToString());
                cmd.Parameters.AddWithValue("@timesPerDay", instruction.TimesPerDay);
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertPatientEditRequests()
        {
            List<string> examinationIds = DatabaseHelpers.ExecuteReaderQueries("select id from examination", Connection);
            List<string> doctors = GetDoctorIds();
            List<string> rooms = DatabaseHelpers.ExecuteReaderQueries("select id from rooms where type = '" + TypeOfRoom.ExaminationRoom.ToString() + "'", Connection);

            InsertSinglePatientEditRequest(Convert.ToInt32(examinationIds[0]), DateTime.Now, true, false, Convert.ToInt32(doctors[0]), DateTime.Now.AddDays(2), Convert.ToInt32(rooms[0]));
            InsertSinglePatientEditRequest(Convert.ToInt32(examinationIds[1]), DateTime.Now, false, true, Convert.ToInt32(doctors[0]), DateTime.Now, Convert.ToInt32(rooms[0]));

        }

        private static void InsertSinglePatientEditRequest(int examinationId, DateTime dateOfChange, bool isEdit, bool isDelete, int doctorId, DateTime newDate, int roomId)
        {
            var query = "INSERT INTO PatientEditRequest(id_examination, dateOf, isChanged, isDeleted, id_doctor, dateTimeOfExamination, id_room) VALUES(@id_examination, @dateOf, @isChanged, @isDeleted, @id_doctor, @dateTimeOfExamination, @id_room)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_examination", examinationId);
                cmd.Parameters.AddWithValue("@dateOf", dateOfChange.ToString());
                cmd.Parameters.AddWithValue("@isChanged", isEdit);
                cmd.Parameters.AddWithValue("@isDeleted", isDelete);
                cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                cmd.Parameters.AddWithValue("@dateTimeOfExamination", newDate.ToString());
                cmd.Parameters.AddWithValue("@id_room", roomId);

                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertReceipts()
        {
            List<Receipt> receipts = GetReceipts();

            foreach (Receipt receipt in receipts)
            {
                InsertSingleReceipt(receipt);
            }
        }
        private static void InsertSingleReceipt(Receipt receipt)
        {
            var query = "INSERT INTO Receipt(id_instructions, id_doctor, id_patient, dateOf) VALUES(@id_instructions, @id_doctor, @id_patient, @dateOf)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_instructions", receipt.InstructionId);
                cmd.Parameters.AddWithValue("@id_instructions", receipt.DoctorId);
                cmd.Parameters.AddWithValue("@id_instructions", receipt.PatientId);
                cmd.Parameters.AddWithValue("@id_instructions", receipt.DateOfHandout.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        private static List<Receipt> GetReceipts()
        {
            List<Receipt> receipts = new List<Receipt>();
            List<string> patientIds = GetPatientIds();
            List<string> doctorIds = GetDoctorIds();
            List<string> instructionIds = DatabaseHelpers.ExecuteReaderQueries("select id from Instructions", Connection);
            receipts.Add(new Receipt(Convert.ToInt32(doctorIds[0]), Convert.ToInt32(instructionIds[0]), Convert.ToInt32(patientIds[0]), DateTime.Now));
            receipts.Add(new Receipt(Convert.ToInt32(doctorIds[0]), Convert.ToInt32(instructionIds[1]), Convert.ToInt32(patientIds[1]), DateTime.Now));
            receipts.Add(new Receipt(Convert.ToInt32(doctorIds[1]), Convert.ToInt32(instructionIds[2]), Convert.ToInt32(patientIds[2]), DateTime.Now));


            return receipts;
        }

        private static void InsertReceiptMedication()
        {
            List<string> receiptIds = DatabaseHelpers.ExecuteReaderQueries("select id from Receipt", Connection);
            List<string> medicationIds = GetMedicationIds();
            InsertSingleReceiptMedication(Convert.ToInt32(receiptIds[0]), Convert.ToInt32(medicationIds[0]));
            InsertSingleReceiptMedication(Convert.ToInt32(receiptIds[1]), Convert.ToInt32(medicationIds[0]));
            InsertSingleReceiptMedication(Convert.ToInt32(receiptIds[2]), Convert.ToInt32(medicationIds[1]));


        }
        private static void InsertSingleReceiptMedication(int receiptId, int medicationId)
        {
            var query = "INSERT INTO ReceiptMedications(id_receipt, id_medication) VALUES(@id_receipt, @id_medication)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_receipt", receiptId);
                cmd.Parameters.AddWithValue("@id_medication", medicationId);

                cmd.ExecuteNonQuery();
            }
        }
        private static void InsertPatientExaminationChanges()
        {
            List<string> patientIds = GetPatientIds();
            int desiredPatientId = Convert.ToInt32(patientIds[3]);
            for(int i = 0;i < 8; i++)
            {
                InsertSingleExaminationChange(new ExaminationChange(desiredPatientId, TypeOfChange.Edit, DateTime.Now.AddDays(-2 - i)));
            }
            

        }
        private static void InsertSingleExaminationChange(ExaminationChange change) {
            var query = "INSERT INTO PatientExaminationChanges(id_patient, typeOfChange, dateOf) VALUES(@id_patient, @typeOfChange, @dateOf)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", change.PatientId);
                cmd.Parameters.AddWithValue("@typeOfChange", change.Change.ToString());
                cmd.Parameters.AddWithValue("@dateOf", change.DateOfChange.ToString());

                cmd.ExecuteNonQuery();
            }
        }
      
    }
}
