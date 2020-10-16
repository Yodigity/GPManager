using PrescriptionManagerUI.Library.Models;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.API
{
    public interface INoteEndpoint
    {
        Task AddNote(PatientNoteModel note);
    }
}