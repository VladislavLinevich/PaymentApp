using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsApp.Models
{
    public class Currency
    {
        [Key]
        public int DigitalCode { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
