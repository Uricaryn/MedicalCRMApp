using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.UserDTOs;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Medical_CRM_Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        // Fetches a user by ID and maps it to a UserGetDto
        public async Task<UserGetDto> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return _mapper.Map<UserGetDto>(user);
        }

        // Fetches a user by username and maps it to a UserGetDto
        public async Task<UserGetDto> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.", nameof(username));

            var user = await _unitOfWork.Users.GetUserByUsernameAsync(username);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            return _mapper.Map<UserGetDto>(user);
        }

        // Creates a new user based on the provided UserCreateDto and assigns roles
        public async Task CreateUserAsync(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
                throw new ArgumentNullException(nameof(userCreateDto), "User data is required.");

            var user = _mapper.Map<User>(userCreateDto);

            try
            {
                // Create the user using UserManager to handle Identity-specific creation
                var createResult = await _userManager.CreateAsync(user, userCreateDto.Password);
                if (!createResult.Succeeded)
                {
                    throw new Exception($"Error creating user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }

                // Assign roles if provided
                foreach (var role in userCreateDto.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        // Create the role if it doesn't exist
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }

                    // Add the user to the role
                    var roleResult = await _userManager.AddToRoleAsync(user, role);
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception($"Error assigning role '{role}' to user: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                    }
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the error if logging is set up
                // _logger.LogError(ex, "Error creating user with roles");
                throw new Exception("An error occurred while creating the user with roles.", ex);
            }
        }

        // Updates an existing user based on the provided UserUpdateDto and updates roles
        public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null)
                throw new ArgumentNullException(nameof(userUpdateDto), "User update data is required.");

            var existingUser = await _unitOfWork.Users.GetByIdAsync(userUpdateDto.Id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found for update.");

            _mapper.Map(userUpdateDto, existingUser);

            try
            {
                var updateResult = await _userManager.UpdateAsync(existingUser);
                if (!updateResult.Succeeded)
                {
                    throw new Exception($"Error updating user: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
                }

                // Update roles if specified in the update DTO
                var currentRoles = await _userManager.GetRolesAsync(existingUser);
                var rolesToRemove = currentRoles.Except(userUpdateDto.Roles).ToList();
                var rolesToAdd = userUpdateDto.Roles.Except(currentRoles).ToList();

                // Remove roles that are no longer needed
                if (rolesToRemove.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(existingUser, rolesToRemove);
                    if (!removeResult.Succeeded)
                    {
                        throw new Exception($"Error removing roles: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");
                    }
                }

                // Add new roles
                foreach (var role in rolesToAdd)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }

                    var addResult = await _userManager.AddToRoleAsync(existingUser, role);
                    if (!addResult.Succeeded)
                    {
                        throw new Exception($"Error adding role '{role}': {string.Join(", ", addResult.Errors.Select(e => e.Description))}");
                    }
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the error if logging is set up
                // _logger.LogError(ex, "Error updating user with roles");
                throw new Exception("An error occurred while updating the user with roles.", ex);
            }
        }

        // Deletes a user by ID
        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found for deletion.");

            try
            {
                await _userManager.DeleteAsync(user);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the error if logging is set up
                // _logger.LogError(ex, "Error deleting user");
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }
    }
}
