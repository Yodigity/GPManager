using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PrescriptionDataManager.Models;
using PrescriptionDataManager.Library.Models;

namespace PrescriptionDataManager.Data
{
    public class DataInitialiser : System.Data.Entity. DropCreateDatabaseIfModelChanges<MVCContext>
    {



        protected override void Seed(MVCContext context)
        {
            var patients = new List<PatientModel>
            {
            new PatientModel{ID=1, FirstName="Carson",LastName="Alexander",DOB = new DateTime(1993-08-10), Email = "carsonalex@test.com", PhoneNumber= "54837829014"},
            new PatientModel{ID=2, FirstName="Meredith",LastName="Alonso", DOB = new DateTime(1993-08-10), Email= "mereal@test.com", PhoneNumber= "54542829014"},
            new PatientModel{ID=3, FirstName="Arturo",LastName="Anand", DOB =new DateTime(1993-08-10), Email= "ArunoAnand@test.com", PhoneNumber= "54837895614"},
            new PatientModel{ID=4, FirstName="Gytis",LastName="Barzdukas",DOB = new DateTime(1993-08-10), Email= "GytisBarz@test.com", PhoneNumber= "12890232194"},
            new PatientModel{ID=5, FirstName="Yan",LastName="Li", DOB = new DateTime(1993-08-10), Email= "Yan_x@test.com",PhoneNumber= "54837829014"},
            new PatientModel{ID=6, FirstName="Peggy",LastName="Justice", DOB = new DateTime(1993-08-10), Email= "PeggyJustice@test.com", PhoneNumber= "54837829014", },
            new PatientModel{ID=7, FirstName="Laura",LastName="Norman", DOB = new DateTime(1993-08-10),Email= "LauraNorman@test.com", PhoneNumber= "54837829014"},
            new PatientModel{ID=8, FirstName="Nino",LastName="Olivetto", DOB =new DateTime(1993-08-10), Email= null, PhoneNumber= "54837829014"}
            };

            patients.ForEach (p => context.Patients.Add(p));
            context.SaveChanges();

            var prescriptions = new List<PrescriptionModel>
            {
                new PrescriptionModel{ID= 32341, PatientID= 2, DrugName="Warfarin", Dosage=1.50, RenewalDate= DateTime.Now.AddDays(14)},
                new PrescriptionModel{ID= 83241, PatientID= 1, DrugName="Paracetamol", Dosage=500, RenewalDate= DateTime.Now.AddDays(21)},
                new PrescriptionModel{ID= 33211, PatientID= 4, DrugName="Warfarin", Dosage=1.50, RenewalDate= DateTime.Now.AddDays(14)},
                new PrescriptionModel{ID= 12341, PatientID= 2, DrugName="Riboflaxin", Dosage=200, RenewalDate= DateTime.Now.AddDays(14)},
                new PrescriptionModel{ID= 21141, PatientID= 5, DrugName="Amoxicilin", Dosage=20, RenewalDate= DateTime.Now.AddDays(7)},
                new PrescriptionModel{ID= 23221, PatientID= 7, DrugName="Pennicilin", Dosage=30, RenewalDate= DateTime.Now.AddDays(14)},
                new PrescriptionModel{ID= 44211, PatientID= 1, DrugName="Warfarin", Dosage=1.50, RenewalDate= DateTime.Now.AddDays(21)},
                new PrescriptionModel{ID= 21653, PatientID= 8, DrugName="ibuprofen", Dosage=1.50, RenewalDate= DateTime.Now.AddDays(14)}
            };

            prescriptions.ForEach(pre => context.Prescriptions.Add(pre));
            context.SaveChanges();
          
        }
    }
}

