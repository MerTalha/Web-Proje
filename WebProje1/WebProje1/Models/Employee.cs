using System.ComponentModel.DataAnnotations;
using WebProje1.Models;

namespace WebProje1.Areas.Identity.Data
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Employee Name is required.")]
        public string EmployeeName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNO { get; set; }
        public int Salary { get; set; }

        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }

    }
}
