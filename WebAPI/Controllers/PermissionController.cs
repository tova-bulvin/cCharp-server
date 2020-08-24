using Matchmaker.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Matchmaker.WebAPI.Controllers
{
    public class PermissionController : ApiController
    {

        [HttpGet]
        [ActionName("CheckPermissionUser")]

        public Boolean CheckPermissionUser(string id, string pid)
        {
            if (id == null || pid == null)
            {
                return false;
            }
            else
            {
                int convertedpId = Convert.ToInt32(pid);//both enums are overlapping......(client & server side)

                Boolean result = PermissionUserBL.CheckPermissionUser(id, convertedpId);

                return result;
            }
        }
    }
}