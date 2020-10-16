using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrescriptionDataManager.Data;
using PrescriptionDataManager.Models;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using PrescriptionDataManager.Library.Models;

namespace PrescriptionDataManager.Controllers
{
   
    
    
    
    
    
    
    
    
    
    public class PatientController : Controller
    {
        private MVCContext db = new MVCContext();

        // GET: Patient
        
        public ActionResult Get()
        {

            List<PatientModel> customers = new List<PatientModel>();
            using (IDbConnection db = new SqlConnection(ConnectionHelper.CnnVal("DefaultConnection")))
            {
                Console.WriteLine(db);
                customers = db.Query<PatientModel>("dbo.Patient_GetAll", new { })
                    .ToList();
            }
            return View(customers);
            //return View(db.Patients.ToList());
        }

        // GET: Patient/Details/5
        public ActionResult Details(int? id)
        {
            /*  if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              PatientModel patientModel = db.Patients.Find(id);
              if (patientModel == null)
              {
                  return HttpNotFound();
              }
              return View(patientModel);*/
           
            using (IDbConnection db = new SqlConnection(ConnectionHelper.CnnVal("DefaultConnection")))
            {

                var customers = db.Query<PatientModel>("dbo.Patient_GetDetails @id", new { @id }).SingleOrDefault();
                var prescripts = db.Query<PrescriptionModel>("dbo.Patient_GetPrescriptionDetails @id", new { @id  }).ToList();
                foreach (var pres in prescripts)
                {
                    customers.Prescriptions.Add(pres);
                }
                return View(customers);
            }
            
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string firstName, string lastName, DateTime age, string email, string phoneNumber)
        {
            //This line was in params above.

            //[Bind(Include = "ID,FirstName,LastName,Age,Email,PhoneNumber")] PatientModel patientModel
            /*if (ModelState.IsValid)
            {
                db.Patients.Add(patientModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientModel);*/
            List<PatientModel> patients = new List<PatientModel>();
            patients.Add( new PatientModel { FirstName = firstName, LastName = lastName, DOB = age, Email = email, PhoneNumber = phoneNumber });

            using(IDbConnection db = new SqlConnection(ConnectionHelper.CnnVal("DefaultConnection")))
            {
                db.Execute("dbo.Patient_add @FirstName, @LastName, @Age, @Email, @PhoneNumber", patients);
                return RedirectToAction("Index");
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientModel patientModel = db.Patients.Find(id);
            if (patientModel == null)
            {
                return HttpNotFound();
            }


            return View(patientModel);
        }

        // POST: Patient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientModel patient)
        {
            //This line was in params above
           // [Bind(Include = "ID,FirstName,LastName,Age,Email,PhoneNumber")] PatientModel patientModel
            /* if (ModelState.IsValid)
             {
                 db.Entry(patientModel).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }*/

            using (IDbConnection db = new SqlConnection(ConnectionHelper.CnnVal("DefaultConnection")))
            {
                db.Execute("dbo.Patient_UpdateDetails @id, @firstName, @lastName, @age, @email, @phoneNumber", patient);
            }
            return RedirectToAction("Details/"+id);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientModel patientModel = db.Patients.Find(id);
            if (patientModel == null)
            {
                return HttpNotFound();
            }
            return View(patientModel);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /* PatientModel patientModel = db.Patients.Find(id);
             db.Patients.Remove(patientModel);
             db.SaveChanges();*/

            using (IDbConnection db = new SqlConnection(ConnectionHelper.CnnVal("DefaultConnection")))
            {
                db.Execute("dbo.Patient_Delete @id", new { @id });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
