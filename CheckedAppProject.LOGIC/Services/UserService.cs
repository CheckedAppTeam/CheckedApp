using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CheckedAppProject.LOGIC.Services
{
    public class UserService : IUserService
    {
        private readonly UserItemContext _userItemContext;
        private readonly IMapper _mapper;

        public UserService(UserItemContext userItemContext, IMapper mapper)
        {
            _userItemContext = userItemContext;
            _mapper = mapper;
        }
        public async Task<UserDataDTO> GetUserDataDtoAsync(int userId)
        {
            var userDto = await _userItemContext.Users
                .Where(u => u.UserId == userId)
                .Include(e => e.ItemList)
                .ProjectTo<UserDataDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return userDto;
        }
        public async Task<IEnumerable<UserDataDTO>> GetAllUsersDataDtoAsync()
        {
            var users = await _userItemContext
                .Users
                .Include(e => e.ItemList)
                .ToListAsync();

            var usersDtos = _mapper.Map<List<UserDataDTO>>(users);

            return usersDtos;
        }

        public async Task AddUserAsync(AddUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            _userItemContext.Users.Add(user);
            await _userItemContext.SaveChangesAsync();

        }
        public async Task<bool> DeleteUserDataDtoAsync(int userId)
        {
            var user = await _userItemContext.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();
            if (user is null) return false;

            _userItemContext.Users.Remove(user);
            await _userItemContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUser(int userId, AddUserDTO dto)
        {
            var user = await _userItemContext.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync();
            if (user is null) return false;

            user.UserName = dto.UserName;
            user.UserSurname = dto.UserSurname;
            user.UserEmail = dto.UserEmail;
            user.Password = dto.Password;
            user.UserAge = dto.UserAge;
            user.UserSex = dto.UserSex;

            await _userItemContext.SaveChangesAsync();
            return true;
        }
    }
}
