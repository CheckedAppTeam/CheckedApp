using AutoMapper;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.DbServices.Repository;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;

namespace CheckedAppProject.LOGIC.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(UserItemContext userItemContext, IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<UserDataDTO> GetUserDataDtoAsync(string id)
        {
            var userFromDb = await _userRepository.GetUserAsync(query => query.Where(u => u.AppUserId == id));

            if (userFromDb == null) return null;

            var userDto = _mapper.Map<UserDataDTO>(userFromDb);
            return userDto;
        }
        public async Task<IEnumerable<UserDataDTO>> GetAllUsersDataDtoAsync()
        {
            var users = await _userRepository.GetAllUsersDataAsync();

            var usersDtos = _mapper.Map<List<UserDataDTO>>(users);

            return usersDtos;
        }

        public async Task AddUserAsync(AddUserDTO dto)
        {
            var user = _mapper.Map<UserAccount>(dto);
            user.AppUserId = "hahahahahahaha";
            await _userRepository.AddUserAsync(user);

        }
        public async Task<bool> DeleteUserDataAsync(string userId)
        {
            return await _userRepository.DeleteUserAsync(query => query.Where(u => u.AppUserId == userId));

        }
        public async Task<bool> UpdateUser(UserUpdateDTO dto, string userId)
        {
            var user = _mapper.Map<UserAccount>(dto);
            return await _userRepository.EditUserData(user, userId);
        }
    }
}
