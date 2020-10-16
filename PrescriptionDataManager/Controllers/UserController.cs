using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using PrescriptionDataManager.Library.DataAccess;
using PrescriptionDataManager.Library.Models;
using PrescriptionDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PrescriptionDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {

        [HttpGet]
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userId).First();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var users = userManager.Users.ToList();
                var roles = context.Roles.ToList();

                foreach (var user in users)
                {
                    ApplicationUserModel u = new ApplicationUserModel
                    {
                        Id = user.Id,
                        Email = user.Email
                    };

                    foreach (var r in user.Roles)
                    {
                        u.Roles.Add(r.RoleId, roles.Where(x => x.Id == r.RoleId).First().Name);
                    }
                    output.Add(u);
                }
            }
            return output;
        }

        [Authorize(Roles ="Admin")]
        [Route("Admin/RegisterUserDetails")]
        public void RegisterUserDetails(UserAddModel userDeets)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var users = userManager.Users.ToList();
                var user = users.Find(item => item.Email == userDeets.Email);

                Console.WriteLine();
                /*foreach (var user in users)
                {
                    ApplicationUserModel u = new ApplicationUserModel
                    {
                        Id = user.Id,
                        Email = user.Email
                    };
                }*/
                var userDetails = new UserAddModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = userDeets.FirstName,
                    LastName = userDeets.LastName

                };
                UserData data = new UserData();
                data.RegisterUserDetails(userDetails);
                }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                var roles = context.Roles.ToDictionary(x => x.Id, x => x.Name);
                return roles;
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AddRole")]
        public void AddRole(UserRolePairModel pairing)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.AddToRole(pairing.UserId, pairing.RoleName);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveRole")]
        public void RemoveRole(UserRolePairModel pairing)
        {
            using (var context = new ApplicationDbContext())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                userManager.RemoveFromRole(pairing.UserId, pairing.RoleName);
            }
        }
    }

        //LEGACY CODE SUBJECT TO DELETION
        /* public IAPIHelper _apiHelper { get; set; }
    public UserController(IAPIHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }
    // GET: User
    public ActionResult Index()
    {
        return View();
    }*/
        /*
                [Authorize]
                public ActionResult Register()
                {
                    return View();
                }

                public ActionResult Login()
                {

                    return View();
                }

                [HttpPost]
                public async Task Login(string username, string password)
                {

                    try
                    {
                        var result = await _apiHelper.Authenticate(username, password);

                        await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

                    }
                    catch (Exception)
                    {

                        throw new Exception();
                    }

                }*/
    
}