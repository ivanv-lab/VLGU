using Lab4.Model.Additional;

namespace Lab4.Model.Main
{
    public class Student
    {
        public long id { get; set; }
        public string fullname { get; set; }
        public DateOnly Birthdate { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string passportSerial { get; set; }
        public string passportNumber { get; set; }
        public string passportRegistration { get; set; }
        public string passportIssued { get; set; }
        public string recordBookNumber { get; set; }
        public DateOnly enrollmentDate { get; set; }
        public int statusId {  get; set; }


        public StudentStatus status {  get; set; }
        public ICollection<AcademicRecord> academicRecords { get; set;  }
    }
}
