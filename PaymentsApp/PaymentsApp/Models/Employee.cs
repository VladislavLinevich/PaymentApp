using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Patronimic { get; set; }

        [Required]
        public bool IsFired { get; set; }

        [Column(TypeName = "nvarchar(1024)")]
        public string Note { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
