using System;
using DAL;
using Entities;
using Entities.DTO;

namespace BL
{
    public static class UserLoginBL
    {

        public static UserDto Login(string userName, string password)
        {
            var user = UserDAL.Login(userName, password);
            //if (user.IsAuthorized)
               // UserEntranceDAL.AddUserEntrance(user.UserId);
            return user;
        }

        public static void AddUser(UserDto user)
        {
            UserDAL.AddUser(user);
        }
    }
}

