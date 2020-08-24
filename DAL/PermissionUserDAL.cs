using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DAL
{
    public static class PermissionUserDAL
    {
        public static Boolean CheckPermissionUser(string userId, int permissionId)
        {
            using (var db = new DBContext())
            {
                User user = db.Users.Include("PermissionGroups").AsQueryable().Where(u => u.UserName == userId).FirstOrDefault();
                if (user != null)
                {
                    IEnumerable<PermissionGroup> permission = user.PermissionGroups.Where(p => p.PermissionGroupId == permissionId);
                    if (permission.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
