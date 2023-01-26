namespace Docusign.Models
{
    public class PropertiesCommitmentTerm
    {
        #region Patient
        public string PatientFullName { get; set; }
        public string PatientBirthDate { get; set; }
        public string PatientCPF { get; set; }
        public string PatientEmail { get; set; }
        public string PatientFullAddress { get; set; }
        public string PatientGender { get; set; }
        public string PatientPhoneNumber { get; set; }
        #endregion

        #region Doctor
        public string DoctorFullName { get; set; }
        public string DoctorCRMNumber { get; set; }
        public string DoctorCRMUF { get; set; }
        public string DoctorEmail { get; set; }
        #endregion

        #region Witness
        public string WitnessFullName { get; set; }
        public string WitnessRG { get; set; }
        public string WitnessEmail { get; set; }
        #endregion

        #region Clinic
        public string ClinicFullName { get; set; }
        #endregion

        #region Exam
        public string ExamRequested { get; set; }
        public string ExamType { get; set; }
        public string BiologicalSampleType { get; set; }
        #endregion

        #region Laboratory
        public string LabFullName { get; set; }
        #endregion

        #region Sign
        public string SignCity { get; set; }
        public string SignDay { get; set; }
        public string SignMonth { get; set; }
        public string SignYearLastTwoDigits { get; set; }
        public string Voucher { get; set; }
        #endregion

        #region DraftInformation
        public string ProfileSigner1 { get; set; } // string "Patient" or "Caregiver"
        public string ProfileSigner2 { get; set; } // string "Doctor" ou  "Witness"
        public string TemplateId { get; set; }
        #endregion

    }
}
