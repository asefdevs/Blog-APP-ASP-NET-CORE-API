using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;



namespace newProject.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponse> CreateUser(UserCreateRequest model);

        Task<LoginResponse> Login(LoginRequest model);  

    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly MyprojectdbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public AuthenticationService(MyprojectdbContext context, IMapper mapper, JwtUtils jwtUtils)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
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


        public async Task<LoginResponse> Login(LoginRequest model)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName && x.Password == model.Password);

            if (userEntity == null) return null;
            var token  = _jwtUtils.GenerateToken(userEntity);

            return  new LoginResponse(userEntity, token);
        }


    }
}