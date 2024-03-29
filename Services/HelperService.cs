using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using newProject.Helpers.Dto;
using AutoMapper;

namespace newProject.Services
{
    public interface IHelperService
    {
        Task<List<UserResponse>> AllUsers();
        Task<List<CategoryDto>> AllCategories();
    }


    public class HelperService : IHelperService
    {
        private readonly MyprojectdbContext _context;
        private readonly IMapper _mapper;


        public HelperService(MyprojectdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> AllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<List<UserResponse>>(users);
        }

        public async Task<List<CategoryDto>> AllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }


    }
}