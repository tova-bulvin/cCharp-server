using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UserDto
    {
        public bool IsAuthorized { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
