using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsApp.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public int CurrencyDigitalCode { get; set; }
        public Currency Currency { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime DatePayment { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }
    }
}
