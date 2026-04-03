using Lab4.Model.Additional;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Model.Main
{
    public class Application
    {
        public long id { get; set; }
        public string name { get; set; }
        [Column("submission_date")]
        public DateTime submissionDate { get; set; }
        [Column("application_type_id")]
        public long applicationTypeId { get; set; }

        [Column("application_status_id")]
        public long applicationStatusId { get; set; }
        [Column("deanery_staff_id")]
        public long deaneryStaffId { get; set; }

        public ApplicationType applicationType { get; set; }

        [ForeignKey(nameof(applicationStatusId))]
        public ApplicationStatus applicationStatus { get; set; }
        public DeaneryStaff deaneryStaff { get; set; }
        public Order order { get; set; }
    }
}
