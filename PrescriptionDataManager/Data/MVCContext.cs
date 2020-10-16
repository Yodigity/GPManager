using PrescriptionDataManager.Library.Models;
using PrescriptionDataManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PrescriptionDataManager.Data
{
    public class MVCContext : DbContext
    {
        public MVCContext() : base()
        {
        }

        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<PrescriptionModel> Prescriptions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<PrescriptionDataManager.Models.LoginUserModel> UserModels { get; set; }
    }
}