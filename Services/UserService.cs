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

        Task<UserResponse> UpdateUser(int id,ClaimsPrincipal user, UserUpdateRequest model);

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
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserResponse>>(users);
        }


        public async Task<UserResponse> UpdateUser(int id, ClaimsPrincipal user, UserUpdateRequest model)
        {
          
                int? userId = ClaimsHelper.RequestedUser(user);
                if (userId != id)
                {
                    throw new ForbbidenAccessException("You are not allowed to update this user");
                }
                var userEntity = await _context.Users.FindAsync(id);
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == id);
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
                var userEntity = await _context.Users.FindAsync(userId);
                if (userEntity == null)
                {
                    throw new NotFoundException("User not found");
                }
                return _mapper.Map<UserResponse>(userEntity);
            
      
        }
    }
}