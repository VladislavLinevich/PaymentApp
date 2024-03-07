using Microsoft.EntityFrameworkCore;
using PaymentsApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsApp.Data
{
    public class PaymentDbContext: DbContext
    {
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<FinancialPeriod> FinancialPeriod { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "payments.db");
            string dbPath = @"C:\Users\uladl\PaymentApp\PaymentsApp\PaymentsApp\Database\payments.db";

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FinancialPeriod>()
                .HasIndex(f => f.StartDate)
            .IsUnique();

            builder.Entity<FinancialPeriod>().Property(f => f.PaymentLimit).HasDefaultValue(0);

            builder.Entity<FinancialPeriod>()
            .ToTable(f => f.HasCheckConstraint("CK_FinancialPeriod_IsClosed", "IsClosed IN (0, 1)"));

            builder.Entity<Employee>()
           .ToTable(e => e.HasCheckConstraint("CK_Employee_IsFired", "IsFired IN (0, 1)"));
        }
    }
}
