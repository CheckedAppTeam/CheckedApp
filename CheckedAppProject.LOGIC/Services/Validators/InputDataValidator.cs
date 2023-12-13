using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedAppProject.LOGIC.Services.Validators
{
    public class InputDataValidator : IInputDataValidator
    {
        public bool ValidateEmail(string email) { return false; }
        public bool ValidatePassword(string password) { return false; }
        public bool ValidateString(string str) { return false; }
        public bool ValidateAge(int age) { return false; }
    }
}
