using PrescriptionManagerUI.Library.Models;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.API
{
    public interface IPrescriptionEndpoint
    {
        Task AddPrescription(PrescriptionModel prescription);
    }
}