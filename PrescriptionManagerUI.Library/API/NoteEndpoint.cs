using PrescriptionManagerUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.API
{
    public class NoteEndpoint : INoteEndpoint
    {

        IAPIHelper _apiHelper;

        public NoteEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task AddNote(PatientNoteModel note)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Notes/Add", note))
            {
                if (response.IsSuccessStatusCode)
                {
                    //log success
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
