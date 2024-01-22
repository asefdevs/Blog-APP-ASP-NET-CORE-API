using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;



namespace newProject.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> AllUsers();
        Task<UserResponse> CreateUser(UserCreateRequest model);

        Task<UserResponse> UpdateUser(int id, UserUpdateRequest model);

        Task<UserResponse> GetUserById(int id);

        Task<LoginResponse> Login(LoginRequest model);  

        Task<UserResponse> MyProfile(ClaimsPrincipal user);
    }

    public class UserService : IUserService
    {
        private readonly MyprojectdbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public UserService(MyprojectdbContext context, IMapper mapper, JwtUtils jwtUtils)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        public async Task<List<UserResponse>> AllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserResponse>>(users);
        }

        public async Task<UserResponse> CreateUser(UserCreateRequest model)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == model.UserName))
            {
                throw new Exception("Username " + model.UserName + " is already taken");
            }

            if (await _context.Users.AnyAsync(x => x.Email == model.Email))
            {
                throw new Exception("Email " + model.Email + " is already taken");
            }

            if (model.Password != model.Password2)
            {
                throw new Exception("Password and Confirm Password does not match");
            }

            var userEntity = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                IsAdmin = model.IsAdmin,
                IsActive = model.IsActive,
            };

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(userEntity);
        }

        public async Task<UserResponse> UpdateUser(int id, UserUpdateRequest model)
        {
            var userEntity = await _context.Users.FindAsync(id);

            if (userEntity == null)
            {
                throw new Exception("User not found");
            }

            if (!string.IsNullOrEmpty(model.Email) && 
                await _context.Users.AnyAsync(x => x.Id != id && x.Email == model.Email))
            {
                throw new Exception("Email " + model.Email + " is already taken");
            }

            if (!string.IsNullOrEmpty(model.UserName) && 
                await _context.Users.AnyAsync(x => x.Id != id && x.UserName == model.UserName))
            {
                throw new Exception("Username " + model.UserName + " is already taken");
            }

            if (!string.IsNullOrEmpty(model.UserName))
            {
                userEntity.UserName = model.UserName;
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                userEntity.Email = model.Email;
            }


            userEntity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(userEntity);
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);

            if (userEntity == null)
            {
                throw new Exception("User not found");
            }

            return _mapper.Map<UserResponse>(userEntity);
        }

        public async Task<LoginResponse> Login(LoginRequest model)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName && x.Password == model.Password);

            if (userEntity == null) return null;
            var token  = _jwtUtils.GenerateToken(userEntity);

            return  new LoginResponse(userEntity, token);
        }

        public async Task<UserResponse> MyProfile(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst("UserId");

            if (userIdClaim != null)
            {
                var userIdstring = userIdClaim.Value;
                var userId = int.Parse(userIdstring);

                var userEntity = await _context.Users.FindAsync(userId);

                return _mapper.Map<UserResponse>(userEntity);
            }

            return null;
        }

    }
}
    // [HttpGet]
    // [Route("MyProfile")]
    // [Authorize]
    // public IActionResult MyProfile()
    // {
    //     try
    //     {
    //         var userIdClaim =  User.FindFirst("UserId");

    //         if (userIdClaim != null)
    //         {
    //             var userId = userIdClaim.Value;

    //             // Now you can use the userId as needed in your action
    //             // For example, fetch user profile data based on the user ID

    //             // TODO: Fetch user profile data using the userId

    //             // For demonstration, you can return the user ID as part of the response
    //             return Ok(new { UserId = userId });
    //         }

    //         // If the user ID claim is not found, return an error response
    //         return BadRequest("User ID not found in claims.");
    //     }
    //     catch (Exception ex)
    //     {
    //         // Log the exception or handle it as needed
    //         return StatusCode(500, "Internal Server Error");
    //     }
    // }
