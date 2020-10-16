using Microsoft.AspNet.Identity;
using PrescriptionDataManager.Library.DataAccess;
using PrescriptionDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PrescriptionDataManager.Controllers
{
    public class PrescriptionController : ApiController
    {
        [Authorize(Roles = "Doctor")]
        [Route("api/Prescription/Add")]
        public void Add(PrescriptionModel prescription)
        {
            PrescriptionData data = new PrescriptionData();
            string prescriberId = RequestContext.Principal.Identity.GetUserId();
            data.AddPrescription(prescription, prescriberId);
        }

        [Authorize(Roles = "Doctor")]
        public void Edit(PrescriptionModel prescription)
        {
            PrescriptionData data = new PrescriptionData();
            string prescriberId = RequestContext.Principal.Identity.GetUserId();
            data.EditPrescription(prescription, prescriberId);
        }
    }
}
