using Entities;
using System;
using System.Linq;
using Entities.DTO;

namespace DAL
{
    public static partial class UserDAL
    {
     //   static public DBContext db = new DBContext();
        public static UserDto Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return new UserDto
                {
                    IsAuthorized = false,
                    ErrorMessage = "לא התקבלו נתונים"
                };
            using (var db = new DBContext())
            {
                try
                {
                    User user = db.Users.FirstOrDefault(findId => findId.Password.Equals(password)&& findId.UserName.Equals(userName));
                    if (user != null)
                        return new UserDto
                        {
                            IsAuthorized = true,
                            UserId = user.UserId,
                            UserName = user.UserName
                        };
                    return new UserDto
                    {
                        IsAuthorized = false,
                        ErrorMessage = "שם משתמש או סיסמה שגויים"
                    };
                }
                catch (Exception ex)
                {
                    return new UserDto
                    {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                    };
                }
            }
        }

        public static void AddUser(UserDto user)
        {
            using (var db = new DBContext())
            {
                db.Users.Add(Converter.ConvertUserDtoToTbl(user));
                db.SaveChanges();
            }
        }
    }
}