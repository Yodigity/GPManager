using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.Models
{
    public class PatientNoteModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
