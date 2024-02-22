using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;
using System.Security.Claims;
using newProject.Exceptions;



namespace newProject.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> AllUsers();

        Task<UserResponse> UpdateUser(int id,ClaimsPrincipal user, UserUpdateRequest model, ProfileCreateRequest? profileModel);

        Task<UserResponse> GetUserById(int id);

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
            var users = await _context.Users.Include(x => x.UserProfiles).ToListAsync();
            return _mapper.Map<List<UserResponse>>(users);
        }


        public async Task<UserResponse> UpdateUser(int id, ClaimsPrincipal user, UserUpdateRequest model, ProfileCreateRequest? profileModel)
        {
          
                int? userId = ClaimsHelper.RequestedUser(user);
                if (userId != id)
                {
                    throw new ForbbidenAccessException("You are not allowed to update this user");
                }
                var userEntity = await _context.Users.Include(x => x.UserProfiles).FirstOrDefaultAsync(x => x.Id == id);
                if (userEntity == null)
                {
                    throw new NotFoundException("User not found");
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
                userEntity.UpdatedAt = DateTime.Now;
                userEntity.Email = model.Email;
                userEntity.UserName = model.UserName;
                await _context.SaveChangesAsync();

                if (profileModel != null)
                {
                    if (userEntity.UserProfiles.Count == 0)
                    {
                        userEntity.UserProfiles.Add(new UserProfile());
                    }

                    var userProfile = userEntity.UserProfiles.First(); 
                    userProfile.FirstName = profileModel.FirstName;
                    userProfile.LastName = profileModel.LastName;
                    userProfile.BirthDate = profileModel.BirthDate;
                    userProfile.Country = profileModel.Country;
                    userProfile.Bio = profileModel.Bio;
                }
                await _context.SaveChangesAsync();


                return _mapper.Map<UserResponse>(userEntity);
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var userEntity = await _context.Users.Include(x => x.UserProfiles).FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity == null)
            {
                throw new NotFoundException("User not found");
            }

            return _mapper.Map<UserResponse>(userEntity);
        }
        public async Task<UserResponse> MyProfile(ClaimsPrincipal user)
        {
           
                int? userId = ClaimsHelper.RequestedUser(user);
                var userEntity = await _context.Users.Include(x => x.UserProfiles).FirstOrDefaultAsync(x => x.Id == userId);
                if (userEntity == null)
                {
                    throw new NotFoundException("User not found");
                }
                return _mapper.Map<UserResponse>(userEntity);
            
      
        }
    }
}