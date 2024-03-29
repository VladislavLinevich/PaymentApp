using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsApp.Models
{
    public class Department: ICloneable
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Payment> Payments { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
