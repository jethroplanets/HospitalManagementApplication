using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HospitalMgt.Models;

namespace HospitalMgt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HospitalMgt.Models.Dispense> Dispense { get; set; }
        public DbSet<HospitalMgt.Models.Doctor> Doctor { get; set; }
        public DbSet<HospitalMgt.Models.Patient> Patient { get; set; }
        public DbSet<HospitalMgt.Models.Medicine> Medicine { get; set; }
        public DbSet<HospitalMgt.Models.AppUser> AspNetUsers { get; set; }
    }
}
