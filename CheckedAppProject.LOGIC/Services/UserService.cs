using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckedAppProject.DATA.CheckedAppDbContext;
using CheckedAppProject.LOGIC.DTOs;
using Microsoft.EntityFrameworkCore;

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

            var userDtos = await _userItemContext.Users
                .Where(u => u.UserTableId == userId)
                .ProjectTo<UserDataDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            //var userDtos = _userItemContext
            //    .Users
            //    //.Include(u => u.OwnItemList)
            //    .FirstOrDefaultAsync(u => u.UserTableId == userId);

            if (userDtos == null) return null;

            //var result = _mapper.Map<UserDataDTO>(userDtos);

            return userDtos;
        }
    }
}
