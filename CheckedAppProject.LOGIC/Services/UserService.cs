using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckedAppProject.DATA;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.DATA.Entities;
using CheckedAppProject.LOGIC.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web.Http;

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
                .Where(u => u.UserTableId == userId)
                .Include(e => e.ItemListTable)
                .ProjectTo<UserDataDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return userDto;
        }

        public async Task AddUserAsync(AddUserDTO dto)
        {
            var user = _mapper.Map<UserTable>(dto);
            _userItemContext.Users.Add(user);
            await _userItemContext.SaveChangesAsync();

        }
    }
}
