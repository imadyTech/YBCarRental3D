using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace YBCarRental3D
{
    public class YB_UserManager : YB_ManagerBasis<YB_User>
    {
        private YB_User _currentUser;
        public YB_User CurrentUser => _currentUser;

        public YB_UserManager(string baseUrl) : base(baseUrl, "YBUsers")
        {
        }


        public async Task<bool> UserRegister(YB_User user)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/register";
            var postdata = JsonConvert.SerializeObject(user);
            var result = await apiContext.PostRequest(requestString, postdata);
            try
            {
                _currentUser = JsonConvert.DeserializeObject<YB_User>(result);
            }
            catch (Exception ex)
            {
                Debug.Log($"[Register error] : {ex.Message}");
                _currentUser = null;
                return false;
            }
            if (result == null) 
                return false;
            else 
                return true;
        }

        public async Task<YB_User> UserLogin(string username, string password)
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/login";
            var postdata = $"{{\"userName\":\"{username}\",\"password\": \"{password}\"}}";
            var result = await apiContext.PostRequest(requestString, postdata);
            try
            {
                _currentUser = JsonConvert.DeserializeObject<YB_User>(result);
            }
            catch (Exception ex)
            {
                Debug.Log($"[Login error] : {ex.Message}");
                _currentUser = null;
            }
            return _currentUser;
        }

        public async Task<bool> UserLogout()
        {
            var requestString = $"{this.apiContext.BaseApiUrl}/logout";
            var postdata = $"{{\"userName\":\"{_currentUser.UserName}\",\"password\": \"{_currentUser.Password}\"}}";
            var result = await apiContext.PostRequest(requestString, postdata);

            _currentUser = null;
            return true;
        }

        public bool IsAdmin()
        {
            return _currentUser.UserRoles.Contains("admin");
        }


        public async Task<YB_User> GetUser(int userId)
        {
            return await base.Get(userId);
        }

        public async Task<bool> UpdateUser(YB_User userPtr)
        {
            return await base.Update(userPtr);
        }

        public async Task<IEnumerable<YB_User>> ListUsers(int pageNum, int pageSize)
        {
            return await base.GetList(pageNum, pageSize);
        }
    }
}
