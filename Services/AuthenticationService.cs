using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;
using BCrypt.Net;




namespace newProject.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponse> CreateUser(UserCreateRequest userModel, ProfileCreateRequest profileModel);

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



        public async Task<UserResponse> CreateUser(UserCreateRequest userModel, ProfileCreateRequest profileModel)
        {
            try
            {
            if (await _context.Users.AnyAsync(x => x.UserName ==userModel.UserName))
            {
                throw new Exception("Username " + userModel.UserName + " is already taken");
            }

            if (await _context.Users.AnyAsync(x => x.Email == userModel.Email))
            {
                throw new Exception("Email " + userModel.Email + " is already taken");
            }

            if (userModel.Password != userModel.Password2)
            {
                throw new Exception("Password and Confirm Password does not match");
            }

            var userEntity = new User
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password),
            };
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
            var userProfile = new UserProfile
            {
                UserId = userEntity.Id,
                FirstName = profileModel.FirstName,
                LastName = profileModel.LastName,
                BirthDate = profileModel.BirthDate,
                Country = profileModel.Country,
                Bio = profileModel.Bio
            };
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<LoginResponse> Login(LoginRequest model)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(
            x => x.UserName == model.UserName
            );

            if (userEntity == null)
            {
                throw new Exception("User Not found");
            }
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, userEntity.Password);
            if (!isValidPassword)
            {
                throw new Exception("Invalid Password");
            }
            if (userEntity.IsActive == false)
            {
                throw new Exception("No active user found");
            }
            var token  = _jwtUtils.GenerateToken(userEntity);

            return  new LoginResponse(userEntity, token);
        }

        public async Task<GenerateTOTPResponse> GenerateTotp(GenerateTOTPRequest model)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(
            x => x.UserName == model.UserName && x.Email == model.Email);
            if (userEntity == null)
            {
                throw new Exception("User not found");
            }
            if (userEntity.IsActive == true)
            {
                throw new Exception("User is already verified");
            }
            else
            {
            var secretKey = _totpservice.GenerateRandomKey();
            var totpCode = _totpservice.GenerateTotpCode(secretKey);
            return new GenerateTOTPResponse(secretKey, totpCode);
            }
        }

        public async Task<bool> VerifyTotp(VerifyTotpRequest model)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.email);
            if (userEntity == null)
            {
                throw new Exception("User not found");
            }
            var isVerified = _totpservice.VerifyTotpCode(model.totpCode, model.secretKey);
            // if (userEntity.IsActive == true)
            // {
            //     throw new Exception("User is already verified");
            // }
            if (isVerified == false)
            {
                return false;
            }
            userEntity.IsActive = true;
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
            return isVerified;
            
        }

    }
}