using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;
using HealthCareSystem.Core.Surveys.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Surveys
{
    
    class SurveyService
    {
        public static int[] GetNumberOfHospitalGrades(List<HospitalSurvey> hospitalSurveys, bool isHygene)
        {
            int[] numberOfGrades = new int[5];


            if (isHygene)
            {
                foreach (HospitalSurvey hospitalSurvey in hospitalSurveys)
                {
                    switch (hospitalSurvey.Cleanliness)
                    {
                        case 1:
                            numberOfGrades[0]++;
                            break;
                        case 2:
                            numberOfGrades[1]++;
                            break;
                        case 3:
                            numberOfGrades[2]++;
                            break;
                        case 4:
                            numberOfGrades[3]++;
                            break;
                        case 5:
                            numberOfGrades[4]++;
                            break;
                    }
                }
            }
            else
            {
                foreach (HospitalSurvey hospitalSurvey in hospitalSurveys)
                {
                    switch (hospitalSurvey.QualityOfService)
                    {
                        case 1:
                            numberOfGrades[0]++;
                            break;
                        case 2:
                            numberOfGrades[1]++;
                            break;
                        case 3:
                            numberOfGrades[2]++;
                            break;
                        case 4:
                            numberOfGrades[3]++;
                            break;
                        case 5:
                            numberOfGrades[4]++;
                            break;
                    }
                }
            }

            return numberOfGrades;
        }
        public static double SumAllHospitalGrades(List<HospitalSurvey> hospitalSurveys, bool isHygene)
        {
            double sum = 0;
            if (isHygene)
            {
                foreach (HospitalSurvey hospitalSurvey in hospitalSurveys)
                {
                    sum += hospitalSurvey.Cleanliness;
                }
            }
            else
            {
                foreach (HospitalSurvey hospitalSurvey in hospitalSurveys)
                {
                    sum += hospitalSurvey.QualityOfService;
                }
            }

            return sum;
        }

        public static Dictionary<Doctor, double> GetDoctorsAndAverages(List<DoctorSurvey> doctorSurveys, BindingList<Doctor> doctors)
        {
            Dictionary<Doctor, double> rankings = new Dictionary<Doctor, double>();
            
            
            foreach(Doctor doctor in doctors) {
                double[] averages = GetAverageDoctorGrades(doctorSurveys, doctor.ID);
                double sumOfAverages = averages[0] + averages[1];
                rankings.Add(doctor, sumOfAverages);
            }

            return rankings;

        }

        public static double[] GetAverageDoctorGrades(List<DoctorSurvey> doctorSurveys, int doctorId)
        {
            double[] avgGrades = new double[2];
            double sumDoctorGrade = 0;
            double sumQualityGrade = 0;
            int countDoctorGrade = 0;
            int countQualityGrade = 0;

            foreach(DoctorSurvey doctorSurvey in doctorSurveys)
            {
                
                if(doctorSurvey.DoctorId == doctorId)
                {
                    sumDoctorGrade += doctorSurvey.Grade;
                    sumQualityGrade += doctorSurvey.Quality;
                    countDoctorGrade++;
                    countQualityGrade++;
                }
            }

            avgGrades[0] = sumDoctorGrade / countDoctorGrade;
            avgGrades[1] = sumQualityGrade / countQualityGrade;

            return avgGrades;
        }

        public static int[] GetNumberOfDoctorGrades(List<DoctorSurvey> doctorSurveys, bool isDoctorGrade, int doctorId)
        {
            int[] numberOfGrades = new int[5];


            if (isDoctorGrade)
            {

                foreach (DoctorSurvey doctorSurvey in doctorSurveys)
                {
                    if(doctorSurvey.DoctorId == doctorId)
                    {
                        switch (doctorSurvey.Grade)
                        {
                            case 1:
                                numberOfGrades[0]++;
                                break;
                            case 2:
                                numberOfGrades[1]++;
                                break;
                            case 3:
                                numberOfGrades[2]++;
                                break;
                            case 4:
                                numberOfGrades[3]++;
                                break;
                            case 5:
                                numberOfGrades[4]++;
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (DoctorSurvey doctorSurvey in doctorSurveys)
                {
                    if(doctorSurvey.DoctorId == doctorId)
                    {
                        switch (doctorSurvey.Quality)
                        {
                            case 1:
                                numberOfGrades[0]++;
                                break;
                            case 2:
                                numberOfGrades[1]++;
                                break;
                            case 3:
                                numberOfGrades[2]++;
                                break;
                            case 4:
                                numberOfGrades[3]++;
                                break;
                            case 5:
                                numberOfGrades[4]++;
                                break;
                        }
                    }
                }
            }

            return numberOfGrades;
        }
    }
}
