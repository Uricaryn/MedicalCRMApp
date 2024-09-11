using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.UserDTOs;

namespace Medical_CRM_Domain.Services
{
    public interface IUserService
    {
        Task<UserGetDto> GetUserByIdAsync(Guid id);
        Task<UserGetDto> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(UserCreateDto userCreateDto);
        Task UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task DeleteUserAsync(Guid id);
    }
}
