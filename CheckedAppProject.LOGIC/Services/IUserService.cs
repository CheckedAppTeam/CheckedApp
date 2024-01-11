﻿using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserDTO dto);
        Task<bool> DeleteUserDataAsync(string userId);
        Task<IEnumerable<UserDataDTO>> GetAllUsersDataDtoAsync();
        Task<UserDataDTO> GetUserDataDtoAsync(string userId);
        Task<bool> UpdateUser(UserUpdateDTO dto, string userId);
    }
}