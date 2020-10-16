using PrescriptionDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using PrescriptionManagerUI.Library.Models;
using PrescriptionManagerUI.Library.API;

namespace PrescriptionDataManager.Library.API
{
    public class PatientEndpoint : IPatientEndpoint
    {

        IAPIHelper _apiHelper;

        public PatientEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

      

        public async Task<List<PatientModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Patient"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<PatientModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<PatientPrescriptionRenewalModel>> GetPatientPrescriptionRenewals()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Patient/Renewals"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<PatientPrescriptionRenewalModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<PatientModel> GetPatientDetails(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/PatientDetails?Id={id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PatientModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<PatientNoteModel>> GetPatientNotes(int patientId)
        {
            using(HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/Notes?patientId={patientId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<PatientNoteModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task AddPatient(PatientModel patientData)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Patient/Add", patientData))
            {
                if (response.IsSuccessStatusCode)
                {
                    //Log Success Call
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task EditPatient(PatientModel patientData)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Patient/Edit", patientData))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Log success call
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }




    }
}
