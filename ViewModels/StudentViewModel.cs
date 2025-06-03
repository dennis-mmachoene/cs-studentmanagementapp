using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp.ViewModels
{
    public class StudentViewModel
    {
        [Required]
        public string StudentNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
