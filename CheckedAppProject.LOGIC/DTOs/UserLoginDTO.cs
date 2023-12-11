using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedAppProject.LOGIC.DTOs
{
    public class UserLoginDTO
    {
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public bool IsUserLogged { get; set; }
    }
}
