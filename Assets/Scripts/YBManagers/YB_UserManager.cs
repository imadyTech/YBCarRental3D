using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace YBCarRental3D
{
    public class YB_UserManager : YB_ManagerBasis<YB_User>
    {
        YB_User currentUser;

        public YB_UserManager(string baseUrl): base(baseUrl, "YBUsers")
        {
        }



        public bool UserRegister(YB_User user)

        {
            var existingUser = base.Get(user.Id);
            if (existingUser != null)
            {
                return false; //user already exist
            }
            return base.Add(user).Result;
        }

        public async Task<YB_User> UserLogin(string username, string password)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/login";
            var postdata = $"{{\"userName\":\"{username}\",\"password\": \"{password }\"}}";
            var result = await apiContext.PostRequest(requestString, postdata);
            currentUser = JsonConvert.DeserializeObject<YB_User>(result);
            return currentUser;
        }

        public bool UserLogout()

        {
            currentUser.LoginStatus = false;

            if (this.Update(currentUser).Result)
                return true;
            else
                return false;
        }

        public bool IsAdmin()
        {
            return currentUser.UserRoles.Contains("admin");
        }


        public YB_User GetUser(int userId)
        {
            return base.Get(userId).Result;
        }

        public bool UpdateUser(YB_User userPtr)
        {
            return base.Update(userPtr).Result;
        }

        public YB_User CurrentUser
        {
            get => currentUser;
        }
    }
}
