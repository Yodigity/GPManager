using PrescriptionManagerUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.API
{
    public class PrescriptionEndpoint : IPrescriptionEndpoint
    {
        public IAPIHelper _apiHelper;

        public PrescriptionEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task AddPrescription(PrescriptionModel prescription)
        {
            if (prescription.ID == 0 )
            { 
                using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Prescription/Add", prescription))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Log Successful Call
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            else 
                {
                using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Prescription/Edit", prescription))
                    if (response.IsSuccessStatusCode)
                    {
                        // Log successful call
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
        }
    }
}

