using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;

namespace newProject.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> AllUsers();
        Task<UserResponse> CreateUser(UserCreateRequest model);

        Task<UserResponse> UpdateUser(int id, UserUpdateRequest model);

        Task<UserResponse> GetUserById(int id);

        Task<LoginResponse> Login(LoginRequest model);  

        // Task<UserResponse> MyProfile(int id);
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

            userEntity.UserName = model.UserName;
            userEntity.Email = model.Email;
            userEntity.Password = model.Password;
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
    }

}
