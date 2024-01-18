using CheckedAppProject.DATA.Entities;
using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.API.Contracts
{
    public record RegistrationRequest(   
    [Required]string Email,
    [Required]string Username,
    [Required]string Password);  
}
