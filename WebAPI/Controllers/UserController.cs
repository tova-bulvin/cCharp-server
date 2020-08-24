using BL;
using Entities.DTO;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://makeup4u.herokuapp.com,http://localhost:4200", headers: "*", methods: "*")]
    public class UserController : ApiController
    {

        [HttpGet]
        [Route("api/User/Login")]
        public UserDto Login(string userName, string password)
        {
            return UserLoginBL.Login(userName, password);
        }
        [HttpPost]
        [Route("api/User/AddUser")]
        public void AddUser(UserDto user)
        {
            UserLoginBL.AddUser(user);
        }
    }
}
