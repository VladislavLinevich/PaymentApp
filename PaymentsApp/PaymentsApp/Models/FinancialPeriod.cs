using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace PaymentsApp.Models
{
    public class FinancialPeriod
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PaymentLimit { get; set; }

        [Required]
        public bool IsClosed { get; set; }

    }
}
