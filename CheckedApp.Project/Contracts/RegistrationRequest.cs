using CheckedAppProject.LOGIC.DTOs;
using System.ComponentModel.DataAnnotations;

namespace CheckedAppProject.API.Contracts
{
    public record RegistrationRequest
        (   
            AddUserDTO addUserDto
        );  
}
