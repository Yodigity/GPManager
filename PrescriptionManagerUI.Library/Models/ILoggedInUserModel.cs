using System;

namespace PrescriptionDataManager.Library.Models
{
    public interface ILoggedInUserModel
    {
        DateTime CreatedData { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        string Token { get; set; }

        void ResetUserModel();
    }
}