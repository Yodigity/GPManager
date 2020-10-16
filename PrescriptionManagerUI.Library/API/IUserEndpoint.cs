using PrescriptionDataManager.Library.Models;
using PrescriptionManagerUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrescriptionManagerUI.Library.API
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task<LoggedInUserModel> GetUserId();
        Task AddUser(UserAddModel userDetails);
        Task AddUserToRole(string userId, string roleName);
        Task RemoveUserToRole(string userId, string roleName);
    }
}