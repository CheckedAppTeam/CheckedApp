namespace CheckedAppProject.LOGIC.Services.Validators
{
    public interface IInputDataValidator
    {
        bool ValidateAge(int age);
        bool ValidateEmail(string email);
        bool ValidatePassword(string password);
        bool ValidateString(string str);
    }
}