using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.API.Contracts
{
    public record RegistrationRequest
        (   
            [Required]string Email,
            [Required]string Username,
            string? UserSurname,
            [Required]string Password,
            int UserAge,
            string? UserSex
        );  
}
