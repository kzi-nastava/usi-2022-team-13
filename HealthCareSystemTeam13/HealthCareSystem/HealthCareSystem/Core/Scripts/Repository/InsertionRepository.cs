using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Patients.Model;
using HealthCareSystem.Core.Users.Secretaries.Model;
using HealthCareSystem.Core.Medications.Model;
using HealthCareSystem.Core.Users.HospitalManagers;
using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;
using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;
using HealthCareSystem.Core.Ingredients.Model;
using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Medications.Receipts.Model;
using HealthCareSystem.Core.Rooms.Renovations.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.TransferHistoryOfEquipment.Model;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Rooms.DynamicEqipmentRequests.Model;
using static HealthCareSystem.Core.Rooms.Renovations.Model.Renovation;

namespace HealthCareSystem.Core.Scripts.Repository
{
    class InsertionRepository
    {
        private static OleDbConnection Connection;
        RoomRepository RoomRep;

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
            RoomRep = new RoomRepository();
        }

        public void ExecuteQueries()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
            InsetUsersToDatabase();
            InsertRooms();
            InsertMedications();
            InsertIngredients();
            InsertReferralLetters();
            InsertMedicationsIngredients();
            InsertRejectedMedications();
            InsertEquipment();
            InsertHospitalSurveys();
            InsertPatientAlergies();
            InsertMedicalRecords();
            InsertExaminations();
            InsertAnamnesises();
            InsertInstructions();
            InsertDiseaseHistories();
            InsertRoomHasEquipment();
            InsertDynamicEquipmentRequests();
            InsertRenovations();
            InsertTransferHistoryOfEquipment();
            InsertPatientEditRequests();
            InsertReceipts();
            InsertReceiptMedication();
            InsertPatientExaminationChanges();
            UpdateTransfers();
            UpdateRenovations();
            InsertDoctorSurveys();

            Connection.Close();
        }
        private void InsetUsersToDatabase()
        {
            InsertUsers();
            InsertDoctors();
            InsertManagers();
            InsertPatients();
            InsertSecretaries();
            InsertBlockedPatients();
        }


        public void UpdateRenovations()
        {
            string unrealizedRenovationsQuery = "select * from Renovations where dateOfFinish < #" + DateTime.Now.ToString() + "#";   
       
            List<Renovation> unrealizedRenovations;
            unrealizedRenovations = RoomRep.GetRenovations(unrealizedRenovationsQuery);

            foreach(Renovation renovation in unrealizedRenovations)
            {
               
                if(renovation.Type == TypeOfRenovation.Regular)
                {
                    ExecuteRegularRenovations(renovation);
                }
                else if(renovation.Type == TypeOfRenovation.Merging)
                {
                    //In merging we just remove the room that has id_other_room and we transfer equipment to the room that has id_room

                    ExecuteMergingRenovations(renovation);

                }
                else
                {
                    //In spliting we just look at the type of id_room and we create new room with new id( basically one room remains and new is formed with the same type attribute and 0 equipment),
                    //and all equipment is returned to warehouse leaving to hospital manager to manually allocate equipment in already existing equipment moving manager
                    ExecuteSplitingRenovations(renovation);
                }
            }
        }

        private void ExecuteRegularRenovations(Renovation renovation)
        {
            string deleteRegularRenovationQuery = "delete from Renovations where id_room = " + renovation.RoomId;
            RoomRep.UpdateContent(deleteRegularRenovationQuery);
        }

        private void ExecuteMergingRenovations(Renovation renovation)
        {
            string getEquipmentInRoomQuery = "select * from RoomHasEquipment where id_room = " + renovation.SecondRoomId;
            List<RoomHasEquipment> equipmentToBeMoved = RoomRep.GetEquipmentInRoom(getEquipmentInRoomQuery);

            foreach (RoomHasEquipment equipment in equipmentToBeMoved)
            {
                //checking to see if there is an instance of RoomhasEquipment for room that we are putting equipment in
                string checkQuery = "select * from RoomHasEquipment where id_room = " + renovation.RoomId + " and id_equipment = " + equipment.EquipmentId + "";
                List<RoomHasEquipment> checkNumber = RoomRep.GetEquipmentInRoom(checkQuery);


                if (checkNumber.Count == 0)
                {
                    //if there is not that particular instance we create new one with amount of 0
                    string insertQueryDestination = "insert into RoomHasEquipment (id_room, id_equipment, amount) values (" + renovation.RoomId + ", " + equipment.EquipmentId + ", 0)";
                    RoomRep.UpdateContent(insertQueryDestination);
                }

                string updateFirstRoomEquipment = "update RoomHasEquipment set amount = amount + " + equipment.Quantity + " where id_room = " + renovation.RoomId + " and id_equipment = " + equipment.EquipmentId;
                RoomRep.UpdateContent(updateFirstRoomEquipment);

            }

            string deleteMergingRenovationQuery = "delete from Renovations where id_room = " + renovation.RoomId + " and id_other_room = " + renovation.SecondRoomId;
            RoomRep.UpdateContent(deleteMergingRenovationQuery);


            string deleteOtherRoomQuery = "delete from Rooms where ID = " + renovation.SecondRoomId;
            RoomRep.UpdateContent(deleteOtherRoomQuery);
        }

        private void ExecuteSplitingRenovations(Renovation renovation)
        {
            string getEquipmentInFirstRoomQuery = "select * from RoomHasEquipment where id_room = " + renovation.RoomId;
            List<RoomHasEquipment> equipmentToBeMovedToWarehouse = RoomRep.GetEquipmentInRoom(getEquipmentInFirstRoomQuery);

            Room warehouse = RoomRep.GetRooms("select * from Rooms where Type = 'Warehouse'")[0];
            Room firstRoom = RoomRep.GetRooms("select * from Rooms where ID = " + renovation.RoomId)[0];

            foreach (RoomHasEquipment equipment in equipmentToBeMovedToWarehouse)
            {
                string checkQuery = "select * from RoomHasEquipment where id_equipment = " + equipment.EquipmentId + " and id_room in (select ID from Rooms where Type = 'Warehouse'";
                List<RoomHasEquipment> checkNumber = RoomRep.GetEquipmentInRoom(checkQuery);


                if (checkNumber.Count == 0)
                {
                    string insertQueryDestination = "insert into RoomHasEquipment (id_room, id_equipment, amount) values (" + warehouse.ID + ", " + equipment.EquipmentId + ", 0)";
                    RoomRep.UpdateContent(insertQueryDestination);
                }

                string updateWarehouseEquipment = "update RoomHasEquipment set amount = amount + " + equipment.Quantity + " where id_room = " + warehouse.ID + " and id_equipment = " + equipment.EquipmentId;
                RoomRep.UpdateContent(updateWarehouseEquipment);

            }

            string deleteSplittingRenovationQuery = "delete from Renovations where id_room = " + renovation.RoomId;
            RoomRep.UpdateContent(deleteSplittingRenovationQuery);

            var query = "INSERT INTO rooms(type) VALUES('"+firstRoom.Type.ToString()+"')";
            InsertSingle(query);
        }

        public void UpdateTransfers()
        {
            string unrealizedTransfersQuery = "select * from EquipmentTransferHistory where isExecuted = false";

            List<TransferHistoryOfEquipment> unrealizedTransfers;
            unrealizedTransfers = RoomRep.GetTransferHistory(unrealizedTransfersQuery);

            Connection.Close();
        }

        private static void InsertDiseaseHistories()
        {
            List<DiseaseHistory> diseaseHistories = GetDiseaseHistories();

            foreach (DiseaseHistory diseaseHistory in diseaseHistories)
            {
                var query = "INSERT INTO DiseaseHistory(id_medicalRecord, nameOfDisease) VALUES("+diseaseHistory.MedicalRecordId+", '"+diseaseHistory.Name+"')";
                InsertSingle(query);
            }
        }
        private static List<DiseaseHistory> GetDiseaseHistories()
        {
            List<DiseaseHistory> diseaseHistory = new List<DiseaseHistory>();
            List<String> medicalRecordIds = GetMedicalRecordIds();


            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[0]), "Dementia"));
            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[0]), "Alzheimer"));
            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[1]), "Alzheimer"));
            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[1]), "Diabetes"));
            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[2]), "Diabetes"));
            diseaseHistory.Add(new DiseaseHistory(Convert.ToInt32(medicalRecordIds[2]), "Alzheimer"));
            return diseaseHistory;
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
                List<String> tableNames = new List<string>() {
                    "DoctorSurveys", "BlockedPatients", "PatientExaminationChanges", "ReceiptMedications", "Receipt", "HospitalSurveys", "PatientAlergicTo", "MedicalRecord", "Examination", "Instructions", "DiseaseHistory", "RequestForDinamicEquipment", "users", "rooms", "medications", "Ingredients", "ReferralLetter", "MedicationContainsIngredient", "RejectedMedications", "Equipment", "Anamnesises", "RoomHasEquipment", "EquipmentTransferHistory"
                };


                // Deleting all records from database
                foreach (string tableName in tableNames)
                    DeleteTableData(tableName);

                Connection.Close();
            }
        }
        private void DeleteTableData(string tableName)
        {
            DatabaseHelpers.ExecuteNonQueries("Delete from " + tableName, Connection);
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

        private static List<String> GetIngredientIDs()
        {
            var query = "select ID from Ingredients";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        private static List<Equipment> GetEquipment()
        {
            List<Equipment> equipment = new List<Equipment>();

            equipment.Add(new Equipment("Bed", Equipment.EquipmentType.Static ));
            equipment.Add(new Equipment("Bandage", Equipment.EquipmentType.Dynamic));
            equipment.Add(new Equipment("Gauze", Equipment.EquipmentType.Dynamic));
            equipment.Add(new Equipment("Injection", Equipment.EquipmentType.Dynamic));

            return equipment;
        }

        private static void InsertEquipment()
        {
            List<Equipment> equipment = GetEquipment();

            foreach (Equipment singleEquipment in equipment)
            {
                var query = "INSERT INTO equipment(nameOf, type) VALUES('"+singleEquipment.Name+"', '"+singleEquipment.Type.ToString()+"')";
                InsertSingle(query);
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
                var query = "INSERT INTO hospitalSurveys(quality," +
               "higyene," +
               "isSatisfied," +
               "wouldRecomend," +
               "comment, id_patient) VALUES("+hospitalSurvey.QualityOfService+", "+ hospitalSurvey.Cleanliness + ", "+ hospitalSurvey .Happiness+ ", "+hospitalSurvey.WouldRecommend+", '"+ hospitalSurvey .Comment+ "', "+Convert.ToInt32(patientIDs[0])+")";
                InsertSingle(query);
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

            return users;
        }

        private static void InsertUsers()
        {
            List<User> users = GetUsers();

            foreach(User user in users)
            {
                var query = "INSERT INTO users(usrnm, pass, role) VALUES('" + user.Username + "', '" + user.Password + "', '" + user.Role.ToString() + "')";
                InsertSingle(query);
            }
  
        }
        private static void InsertSingle(string query)
        {
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }


        private static void InsertDoctors()
        {
            List<Doctor> doctors = GetDoctors();

            foreach (Doctor doctor in doctors)
            {
                var query = "INSERT INTO Doctors(firstName, lastName, user_id, speciality) VALUES('" + doctor.FirstName + "', '" + doctor.LastName + "', "+doctor.UserId+", '" + doctor.Speciality.ToString() + "')";
                InsertSingle(query);
            }
        }

        private static List<Renovation> GetRenovations()
        {
            List<Renovation> renovations = new List<Renovation>();
            List<String> roomIDs = GetRoomIDs();

            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[0]), DateTime.Now, DateTime.Now.AddMonths(1)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[1]), DateTime.Now, DateTime.Now.AddMonths(2)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[2]), DateTime.Now, DateTime.Now.AddMonths(3)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[5]), DateTime.Now, DateTime.Now.AddMonths(4)));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[4]), DateTime.Now, DateTime.Now.AddMonths(5), Convert.ToInt32(roomIDs[3]), TypeOfRenovation.Merging));
            renovations.Add(new Renovation(Convert.ToInt32(roomIDs[6]), DateTime.Now, DateTime.Now.AddMonths(6), -1, TypeOfRenovation.Splitting));


            return renovations;
        }

        private static void InsertRenovations()
        {
            List<Renovation> renovations = GetRenovations();

            foreach (Renovation renovation in renovations)
            {
                InsertSingleRenovation(renovation);
            }
        }

        private static void InsertSingleRenovation(Renovation renovation)
        {
            var query = "INSERT INTO Renovations(id_room, dateOfStart, dateOfFinish, id_other_room, renovationType) VALUES(@id_room, @startingDate, @ending_date, @id_other_room, @renovationType)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_room", renovation.RoomId);
                cmd.Parameters.AddWithValue("@startingDate", renovation.StartingDate.ToString());
                cmd.Parameters.AddWithValue("@ending_date", renovation.EndingDate.ToString());
                if(renovation.SecondRoomId == -1)
                {
                    cmd.Parameters.Add("@id_other_room", OleDbType.Integer).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@id_other_room", renovation.SecondRoomId);
                }
                
                cmd.Parameters.AddWithValue("@renovationType", renovation.Type.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        private static List<TransferHistoryOfEquipment> GetTransferHistoryOfEquipment()
        {
            List<TransferHistoryOfEquipment> transferHistoryOfEquipment = new List<TransferHistoryOfEquipment>();
            List<String> roomIDs = GetRoomIDs();
            List<String> equipmentIDs = GetEquipmentIDs();
                                                          
            
            transferHistoryOfEquipment.Add(new TransferHistoryOfEquipment(Convert.ToInt32(roomIDs[0]), Convert.ToInt32(roomIDs[4]), DateTime.Now, true, 5, Convert.ToInt32(equipmentIDs[0])));
            transferHistoryOfEquipment.Add(new TransferHistoryOfEquipment(Convert.ToInt32(roomIDs[1]), Convert.ToInt32(roomIDs[3]), DateTime.Now, true, 4, Convert.ToInt32(equipmentIDs[1]))); 

            return transferHistoryOfEquipment;
        }

        private static void InsertTransferHistoryOfEquipment()
        {
            List<TransferHistoryOfEquipment> transferHistoryOfEquipment = GetTransferHistoryOfEquipment();

            foreach (TransferHistoryOfEquipment singleTransferHistoryOfEquipment in transferHistoryOfEquipment)
            {
                var query = "INSERT INTO EquipmentTransferHistory(id_original_room, id_new_room, dateOfChange, isExecuted, amount, id_equipment) " +
                "VALUES(" + singleTransferHistoryOfEquipment.FirstRoomId + ", " + singleTransferHistoryOfEquipment.SecondRoomId + ", '" + singleTransferHistoryOfEquipment.TransferDate.ToString() + "', " + singleTransferHistoryOfEquipment.IsExecuted + ", " + singleTransferHistoryOfEquipment.Amount + ", " + singleTransferHistoryOfEquipment.EquipmentId + ")";
                InsertSingle(query);
            }
        }


        private static List<RoomHasEquipment> GetRoomHasEquipment()
        {
            List<RoomHasEquipment> roomHasEquipment = new List<RoomHasEquipment>();
            List<String> roomIDs = GetRoomIDs();
            List<String> equipmentIDs = GetEquipmentIDs();
            Random random = new Random();

            foreach (string roomID in roomIDs)
            {
                foreach (string equipmentID in equipmentIDs)
                {
                    roomHasEquipment.Add(new RoomHasEquipment(Convert.ToInt32(equipmentID), Convert.ToInt32(roomID), random.Next(0, 20)));
                }
            }

            return roomHasEquipment;
        }

        private static void InsertRoomHasEquipment()
        {
            List<RoomHasEquipment> roomHasEquipment = GetRoomHasEquipment();

            foreach (RoomHasEquipment singleRoomHasEquipment in roomHasEquipment)
            {
                var query = "INSERT INTO RoomHasEquipment(id_room, id_equipment, amount) " +
                "VALUES(" + singleRoomHasEquipment.RoomId + ", " + singleRoomHasEquipment.EquipmentId + ", " + singleRoomHasEquipment.Quantity + ")";
                InsertSingle(query);
            }
        }


        private static List<DynamicEquipmentRequest> GetDynamicEquipmentRequests()
        {
            List<DynamicEquipmentRequest> dynamicEquipmentRequests = new List<DynamicEquipmentRequest>();
            List<String> equipmentIDs = GetEquipmentIDs();
            List<String> secreatyIDs = GetSecretaryIDs();
            dynamicEquipmentRequests.Add(new DynamicEquipmentRequest(Convert.ToInt32(equipmentIDs[0]), 10, DateTime.Now, Convert.ToInt32(secreatyIDs[0])));
            dynamicEquipmentRequests.Add(new DynamicEquipmentRequest(Convert.ToInt32(equipmentIDs[1]), 15, DateTime.Now.AddDays(-1).AddMinutes(-5), Convert.ToInt32(secreatyIDs[0])));

            return dynamicEquipmentRequests;
        }

        private static void InsertDynamicEquipmentRequests()
        {
            List<DynamicEquipmentRequest> dynamicEquipmentRequests = GetDynamicEquipmentRequests();
            List<String> secreatyIDs = GetSecretaryIDs();
            foreach (DynamicEquipmentRequest dynamicEquipmentRequest in dynamicEquipmentRequests)
            {
                var query = "INSERT INTO RequestForDinamicEquipment(id_equipment, amount, id_secretary, dateOf) VALUES(" + dynamicEquipmentRequest.EquipmentId + ", " + dynamicEquipmentRequest.Quantity + ", " + Convert.ToInt32(secreatyIDs[0]) + ", '" + dynamicEquipmentRequest.Date.ToString() + "')";
                InsertSingle(query);
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
                var query = "INSERT INTO Patients(firstName, lastName, user_id, isBlocked, notificationTime) VALUES('"+patient.FirstName+"', '"+patient.LastName+"', "+patient.UserId+", "+patient.IsBlocked+", "+2+")";
                InsertSingle(query);
            }
        }

        private static void InsertBlockedPatients()
        {
            List<BlockedPatient> blockedPatients = GetBlockedPatients();

            foreach (BlockedPatient blockedPatient in blockedPatients)
            {
                var query = "INSERT INTO BlockedPatients(id_patient, id_secretary, dateOf) VALUES("+blockedPatient.PatientID+", "+blockedPatient.SecretaryID+", '"+blockedPatient.DateOf.ToString()+"')";
                InsertSingle(query);
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


        private static void InsertSecretaries()
        {
            List<Secretary> secretaries = GetSecretaries();

            foreach (Secretary secretary in secretaries)
            {
                var query = "INSERT INTO Secretaries(firstName, lastName, user_id) VALUES('"+secretary.FirstName+"', '"+secretary.LastName+"', "+secretary.UserId+")";
                InsertSingle(query);
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

        private static void InsertManagers()
        {
            List<HospitalManager> managers = GetHospitalManagers();

            foreach (HospitalManager manager in managers)
            {
                var query = "INSERT INTO HospitalManagers(firstName, lastName, user_id) VALUES('" + manager.FirstName + "', '" + manager.LastName + "', " + manager.UserId + ")";
                InsertSingle(query);
            }

        }

        private static List<HospitalManager> GetHospitalManagers()
        {
            List<HospitalManager> hospitalManager = new List<HospitalManager>();
            List<String> userIDs = GetUserIDs(UserRole.HospitalManagers);

            hospitalManager.Add(new HospitalManager("Marko", "Markovic", Convert.ToInt32(userIDs[0])));

            return hospitalManager;
        }
        private static void InsertRooms()
        {
            List<Room> rooms = GetRooms();
            foreach(Room room in rooms)
            {
                var query = "INSERT INTO rooms(type) VALUES('" + room.Type.ToString() + "')";
                InsertSingle(query);
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

        private static void InsertMedications()
        {
            List<Medication> medications = GetMedications();
            foreach (Medication medication in medications)
            {
                var query = "INSERT INTO medications(nameOfMedication, status) VALUES('" + medication.Name + "', '" + medication.Status.ToString() + "')";
                InsertSingle(query);
            }
        }
        private static List<Medication> GetMedications()
        {
            List<Medication> medications = new List<Medication>();

            medications.Add(new Medication("Brufen", MedicationStatus.Approved));
            medications.Add(new Medication("Analgin", MedicationStatus.Denied));
            medications.Add(new Medication("Panklav", MedicationStatus.Approved));
            medications.Add(new Medication("Aspirin", MedicationStatus.Approved));
            medications.Add(new Medication("Altal", MedicationStatus.InProgress));
            medications.Add(new Medication("Penicilin", MedicationStatus.InProgress));

            return medications;
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
                var query = "INSERT INTO RejectedMedications(id_medication, id_doctor, description) VALUES(" + rejectedMedication.MedicationID + ", " + rejectedMedication.DoctorID + ", '" + rejectedMedication.Description + "')";
                InsertSingle(query);
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

        private static void InsertIngredients()
        {
            List<Ingredient> ingredients = GetIngredients();

            foreach (Ingredient ingredient in ingredients)
            {
                var query = "INSERT INTO Ingredients(nameOfIngredient) VALUES('"+ingredient.Name+"')";
                InsertSingle(query);
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

        private static List<String> GetMedicalRecordIds()
        {
            var query = "select ID from MedicalRecord";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);

        }

        private static void InsertPatientAlergies()
        {
            List<string> patientIDs = GetPatientIds();
            List<string> ingredientIDs = DatabaseHelpers.ExecuteReaderQueries("select id from ingredients", Connection);

            for(int i =0;i < patientIDs.Count();i++)
            {
                var query = "INSERT INTO PatientAlergicTo(id_patient, id_ingredient) VALUES("+Convert.ToInt32(patientIDs[i])+", "+Convert.ToInt32(ingredientIDs[i])+")";
                InsertSingle(query);

            }
        }

        private static void InsertMedicationsIngredients()
        {
            List<MedicationsIngredient> medicationsIngredients = GetMedicationsIngredients();

            foreach (MedicationsIngredient medicationsIngredient in medicationsIngredients)
            {
                var query = "INSERT INTO MedicationContainsIngredient(id_medication, id_ingredient) VALUES(" + medicationsIngredient.MedicationID + ", " + medicationsIngredient.IngredientID + ")";
                InsertSingle(query);
            }

        }

        private static List<MedicationsIngredient> GetMedicationsIngredients()
        {
            List<MedicationsIngredient> medicationsIngredients = new List<MedicationsIngredient>();
            List<String> medicationsIDs = GetMedicationIds();
            List<String> ingredientsIDs = DatabaseHelpers.ExecuteReaderQueries("select id from Ingredients", Connection);

            medicationsIngredients.Add(new MedicationsIngredient(Convert.ToInt32(medicationsIDs[0]), Convert.ToInt32(ingredientsIDs[0])));
            medicationsIngredients.Add(new MedicationsIngredient(Convert.ToInt32(medicationsIDs[1]), Convert.ToInt32(ingredientsIDs[1])));
            medicationsIngredients.Add(new MedicationsIngredient(Convert.ToInt32(medicationsIDs[2]), Convert.ToInt32(ingredientsIDs[2])));

            return medicationsIngredients;
        }

        private static void InsertMedicalRecords()
        {
            List<MedicalRecord> medicalRecords = GetMedicalRecords();

            foreach (MedicalRecord medicalRecord in medicalRecords)
            {
                var query = "INSERT INTO MedicalRecord(id_patient, height, weight) VALUES(" + medicalRecord.IdPatient + ", " + medicalRecord.Height + ", " + medicalRecord.Weight + ")";

                InsertSingle(query);
            }
        }

        private static List<MedicalRecord> GetMedicalRecords()
        {
            List<MedicalRecord> medicalRecords = new List<MedicalRecord>();
            List<String> patientIDs = GetPatientIds();

            medicalRecords.Add(new MedicalRecord(Convert.ToInt32(patientIDs[0]), 85, 185));
            medicalRecords.Add(new MedicalRecord(Convert.ToInt32(patientIDs[1]), 92, 192));
            medicalRecords.Add(new MedicalRecord(Convert.ToInt32(patientIDs[2]), 75, 183));

            return medicalRecords;
        }

        private static void InsertExaminations()
        {
            List<Examination> examinations = GetExaminations();

            foreach (Examination examination in examinations)
            {
                var query = "INSERT INTO Examination(id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration) " +
                "VALUES(" + examination.IdDoctor + ", " + examination.IdPatient + ", " + examination.IsEdited + ", " + examination.IsCancelled + ", " + examination.IsFinished + ", '" + examination.DateOf.ToString() + "', '" + examination.TypeOfExamination.ToString() + "', " + examination.IsUrgent + ", " + examination.IdRoom + ", " + examination.Duration + ")";
                InsertSingle(query);
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
            examinations.Add(new Examination(Convert.ToInt32(doctorIDs[0]), Convert.ToInt32(patientIDs[2]), false, false, false, new DateTime(2022, 4, 28, 10, 10, 10), TypeOfExamination.BasicExamination, false, Convert.ToInt32(roomIDs[4]), 15));
            examinations.Add(new Examination(Convert.ToInt32(doctorIDs[1]), Convert.ToInt32(patientIDs[2]), false, false, false, new DateTime(2022, 4, 24, 10, 10, 10), TypeOfExamination.BasicExamination, false, Convert.ToInt32(roomIDs[4]), 15));

            return examinations;
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
                var query = "INSERT INTO Anamnesises(id_examination, notice, conclusions, dateOf) VALUES("+anamnesis.ExaminationID+", '"+anamnesis.Notice+"', '"+anamnesis.Conclusions+"', '"+anamnesis.DateOf.ToString()+"')";
                InsertSingle(query);
            }
        }

        private static List<Anamnesis> GetAnamnesises()
        {
            List<Anamnesis> anamnesises = new List<Anamnesis>();
            List<String> examinationIDs = GetExaminationIDs();

            anamnesises.Add(new Anamnesis(Convert.ToInt32(examinationIDs[0]), "Runny nose and coughs alot.", "Patient should drink antibiotics.", new DateTime(2022, 4, 26)));
            anamnesises.Add(new Anamnesis(Convert.ToInt32(examinationIDs[4]), "Patient showed signs of Corona Virus: Headeches, High Temperature, Cough and Sleep Depravation", "Patient should go and take his blood and come back with results.", new DateTime(2022, 4, 28)));
            anamnesises.Add(new Anamnesis(Convert.ToInt32(examinationIDs[5]), "Patient is Fatigueing very quickly when training", "Rest for a few days and come back for a check up", new DateTime(2022, 4, 24)));

            return anamnesises;
        }

        private static void InsertInstructions()
        {
            List<Instruction> instructions = GetInstructions();

            foreach (Instruction instruction in instructions)
            {
                var query = "INSERT INTO Instructions(startTime, timesPerDay, description) VALUES('"+instruction.StartTime.ToString()+"', "+instruction.TimesPerDay+", '"+instruction.Description+"')";
                InsertSingle(query);
            }
        }

        private static List<Instruction> GetInstructions()
        {
            List<Instruction> instructions = new List<Instruction>();
            DateTime tomorrow = DateTime.Now.AddHours(20);

            instructions.Add(new Instruction(tomorrow, 3,
                "Morbi tincidunt augue interdum velit euismod in pellentesque massa placerat. Pharetra convallis posuere " +
                "morbi leo urna molestie. Mattis ania at quis risus sed vulputate. Et netus et malesuada falis nibh praesent ."));
            instructions.Add(new Instruction(tomorrow, 4,
                "Morbi tincidunt augue interdum velit euismod in pellentesque massa placerat. Pharetra convallis posuere"
                ));
            instructions.Add(new Instruction(DateTime.Now.AddHours(-1), 2,
                "Morbi tincidunt augue interdum velit euismod in pellentesque massa placerat. Pharetra convallis posuere"
                ));

            return instructions;
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
                var query = "INSERT INTO Receipt(id_instructions, id_doctor, id_patient, dateOf) VALUES(" + receipt.InstructionId + ", " + receipt.DoctorId + ", " + receipt.PatientId + ", '" + receipt.DateOfHandout.ToString() + "')";
                InsertSingle(query);
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
                var query = "INSERT INTO PatientExaminationChanges(id_patient, typeOfChange, dateOf) VALUES("+desiredPatientId+", '"+TypeOfChange.Edit.ToString()+"', '"+DateTime.Now.AddDays(- 2 - i).ToString()+"')";
                InsertSingle(query);
            }
            
        }

        private static List<ReferralLetter> GetInsertReferralLetters()
        {
            List<ReferralLetter> letters = new List<ReferralLetter>();
            List<string> patientsIds = GetPatientIds();
            List<string> doctorsIds = GetDoctorIds();
            letters.Add(new ReferralLetter(Convert.ToInt32(doctorsIds[0]), Convert.ToInt32(patientsIds[2]), Convert.ToInt32(doctorsIds[1]), TypeOfExamination.BasicExamination, DoctorSpeciality.Neurology));

            return letters;
        }

        private static void InsertReferralLetters()
        {
            List<ReferralLetter> referralLetters = GetInsertReferralLetters();

            foreach (ReferralLetter referralLetter in referralLetters)
            {
                var query = "INSERT INTO ReferralLetter(id_doctor, id_patient, id_forwarded_doctor, typeOfExamination, speciality) VALUES(" + referralLetter.CurrentDoctorID + ", " + referralLetter.CurrentPatientID + ", " + referralLetter.ForwardedDoctorID + ", '" + referralLetter.ExaminationType.ToString() + "', '" + referralLetter.Speciality.ToString() + "')";
                InsertSingle(query);
            }
        }
        private static void InsertDoctorSurveys()
        {
            Dictionary<int, List<int>> combinedIds = GetCombinedIdsFromDoctorsAndPatients();

            foreach (KeyValuePair<int, List<int>> entry in combinedIds)
            {
                Random rand = new Random();

                int grade = rand.Next(1, 6);
                int quality = rand.Next(1, 6);
                bool wouldReccomend = grade > 3 && quality > 3;
                string comment = GetCommentBasedOnGrade(grade);

                var query = "INSERT INTO DoctorSurveys(id_doctor, id_patient, doctorGrade, quality, wouldRecommend, comment) VALUES(" + entry.Value[1] + ", " + entry.Value[0] + ", " + grade + ", " + quality + ", " + wouldReccomend + ", '"+comment+"')";

                InsertSingle(query);
            }
        }

        private static Dictionary<int, List<int>> GetCombinedIdsFromDoctorsAndPatients()
        {
            List<String> doctorIds = GetDoctorIds();
            List<String> patientIds = GetPatientIds();
            Dictionary<int, List<int>> combinedIds = new Dictionary<int, List<int>>();
            for (int i = 0; i < doctorIds.Count(); i++)
            {
                for (int j = 0; j < patientIds.Count(); j++)
                {
                    int patientId = Convert.ToInt32(patientIds[j]);
                    int doctorId = Convert.ToInt32(doctorIds[i]);

                    combinedIds[i * doctorIds.Count + j] = new List<int>() { patientId, doctorId};
                }
            }

            return combinedIds;
        }
        private static string GetCommentBasedOnGrade(int grade)
        {
            List<String> comments = new List<string>()
            {
                "The doctor was very inapropriate. Awful experience.",
                "Bad management and appointment accuracy",
                "It was ok.",
                "Went very fast and proffesional, but a bit expensive.",
                "Im very satisfied"
            };
            return comments[grade - 1];
        }

    }
}
