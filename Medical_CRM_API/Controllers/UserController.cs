using Medical_CRM_Domain.DTOs.UserDTOs;
using Medical_CRM_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Medical_CRM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: api/User/all
        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDtos = users.Select(user => new UserGetDto
            {
                Id = Guid.Parse(user.Id),
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                LastLoginDate = user.LastLoginDate,
                Password = user.PasswordHash
            }).ToList();

            return Ok(userDtos);
        }

        // POST: api/User
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User created successfully." });
            }

            // Collect errors and return as a response
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        // GET: api/User/{id}
        [HttpGet("Find/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (!Guid.TryParse(id, out var userId))
            {
                return BadRequest(new { Message = "Invalid user ID format." });
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            var userDto = new UserGetDto
            {
                Id = Guid.Parse(user.Id),
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                LastLoginDate = user.LastLoginDate
            };

            return Ok(userDto);
        }

        // PUT: api/User/{id}
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateDto model)
        {
            if (!Guid.TryParse(id, out var userId) || userId != model.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            user.UserName = model.UserName;
            user.FullName = model.FullName;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            // Update the password if provided
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                // Remove existing password (if applicable) and add the new one
                await _userManager.RemovePasswordAsync(user);
                var passwordResult = await _userManager.AddPasswordAsync(user, model.Password);

                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }

            return Ok(new { Message = "User updated successfully." });
        }

        // DELETE: api/User/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            // Validate if the provided ID is in correct GUID format
            if (!Guid.TryParse(id, out var userId))
            {
                return BadRequest(new { Message = "Invalid user ID format." });
            }

            // Attempt to find the user by ID
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // Delete the user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                // Collect errors and return as a response
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            // Successful deletion response
            return Ok(new { Message = "User deleted successfully." });
        }

        // POST: api/User/login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Attempt to find the user by username or email
            var user = await _userManager.FindByNameAsync(model.UserName) ??
                       await _userManager.FindByEmailAsync(model.UserName);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }

            // Verify the password
            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!passwordCheck.Succeeded)
            {
                return Unauthorized(new { Message = "Incorrect password." });
            }

            // Successful login response
            return Ok(new { Message = $"Login successful! Welcome, {user.UserName}." });
        }
    }
}
