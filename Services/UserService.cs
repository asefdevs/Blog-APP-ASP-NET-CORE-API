using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;
using System;

namespace newProject.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> AllUsers();
        Task<UserResponse> CreateUser(UserCreateRequest model);
    }

    public class UserService : IUserService
    {
        private readonly MyprojectdbContext _context;
        private readonly IMapper _mapper;

        public UserService(MyprojectdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> AllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserResponse>>(users);
        }

        public async Task<UserResponse> CreateUser(UserCreateRequest model)
        {
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
    }

}
