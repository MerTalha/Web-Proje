using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebProje1.Areas.Identity.Data;

namespace WebProje1.Models
{
    public class EmployeeProject
    {
        [Key]
        [Column(Order = 1)]

        public int ProjectId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}
