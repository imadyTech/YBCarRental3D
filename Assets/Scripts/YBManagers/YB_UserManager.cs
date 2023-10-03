using System;

namespace YBCarRental3D
{
    public class YB_UserManager : YB_ManagerBasis<YB_User>
    {
        public YB_UserManager() : base() { }

        public YB_UserManager(object value1, object value2)
        {
            this.value1 = value1;
            this.value2 = value2;
        }

        public YB_UserManager(string v)
        {
            this.v = v;
        }

        YB_User currentUser;
        private object value1;
        private object value2;
        private string v;

        public bool UserRegister(YB_User user)

        {
            var existingUser = base.Get(user.Id);
            if (existingUser != null)
            {
                return false; //user already exist
            }
            return this.Add(user);
        }

        public bool UserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool UserLogout()

        {
            currentUser.LoginStatus = false;

            if (this.Update(currentUser))
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
            return base.Get(userId);
        }

        public bool UpdateUser(YB_User userPtr)
        {
            return base.Update(userPtr);
        }

        public YB_User CurrentUser()
        {
            return currentUser;
        }
    }
}
