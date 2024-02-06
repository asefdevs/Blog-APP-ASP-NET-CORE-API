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
        Task<GenerateTOTPResponse> GenerateTotp(GenerateTOTPRequest model);
        Task<bool> VerifyTotp(VerifyTotpRequest model);

    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly MyprojectdbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        private readonly TOTP _totpservice;

        public AuthenticationService(MyprojectdbContext context, IMapper mapper, JwtUtils jwtUtils, TOTP totpservice)
        {
            _context = context;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
            _totpservice = totpservice;
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

        public async Task<GenerateTOTPResponse> GenerateTotp(GenerateTOTPRequest model)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(
            x => x.UserName == model.UserName && x.Email == model.Email && x.IsActive == false);
            if (userEntity != null)
            {
            var secretKey = _totpservice.GenerateRandomKey();
            var totpCode = _totpservice.GenerateTotpCode(secretKey);
            return new GenerateTOTPResponse(secretKey, totpCode);
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<bool> VerifyTotp(VerifyTotpRequest model)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.email);
            if (userEntity == null)
            {
                throw new Exception("User not found");
            }
            if (userEntity.IsActive == true)
            {
                throw new Exception("User is already verified");
            }
            userEntity.IsActive = true;
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
            var isVerified = _totpservice.VerifyTotpCode(model.totpCode, model.secretKey);
            return isVerified;
            
        }

    }
}